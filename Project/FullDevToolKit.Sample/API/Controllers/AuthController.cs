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
   


namespace MyApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : APIControllerBase
    {

        public AuthController(IContext context,
            IContextBuilder contextbuilder, MailManager mail)
        {
            Init(context, contextbuilder, "");
            Context.LocalizationLanguage = Context.Settings.LocalizationLanguage;
            this.MailCenter = mail;
            LocalizationText.LoadData(context); 
        }

       

        [HttpGet]
        [Route("index")]
        [AllowAnonymous]
        public object Index()
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);
         

            return ret;
        }


        [HttpGet]
        [Route("listlocalizationtexts")]
        public async Task<object> ListLocalizationTexts()
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            List<LocalizationTextResult> data = null;
            data = await Manager.IdentityModule.Domainset.LocalizationText.Search(new LocalizationTextParam());
            ret = SetReturn<List<LocalizationTextResult>>(data);
            
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
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            param.Password = Utilities.ConvertFromBase64(param.Password);
            param.Password = MD5.BuildMD5(param.Password);
            UserResult userM = await Manager.IdentityModule.Login(param);

            if (Context.Status.Success)
            {                
                string permissions_content =  JsonConvert.SerializeObject(userM.Permissions);

                AuthToken token = TokenService.GenerateToken(userM.UserID.ToString(),
                    userM.Roles[0].RoleName, userM.Instances[0].InstanceID.ToString(),
                    permissions_content, userM.DefaultLanguage, 
                    int.Parse(param.SessionTimeOut) );
                           
                UserAuthenticated userA = new UserAuthenticated();
                userA.UserID = userM.UserID.ToString();
                userA.UserName = userM.UserName;
                userA.Email = userM.Email;
                userA.RoleName = userM.Roles[0].RoleName;
                userA.InstanceName = userM.Instances[0].InstanceName;
                userA.Token = token.TokenValue;
                userA.Expires = token.ExpiresDate;
                userA.Permissions = userM.Permissions;
                userA.LocalizationLanguage = userM.DefaultLanguage; 

                UpdateUserLogin uplogin = new UpdateUserLogin()
                {
                    UserID = userM.UserID,
                    LastLoginDate = DateTime.Now,
                    AuthToken = token.TokenValue,
                    AuthTokenExpires = token.ExpiresDate
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
                
                userA.ProfileImageURL =
                    Context.Settings.SiteURL+ "auth/GetUserImageProfile?file=" + userM.ProfileImage;

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

            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);
            MyAppSettings settings = (MyAppSettings)Context.Settings;

            LocalFileService service = new LocalFileService("", settings.ProfileImageDir);
            Stream body = Request.Body;
            
            ChangeUserImage data = new ChangeUserImage();
            data.UserID = Int64.Parse(this.UserID.ToString());
            data.FileName = "IMG_" + Guid.NewGuid() + ".png";

            await Manager.IdentityModule.Domainset.User.ChangeUserProfileImage(data);

            if (Context.Status.Success)
            {
                opsts = await service.UploadFile(body, data.FileName);

                ret = SetReturn<bool>(opsts.Success);
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
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            MyAppSettings settings = (MyAppSettings)Context.Settings;

            LocalFileService service = new LocalFileService("", settings.ProfileImageDir);

            Stream str =  service.DownloadFile(file);

            FileStreamResult result = new FileStreamResult(str, "application/octet-stream");

            return result;
        }


        [HttpGet]
        [Route("logout")]
        [Authorize]
        public async Task<object> Logout()
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            await Manager.IdentityModule.Logout(long.Parse(this.UserID.ToString())); 

            FinalizeManager();

            return ret;
        }

    }
}
