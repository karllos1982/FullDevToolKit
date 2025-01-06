using Microsoft.JSInterop;
using FullDevToolKit.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Models.Identity;
using MyApp.Proxys;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.X86;
using FullDevToolKit.Core;


namespace MyApp.ServerCode
{
 
    public class MyAppSettings : IAppSettings
    {

        private IConfiguration _env;

        public MyAppSettings()
        {

        }

        public MyAppSettings(IConfiguration webhost)
        {
            _env = webhost;
            LoadSettings();
        }

        public string SiteURL { get; set; }

        public string ServiceURL { get; set; }

        public string NomeSistema { get; set; }

        public string SessionTimeOut { get; set; }

        public string DefaultLanguage { get; set; }


        public string FileContentMenu { get; set; }

        public List<MenuItemConfigs> ContentMenu { get; set; }

        public void LoadSettings(HttpClient http = null)
        {

            this.SiteURL = _env["SiteURL"];
            this.ServiceURL = _env["ServiceURL"];
            this.NomeSistema = _env["NomeSistema"];
            this.DefaultLanguage = _env["DefaultLanguage"];
            this.SessionTimeOut = _env["SessionTimeOut"];
            this.FileContentMenu = _env["FileContentMenu"];

            string jcontent = _env["ContentMenu"];
			
			if (jcontent != null) {

                ContentMenu = JsonConvert.DeserializeObject<List<MenuItemConfigs>>(jcontent);
            }
            

        }
    }

    public class MyAppController : IAppControllerAsync<UserAuthenticated>
    {
        public UserAuthenticated UserInfo { get; set; }

        private bool _IsAuthenticated = false;

        private Cookies _cookies;
        public Cookies AppCookies
        {
            get
            {
                return _cookies;
            }
        }

        private LocalStorage _localStorage;

        public LocalStorage AppLocalStorage
        {
            get
            {
                return _localStorage;
            }
        }


        private IJSRuntime _jscontext;
        public IJSRuntime JSContext
        {
            get
            {
                return _jscontext;
            }
        }

        private IConfiguration _webhost;
        public IConfiguration WebHost
        {
            get
            {
                return _webhost;
            }
        }

        private IAppSettings _settings;
        public IAppSettings Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }

        public MyAppController(IConfiguration webhost, IJSRuntime jscontext)
        {
            _webhost = webhost;
            _jscontext = jscontext;
            _cookies = new Cookies(jscontext);
            _localStorage = new LocalStorage(jscontext);

        }

        public async Task<bool> CheckSession()
        {
            bool ret = false;

            await GetSession();

            if (UserInfo != null)
            {
                ret = true;
            }

            return ret;
        }

        public async Task ClearSession()
        {
            await _cookies.ClearUserInfo();
        }

        public async Task<ExecutionStatus> CreateSession(UserAuthenticated user)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            DateTime expires = DateTime.Now.AddMinutes(int.Parse(_settings.SessionTimeOut));
            await _cookies.SetUserInfo(user, expires);

            return ret;
        }

        public async Task ReplaceUserInfo(UserAuthenticated user)
        {
            await _cookies.ClearUserInfo();
            DateTime expires = DateTime.Now.AddMinutes(int.Parse(_settings.SessionTimeOut));
            await _cookies.SetUserInfo(UserInfo, expires);

        }

        public async Task GetSession()
        {
            UserAuthenticated ticket = null;

            ticket = await _cookies.GetUserInfo();

            UserInfo = ticket;
        }

        public async Task<bool> IsAuthenticated()
        {
            bool ret = _IsAuthenticated;

            if (!_IsAuthenticated)
            {
                ret = await CheckSession();
            }

            return ret;
        }

        public async Task<ExecutionStatus> Login(IAuthProxyManager apiproxy, UserLogin user)
        {
            ExecutionStatus ret = new ExecutionStatus(true);
            UserAuthenticated usr = null;

            user.SessionTimeOut = _settings.SessionTimeOut;
            
            APIResponse<UserAuthenticated?> response
                 = await ((AuthProxy)apiproxy).Login(user);

            if (response.IsSuccess)
            {
                usr = response.Data; 
                DateTime expires = DateTime.Now.AddMinutes(int.Parse(_settings.SessionTimeOut));
                await _localStorage.SetUserPermissions(usr.Permissions, usr.Token);
             
                // criando a sessão do usuario no navegador
                usr.Permissions = null;
                await this.CreateSession(usr);

                ret.Returns = usr;
            }
            else
            {
                ret.Exceptions = response.Exceptions;
                ret.Success = false;
            }

            return ret;
        }

        public async Task Logout()
        {
            await _cookies.ClearUserInfo();
            await _cookies.ClearUserPermissions();
            await _cookies.ClearAllCookies();
            await _localStorage.ClearUserPermissions();
        }

        public PermissionsState CheckPermissions(UserAuthenticated user,
            string objectcode, bool allownone)
        {
            PermissionsState ret = new PermissionsState(false, false, false);

            List<UserPermissions> permissions = user.Permissions;

            ret =
                Utilities.GetPermissionsState(permissions, objectcode, allownone);

            return ret;
        }

        public async Task<List<UserPermissions>> GetUserPermissions(string token)
        {
            List<UserPermissions> ret = new List<UserPermissions>();

            ret = await _localStorage.GetUserPermissions(token);

            return ret;

        }

        public async Task SetPageTitle(string title)
        {
            await _cookies.SetPageTitle(title);
        }

        public async Task<string> GetPageTitle()
        {
            return await _cookies.GetPageTitle();
        }

		private Dictionary<string, MenuItemConfigs> _menuconfigs { get; set; }

			
	}


}