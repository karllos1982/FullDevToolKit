using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Common;
using FullDevToolKit.Helpers;
using MyApp.API;
using MyApp.Managers;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using FullDevToolKit.Core;
using FullDevToolKit.ApplicationHelpers;
using FullDevToolKit.Core.Common;    


namespace MyApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : APIControllerBase
    {

        public AuthController(IContext context,MailManager mail)
        {
            Init(context, mail,""); 
            
        }
               

        [HttpGet]
        [Route("index")]
        [AllowAnonymous]
        public object Index()
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            throw new Exception("Teste de gerenciamento de erro"); 

           // return ret;
        }


        [HttpGet]
        [Route("listlocalizationtexts")]
        public async Task<object> ListLocalizationTexts()
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            PagedList<LocalizationTextResult> data = null;
            data = await Manager.IdentityModule.Domainset
                .LocalizationText.Search(new LocalizationTextParam()
                {
                    RecordsPerPage= 10000,
                    PageIndex= 0,
                });

            if (data != null)
            {
                ret = SetReturn<List<LocalizationTextResult>>(data.RecordList);
            }
                        
            FinalizeManager();

            return ret;
        }


        //[HttpPost]
        //[Route("registraruser")]
        //[Authorize]
        //public async Task<object> RegistrarUser(NewUser data)
        //{
        //    Init();

        //    data.RoleID = 3; // id do perfil
           
        //    UserEntry obj = await Membership.CreateNewUser(data, true, null); 

        //    if (Context.ExecutionStatus.Status)
        //    {
        //        ret = obj;
                
        //    }
        //    else
        //    {
        //        ret = Context.ExecutionStatus.InnerExceptions;
                
        //        Response.StatusCode = 500;

        //    }

        //    FinalizeManager();

        //    return ret;
        //}


        [HttpPost]
        [Route("sendemailconfirmation")]
        [AllowAnonymous]
        public object SendEmailConfirmation(EmailConfirmation data) //EmailConfirmation
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            Validations val = new Validations();

            if (val.ValidateEmail(data.Email))
            {
                data.Code = Utilities.GenerateCode(6);

                opsts =
                  ((MyApMailCenter)MailCenter).SendEmailConfirmationCode(data.Email, data.UserName, data.Code);

                ret = SetReturn<bool>(opsts.Success);
            }
            else
            {
                Context.Status = new ExecutionStatus();
                Context.Status.SetFailStatus("Email", "O e-mail informado é inválido"); 
                ret = SetReturn<bool>(false);
            }

            FinalizeManager();

            return ret;
        }


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<object> Login(UserLogin param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            param.Password = Utilities.ConvertFromBase64(param.Password);
            param.Password = MD5.BuildMD5(param.Password);
            param.ClientIP = HttpContext.Connection.RemoteIpAddress?.ToString();

            UserResult userM = await Manager.IdentityModule.Login(param);

            if (Context.Status.Success)
            {                
                string permissions_content =  JsonConvert.SerializeObject(userM.Permissions);

                AuthToken token = TokenService.GenerateToken(userM.UserID.ToString(),
                    userM.Roles[0].RoleName, userM.Instances[0].InstanceID.ToString(),
                    permissions_content, userM.LanguageID.ToString() );
                                               
                UserAuthenticated userA = new UserAuthenticated();
                userA.UserID = userM.UserID.ToString();
                userA.UserName = userM.UserName;
                userA.Email = userM.Email;
                userA.RoleName = userM.Roles[0].RoleName;
                userA.InstanceName = userM.Instances[0].InstanceName;
                userA.Token = token.TokenValue;
                userA.Expires = DateTime.Now.AddMinutes(int.Parse(param.SessionTimeOut));
                userA.Permissions = userM.Permissions;
                userA.LanguageID = userM.LanguageID;
                userA.KeepConnection = param.KeepConnection; 

                UpdateUserLogin uplogin = new UpdateUserLogin()
                {
                    UserID = userM.UserID,
                    LastLoginDate = DateTime.Now,
                    AuthToken = token.TokenValue,
                    AuthTokenExpires = userA.Expires
                };
            
               await Manager.IdentityModule.RegisterLoginState(param, uplogin);
                
                switch (userA.RoleName)
                {
                    case "Admin":
                    {
                            userA.HomeURL = "admin/home";
                            break;
                    }

                    case "SuperAdmin":
                    {
                        userA.HomeURL = "superadmin/home";
                        break;
                    }
                  
                }

                string img = userM.ProfileImage; 
                if (img==null) { img = "";  }
                if (img=="") { img = "user_anonymous.png";  }
                userA.ProfileImageURL =
                    Context.Settings.SiteURL+ "auth/GetUserImageProfile?file=" + img;

                ret = SetReturn<UserAuthenticated>(userA);                

            }
            else
            {               
                ret = SetReturn<UserAuthenticated>(null);
            }

            FinalizeManager(); 

            return ret;
        }

		[HttpPost]
		[Route("refreshlogin")]
		[AllowAnonymous]
		public async Task<object> RefreshLogin(AuthTokenModel param)
		{
			BeginManager();
			CheckPermission(PERMISSION_CHECK_ENUM.READ, true);            
			
			UserResult userM = await Manager.IdentityModule.RefreshLogin(param);

			if (Context.Status.Success)
			{
				string permissions_content = JsonConvert.SerializeObject(userM.Permissions);

				AuthToken token = TokenService.GenerateToken(userM.UserID.ToString(),
					userM.Roles[0].RoleName, userM.Instances[0].InstanceID.ToString(),
					permissions_content, userM.LanguageID.ToString());
					
				UserAuthenticated userA = new UserAuthenticated();
				userA.UserID = userM.UserID.ToString();
				userA.UserName = userM.UserName;
				userA.Email = userM.Email;
				userA.RoleName = userM.Roles[0].RoleName;
				userA.InstanceName = userM.Instances[0].InstanceName;
				userA.Token = token.TokenValue;
                userA.Expires = DateTime.Now.AddMinutes(int.Parse(param.SessionTimeOut));
                userA.Permissions = userM.Permissions;
				userA.LanguageID = userM.LanguageID;
				userA.KeepConnection = param.KeepConnection;

				UpdateUserLogin uplogin = new UpdateUserLogin()
				{
					UserID = userM.UserID,
					LastLoginDate = DateTime.Now,
					AuthToken = token.TokenValue,
					AuthTokenExpires = userA.Expires
                };

                UserLogin usrlogin = new UserLogin()
                {
                    ClientIP = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    ClienteBrowserName = ""
                }
                ;

				await Manager.IdentityModule.RegisterLoginState(usrlogin, uplogin);

				switch (userA.RoleName)
				{
					case "Admin":
						{
							userA.HomeURL = "admin/home";
							break;
						}

					case "SuperAdmin":
						{
							userA.HomeURL = "superadmin/home";
							break;
						}

				}

				string img = userM.ProfileImage;
				if (img == null) { img = ""; }
				if (img == "") { img = "user_anonymous.png"; }
				userA.ProfileImageURL =
					Context.Settings.SiteURL + "auth/GetUserImageProfile?file=" + img;

				ret = SetReturn<UserAuthenticated>(userA);

			}
			else
			{
				ret = SetReturn<UserAuthenticated>(null);
			}

			FinalizeManager();

			return ret;
		}


		[HttpPost]
        [Route("recoverypassword")]
        [AllowAnonymous]
        public async Task<object> RecoveryPassword(ChangeUserPassword param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            string data  =
                await Manager.IdentityModule.GetTemporaryPassword(param); 

            if (Context.Status.Success)
            {
                opsts =
                  ((MyApMailCenter)MailCenter).SendTemporaryPassword(param.Email, "Usuário", data);

                ret = SetReturn<bool>(opsts.Success);
            }
            else
            {
                ret = SetReturn<bool>(false);
            }
            
         
            FinalizeManager();

            return ret;
        }


        [HttpPost]
        [Route("requestactiveaccountcode")]
        [AllowAnonymous]
        public async Task<object> RequestActiveAccountCode(ActiveUserAccount param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            string data =
                await Manager.IdentityModule.GetActiveAccountCode(param);

            if (Context.Status.Success)
            {
                opsts =
                  ((MyApMailCenter)MailCenter).SendActiveAccountCode(param.Email, "Usuário", data);

                ret = SetReturn<bool>(opsts.Success);
                
            }
            else
            {
                ret = SetReturn<bool>(false);
            }

            FinalizeManager();
         

            return ret;
        }

        [HttpPost]
        [Route("activeaccount")]
        [AllowAnonymous]
        public async Task<object> ActiveAccount(ActiveUserAccount param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

             await Manager.IdentityModule.Domainset.User.ActiveUserAccount(param);

            ret = SetReturn<bool>(Context.Status.Success);  
                       
            FinalizeManager();

            return ret;
        }


        [HttpPost]
        [Route("requestchangepasswordcode")]
        [Authorize]
        public async Task<object> RequestChangePasswordCode(ChangeUserPassword param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            string data =
                await Manager.IdentityModule.GetChangePasswordCode(param);

            if (Context.Status.Success)
            {
                opsts =
                  ((MyApMailCenter)MailCenter).SendChangePassowordCode(param.Email, "Usuário", data);

                ret = SetReturn<bool>(opsts.Success);

            }
            else
            {
                ret = SetReturn<bool>(false);
            }

            FinalizeManager();

            return ret;
        }

        [HttpPost]
        [Route("changepassword")]
        [Authorize]
        public async Task<object> ChangePassword(ChangeUserPassword param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            await Manager.IdentityModule.Domainset.User.ChangeUserPassword(param);

            ret = SetReturn<bool>(Context.Status.Success);

            FinalizeManager();

            return ret;
          
        }

        [HttpPost]
        [Route("changeuserimageprofile")]
        [Authorize]
        public async Task<object> ChangeUserImageProfile()
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);
            MyAppSettings settings = (MyAppSettings)Context.Settings;

            LocalFileService service = new LocalFileService("");
            Stream body = Request.Body;
            
            ChangeUserImage data = new ChangeUserImage();
            data.UserID = Int64.Parse(this.UserID.ToString());
            data.FileName = "IMG_" + Guid.NewGuid() + ".png";

            await Manager.IdentityModule.Domainset.User.ChangeUserProfileImage(data);

            if (Context.Status.Success)
            {
              
                FileOperationResult opsts
                    = await service.UploadFile(body, settings.ProfileImageDir, data.FileName);

                ret = SetReturn<bool>(opsts.Status);
            }
            else
            {
                ret = SetReturn<bool>(Context.Status.Success);
            }         

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("getuserimageprofile")]       
        public FileStreamResult GetUserImageProfile(string file)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            MyAppSettings settings = (MyAppSettings)Context.Settings;

            LocalFileService service = new LocalFileService("");

            Stream str =  service.DownloadFile(settings.ProfileImageDir,file);

            if (str == null)
            {
                str = service.DownloadFile(settings.ProfileImageDir,"user_anonymous.png");
            }

            FileStreamResult result = new FileStreamResult(str, "application/octet-stream");

            return result;
        }


        [HttpPost]
        [Route("changeuserlanguage")]
        public async Task<object> ChangeUserLanguage(ChangeUserLanguage param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            if (IsAllowed)
            {
                bool go = await Manager.IdentityModule.Domainset.User.ChangeUserLanguage(param);
                ret = SetReturn<bool>(go);

            }
            else
            {
                ret = SetReturn<bool>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }


        [HttpGet]
        [Route("logout")]
        [Authorize]
        public async Task<object> Logout()
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            await Manager.IdentityModule.Logout(long.Parse(this.UserID.ToString()));

			ret = SetReturn<bool>(true);

			FinalizeManager();

            return ret;
        }

    }
}
