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
    public class ObjectPermissionController : APIControllerBase
    {

        public ObjectPermissionController(IContext context)
        {
            Init(context, "SYSOBJECTPERMISSION");
        }
       

        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(ObjectPermissionParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<ObjectPermissionResult> data
                    = await Manager.IdentityModule.Domainset.ObjectPermission.Search(param);
                ret = SetReturn(data);
            });
                 
            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(ObjectPermissionParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<ObjectPermissionList> data
                    = await Manager.IdentityModule.Domainset.ObjectPermission.List(param);
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
                ObjectPermissionResult data
                    = await Manager.IdentityModule
                         .Domainset.ObjectPermission.Get(new ObjectPermissionParam() { pObjectPermissionID = Int64.Parse(id) });
                ret = SetReturn(data);
            });       
           
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(ObjectPermissionEntry param)
        {
            await ExecuteForSave(param, async (param) =>
            {
                ObjectPermissionEntry data
                    = await Manager.IdentityModule
                        .Domainset.ObjectPermission.Set(param, this.UserID);
                ret = SetReturn(data);
            });                  

            return ret;
        }

    }
}
