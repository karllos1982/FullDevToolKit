using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Models.Identity;
using Newtonsoft.Json;
using FullDevToolKit.ApplicationHelpers;


namespace MyApp.Proxys
{

    public interface IAuthProxyManager
    {
        void Init(HttpClient http, string baseurl, string token);
       

        Task<APIResponse<List<LocalizationTextResult>?>> ListLocalizationTexts();

    }

    public class AuthProxy: APIProxyBase, IAuthProxyManager
    {
        
      
        public AuthProxy()
        {
              
        }

        public void Init(HttpClient http, string baseurl, string token)
        {
            this.InitializeAPI(http,baseurl + "/auth/",token);
            
        }
      

        public async Task<APIResponse<List<LocalizationTextResult>?>> ListLocalizationTexts()
        {
            APIResponse<List<LocalizationTextResult>?> ret = null;

            ret = 
                await this.GetAsJSON<List<LocalizationTextResult>?>("listlocalizationtexts", null);                        

            return ret;
        }

        public async Task<APIResponse<UserEntry?>> Registrar(NewUser data)
        {
            APIResponse<UserEntry?> ret = null;
            
            ret = await this.PostAsJSON<UserEntry?>("registraruser", JsonConvert.SerializeObject(data),null);
                       
            return ret;
        }

        public async Task EnviarEmailConfirmacao(EmailConfirmation data)
        {
            object ret = null;
                                
            ret = await this.PostAsJSON<object>("sendemailconfirmation", JsonConvert.SerializeObject(data), null);            
            
        }

        public async Task<APIResponse<UserAuthenticated?>> Login(UserLogin data)
        {
            APIResponse<UserAuthenticated?> ret = null;

            string jsoncontent = JsonConvert.SerializeObject(data);

            ret = await this.PostAsJSON<UserAuthenticated?>("login",jsoncontent , null);

            return ret;
        }

		public async Task<APIResponse<UserAuthenticated?>> RefreshLogin(AuthTokenModel data)
		{
			APIResponse<UserAuthenticated?> ret = null;

			string jsoncontent = JsonConvert.SerializeObject(data);

			ret = await this.PostAsJSON<UserAuthenticated?>("refreshlogin", jsoncontent, null);

			return ret;
		}

		public async Task<APIResponse<bool>> RecoveryPassword(string email)
        {
            APIResponse<bool> ret = null;

            ChangeUserPassword param = new ChangeUserPassword();
            param.Email = email; 
                       
            ret = await this.PostAsJSON<bool>("recoverypassword", JsonConvert.SerializeObject(param), null);
            
            return ret ;
        }

        public async Task<APIResponse<bool>> RequestActiveAccountCode(string email)
        {
            APIResponse<bool> ret = null;

            ActiveUserAccount param = new ActiveUserAccount();
            param.Email = email;            

            ret = await this.PostAsJSON<bool>("requestactiveaccountcode", JsonConvert.SerializeObject(param), null);

            return ret;
        }

        public async Task<APIResponse<bool>> ActiveAccount(ActiveUserAccount param)
        {
            APIResponse<bool> ret = null;

            ret = await this.PostAsJSON<bool>("activeaccount", JsonConvert.SerializeObject(param), null);

            return ret;
        }

        public async Task<APIResponse<bool>> RequestChangePasswordCode(string email)
        {
            APIResponse<bool> ret = null;

            this.IsAuthenticated = true; 

            ChangeUserPassword param = new ChangeUserPassword();
            param.Email = email;            

            ret = await this.PostAsJSON<bool>("requestchangepasswordcode", JsonConvert.SerializeObject(param), null);

            return ret;
        }

        public async Task<APIResponse<bool>> ChangePassword(ChangeUserPassword param)
        {
            APIResponse<bool> ret = null;
            this.IsAuthenticated = true;           

            ret = await this.PostAsJSON<bool>("changepassword", JsonConvert.SerializeObject(param), null);

            return ret;
        }

        public async Task<APIResponse<bool>> ChangeUserImage(byte[] data)
        {
            APIResponse<bool> ret = null;
            this.IsAuthenticated = true;
           
            ret = await this.PostAsStream<bool>("changeuserimageprofile",data, null);

            return ret;
        }

        public async Task<APIResponse<bool>> ChangeUserLanguage(ChangeUserLanguage param)
        {
            APIResponse<bool> ret = null;
            this.IsAuthenticated = true;

            ret = await this.PostAsJSON<bool>("changeuserlanguage", 
                JsonConvert.SerializeObject(param), null);

            return ret;
        }

        public async Task Logout()
        {
			APIResponse<bool> ret = null;

			try
            {
				
                this.IsAuthenticated = true;
				 ret = await this.GetAsJSON<bool>("logout", null);

			}
            catch(Exception ex)
            {
                var e = ex.Message; 
            }		

		}

    }
}
