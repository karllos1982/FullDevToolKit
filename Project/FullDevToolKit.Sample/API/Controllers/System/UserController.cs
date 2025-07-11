using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;


namespace MyApp.Controllers
{

    [Route("system/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : APIControllerBase
    {

        public UserController(IContext context)
        {
            Init(context, "SYSUSER");                    
        }


        [HttpPost]
        [Route("search")]        
        public async Task<object> Search(UserParam param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                List<UserResult> data = null;
                data = await Manager.IdentityModule.Domainset.User.Search(param);
                ret = SetReturn<List<UserResult>>(data);

            }
            else
            {
                ret = SetReturn<List<UserResult>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }

        [HttpPost]
        [Route("list")]
        public async Task<object> List(UserParam param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            if (IsAllowed)
            {
                List<UserList> data = null;
                data = await Manager.IdentityModule.Domainset.User.List(param);
                ret = SetReturn<List<UserList>>(data);

            }
            else
            {
                ret = SetReturn<List<UserList>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();           

            return ret;
        }

        [HttpGet]
        [Route("get")]        
        public async Task<object> Get(string id)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                UserResult data = null;
                data = await Manager.IdentityModule
                        .Domainset.User.Get(new UserParam() { pUserID = Int64.Parse(id) });

                if (data != null)
                {
                    string img = data.ProfileImage;
                    if (img == "") { img = "user_anonymous.png"; }
                    data.ProfileImageURL =
                        Context.Settings.SiteURL + "auth/GetUserImageProfile?file=" + img;
                    
                }

                ret = SetReturn<UserResult>(data);

            }
            else
            {
                ret = SetReturn<UserResult>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }

        [HttpPost]
        [Route("set")]        
        public async Task<object> Set(UserEntry param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.SAVE, false);

            if (IsAllowed)
            {
                UserEntry data = null;
                data = await Manager.IdentityModule
                    .Domainset.User.Set(param, this.UserID);
                ret = SetReturn<UserEntry>(data);

            }
            else
            {
                ret = SetReturn<UserEntry>(PERMISSION_CHECK_ENUM.SAVE);
            }

            FinalizeManager();

            return ret;        
        }

        [HttpPost]
        [Route("createnewuser")]        
        public async Task<object> CreateNewUser(NewUser param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.SAVE, false);

            if (IsAllowed)
            {
                UserEntry data = null;
                data = await Manager.IdentityModule.CreateNewUser(param, false, null);                
                ret = SetReturn<UserEntry>(data);

            }
            else
            {
                ret = SetReturn<UserEntry>(PERMISSION_CHECK_ENUM.SAVE);
            }

            FinalizeManager();
         
            return ret;
        }


        [HttpPost]
        [Route("changestate")]        
        public async Task<object> ChangeState(UserChangeState param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.SAVE, false);

            if (IsAllowed)
            {                
                await Manager.IdentityModule.Domainset.User.ChangeState(param); 
                ret = SetReturn<bool>(true);

            }
            else
            {
                ret = SetReturn<bool>(PERMISSION_CHECK_ENUM.SAVE);
            }

            FinalizeManager();          

            return ret;
        }

     
    }
}
