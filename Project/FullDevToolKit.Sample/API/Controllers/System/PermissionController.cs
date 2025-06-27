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
    public class PermissionController : APIControllerBase
    {

        public PermissionController(IContext context,
               IContextBuilder contextbuilder)
        {
            Init(context, contextbuilder, "SYSPERMISSION");
        }

        
        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(PermissionParam param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                List<PermissionResult> data = null;
                data = await Manager.IdentityModule.Domainset.Permission.Search(param);
                ret = SetReturn<List<PermissionResult>>(data);

            }
            else
            {
                ret = SetReturn<List<PermissionResult>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();          

            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(PermissionParam param)
        {
           
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            if (IsAllowed)
            {
                List<PermissionList> data = null;
                data = await Manager.IdentityModule.Domainset.Permission.List(param);
                ret = SetReturn<List<PermissionList>>(data);

            }
            else
            {
                ret = SetReturn<List<PermissionList>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

          
            return ret;
        }

        [HttpGet]
        [Route("get")]
        [Authorize]
        public async Task<object> Get(string id)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                PermissionResult data = null;
                data = await Manager.IdentityModule
                        .Domainset.Permission.Get(new PermissionParam()
                        { pPermissionID = Int64.Parse(id) });

                ret = SetReturn<PermissionResult>(data);

            }
            else
            {
                ret = SetReturn<PermissionResult>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

           
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(PermissionEntry param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.SAVE, false);

            if (IsAllowed)
            {
                PermissionEntry data = null;
                data = await Manager.IdentityModule
                    .Domainset.Permission.Set(param, this.UserID);
                ret = SetReturn<PermissionEntry>(data);

            }
            else
            {
                ret = SetReturn<PermissionEntry>(PERMISSION_CHECK_ENUM.SAVE);
            }

            FinalizeManager();

            return ret;
        }

        [HttpPost]
        [Route("delete")]
        [Authorize]
        public async Task<object> Delete(PermissionEntry param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.DELETE, false);

            if (IsAllowed)
            {
                PermissionEntry data = null;
                data = await Manager.IdentityModule
                    .Domainset.Permission.Delete(param, this.UserID);
                ret = SetReturn<PermissionEntry>(data);

            }
            else
            {
                ret = SetReturn<PermissionEntry>(PERMISSION_CHECK_ENUM.DELETE);
            }

            FinalizeManager();
          
            return ret;
        }

    }
}
