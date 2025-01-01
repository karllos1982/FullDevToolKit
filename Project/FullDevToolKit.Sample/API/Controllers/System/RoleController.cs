using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Managers;

namespace MyApp.Controllers
{
    [Route("system/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : APIControllerBase
    {

        public RoleController(IContext context,
             IContextBuilder contextbuilder)
        {
            Init(context, contextbuilder, "SYSROLE");
        }
      

        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(RoleParam param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                List<RoleResult> data = null;
                data = await Manager.IdentityModule.Domainset.Role.Search(param);
                ret = SetReturn<List<RoleResult>>(data);

            }
            else
            {
                ret = SetReturn<List<RoleResult>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

          
            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(RoleParam param)
        {
           
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            if (IsAllowed)
            {
                List<RoleList> data = null;
                data = await Manager.IdentityModule.Domainset.Role.List(param);
                ret = SetReturn<List<RoleList>>(data);

            }
            else
            {
                ret = SetReturn<List<RoleList>>(PERMISSION_CHECK_ENUM.READ);
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
                RoleResult data = null;
                data = await Manager.IdentityModule
                        .Domainset.Role.Get(new RoleParam() { pRoleID = Int64.Parse(id) });
            
                ret = SetReturn<RoleResult>(data);
            }
            else
            {
                ret = SetReturn<RoleResult>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();
            
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(RoleEntry param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.SAVE, false);

            if (IsAllowed)
            {
                RoleEntry data = null;
                data = await Manager.IdentityModule
                    .Domainset.Role.Set(param, this.UserID);
                ret = SetReturn<RoleEntry>(data);

            }
            else
            {
                ret = SetReturn<RoleEntry>(PERMISSION_CHECK_ENUM.SAVE);
            }

            FinalizeManager();
      
            return ret;
        }

    }
}
