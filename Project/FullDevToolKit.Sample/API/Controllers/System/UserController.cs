using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Core.Common;


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
            await ExecuteForRead(param, async (param) =>
            {
                PagedList<UserResult> data
                    = await Manager.IdentityModule.Domainset.User.Search(param);
                ret = SetReturn(data);
            });           

            return ret;
        }

        [HttpPost]
        [Route("list")]
        public async Task<object> List(UserParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<UserList> data
                    = await Manager.IdentityModule.Domainset.User.List(param);
                ret = SetReturn(data);
            });                    

            return ret;
        }

        [HttpGet]
        [Route("get")]        
        public async Task<object> Get(string id)
        {
            await ExecuteForRead(id, async (param) =>
            {
                UserResult data
                    = await Manager.IdentityModule
                       .Domainset.User.Get(new UserParam() { pUserID = Int64.Parse(id) });
                if (data != null)
                {
                    string img = data.ProfileImage;
                    if (img == "") { img = "user_anonymous.png"; }
                    data.ProfileImageURL =
                        Context.Settings.SiteURL + "auth/GetUserImageProfile?file=" + img;
                }
                ret = SetReturn(data);
            });
       
            return ret;
        }


        [HttpPost]
        [Route("set")]        
        public async Task<object> Set(UserEntry param)
        {
            await ExecuteForSave(param, async (param) =>
            {
                UserEntry data
                    = await Manager.IdentityModule
                        .Domainset.User.Set(param, this.UserID);
                ret = SetReturn(data);
            });
         
            return ret;        
        }

        [HttpPost]
        [Route("createnewuser")]        
        public async Task<object> CreateNewUser(NewUser param)
        {
            await ExecuteForSave(param, async (param) =>
            {
                UserEntry data
                    = await Manager.IdentityModule.CreateNewUser(param, false, this.UserID);
                ret = SetReturn(data);
            });
                     
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
