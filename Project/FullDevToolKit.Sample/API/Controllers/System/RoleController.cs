using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Common;


namespace MyApp.Controllers
{
    [Route("system/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : APIControllerBase
    {

        public RoleController(IContext context)
        {
            Init(context, "SYSROLE");
        }
      

        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(RoleParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<RoleResult> data
                    = await Manager.IdentityModule.Domainset.Role.Search(param);
                ret = SetReturn(data);
            });          
          
            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(RoleParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<RoleList> data
                    = await Manager.IdentityModule.Domainset.Role.List(param);
                ret = SetReturn(data);
            });
           
            return ret;
        }

        [HttpGet]
        [Route("get")]
        [Authorize]
        public async Task<object> Get(string id)
        {
            await ExecuteForRead(id, async (param) =>
            {
                RoleResult data
                    = await Manager.IdentityModule
                         .Domainset.Role.Get(new RoleParam() { pRoleID = Int64.Parse(id) });
                ret = SetReturn(data);
            });
                      
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(RoleEntry param)
        {
            await ExecuteForSave(param, async (param) =>
            {
                RoleEntry data
                    = await Manager.IdentityModule
                        .Domainset.Role.Set(param, this.UserID);
                ret = SetReturn(data);
            });
                 
            return ret;
        }

    }
}
