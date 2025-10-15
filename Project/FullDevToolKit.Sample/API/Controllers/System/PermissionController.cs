using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;
using FullDevToolKit.Core.Helpers;
using FullDevToolKit.Sys.Models.Common;


namespace MyApp.Controllers
{
    [Route("system/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionController : APIControllerBase
    {

        public PermissionController(IContext context)
        {
            Init(context, "SYSPERMISSION");
        }

        
        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(PermissionParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<PermissionResult> data
                    = await Manager.IdentityModule.Domainset.Permission.Search(param);
                ret = SetReturn(data);
            });                 

            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(PermissionParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<PermissionList> data
                    = await Manager.IdentityModule.Domainset.Permission.List(param);
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
                PermissionResult data
                    = await Manager.IdentityModule
                         .Domainset.Permission.Get(new PermissionParam() { pPermissionID = Int64.Parse(id) });
                ret = SetReturn(data);
            });
                     
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(PermissionEntry param)
        {
            await ExecuteForSave(param, async (param) =>
            {
                PermissionEntry data
                    = await Manager.IdentityModule
                        .Domainset.Permission.Set(param, this.UserID);
                ret = SetReturn(data);
            });
          
            return ret;
        }

        [HttpPost]
        [Route("remove")]
        [Authorize]
        public async Task<object> Remove(PermissionEntry param)
        {
            await ExecuteForDelete(param, async (param) =>
            {
                PermissionEntry data
                    = await Manager.IdentityModule
                        .Domainset.Permission.Remove(param, this.UserID);
                ret = SetReturn(data);
            });
                     
            return ret;
        }

    }
}
