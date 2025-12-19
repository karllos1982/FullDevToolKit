using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using MyApp.Proxys;
using FullDevToolKit.Helpers;
using Microsoft.JSInterop;
using Newtonsoft.Json;


namespace MyApp.ServerCode
{
    public interface IAppControllerAsync<T> where T : UserAuthenticated
    {
        Task<bool> IsAuthenticated();

        PermissionsState CheckPermissions(UserAuthenticated user,
            string objectcode, bool allownone);

        T UserInfo { get; set; }

        Task <ExecutionStatus> Login( UserLogin user);

        Task<UserAuthenticated> RefreshLogin(string email, string currenttoken);


		Task Logout();

        Task<ExecutionStatus> CreateSession(UserAuthenticated user);

        Task GetSession(IAppSettings _settings = null);

        Task ClearSession();

        Task<bool> CheckSession();

        Task ReplaceUserInfo(UserAuthenticated user);

        Task<List<UserPermissions>> GetUserPermissions(UserAuthenticated user);

        Task SetPageTitle(string title);

        Task<string> GetPageTitle();	

    }

    public interface IAppSettings
    {
        string SiteURL { get; set; }

        string ServiceURL { get; set; }

        string NomeSistema { get; set; }

        string SessionTimeOut { get; set; }

        string DefaultLanguage { get; set; }

        string FileContentMenu { get; set; }

        List<MenuItemConfigs> ContentMenu { get; set; }
        

    }



    //

    public class MenuObject
    {
        public string ID { get; set; }

        public string Role { get; set; }

        public string Title { get; set; }

        public string NavigationURL { get; set; }

        public string ClassIcon { get; set; }

        public string ClassStatus { get; set; }

        public string Description { get; set; }

        public List<MenuObject> Childs { get; set; }

    }
   
  
    public class MenuItemConfigs
    {

        public string Key { get; set; }

        public string Title { get; set; }

        public string URL { get; set; }

        public string PermissionCode { get; set; }

        public string LocalizationCode { get; set; }

        public bool State  { get; set; }


        public MenuItemConfigs()
        {
                                  
        }

    }

   

    public class MenuConfigs
    {
        public MenuConfigs()
        {
            
        }

        public List<MenuItemConfigs> ItemsMenu { get; set; }

        public List<LocalizationTextResult> Localization { get; set; }
                       

        public void SetMenuConfigs(UserAuthenticated user)
        {            

            List<LocalizationTextResult>  texts 
                = Localization.Where(t => t.LanguageID.ToString() ==user.LanguageID.ToString()).ToList();

            PermissionsState auxpermission;
            
            foreach (MenuItemConfigs i in ItemsMenu)
            {
                var text = texts.Where(t => t.Code==i.LocalizationCode).FirstOrDefault();

                if (text != null)
                {					
					i.Title = text.Text;
                }
                
                auxpermission
                        = FullDevToolKit.Helpers.Utilities.GetPermissionsState(user.Permissions, i.PermissionCode, true);

                if (auxpermission != null)
                {
                    i.State = auxpermission.AllowRead;
                }                                   
              
            }
        }
        

    }


    public class UserInfo: UserAuthenticated
    {

    }
   
    public class UserConext
    {
        public string Id { get; set; }

        public string Name { get; set; }


    }   

    public interface IMenuItemActive
    {
        void ActiveItemMenu(string itemname);

        string GetActiveItemMenu();
    }

    public class MenuItemActive : IMenuItemActive
    {
        private string _itemname = "Admin/Home";

        public void ActiveItemMenu(string itemname)
        {
            _itemname = itemname;
        }

        public string GetActiveItemMenu()
        {
            return _itemname;
        }
    }

}