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
    public class ObjectPermissionController : APIControllerBase
    {

        public ObjectPermissionController(IContext context,
              IContextBuilder contextbuilder)
        {
            Init(context, contextbuilder, "SYSOBJECTPERMISSION");
        }
       

        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(ObjectPermissionParam param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                List<ObjectPermissionResult> data = null;
                data = await Manager.IdentityModule.Domainset.ObjectPermission.Search(param);
                ret = SetReturn<List<ObjectPermissionResult>>(data);

            }
            else
            {
                ret = SetReturn<List<ObjectPermissionResult>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();           

            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(ObjectPermissionParam param)
        {
           
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            if (IsAllowed)
            {
                List<ObjectPermissionList> data = null;
                data = await Manager.IdentityModule.Domainset.ObjectPermission.List(param);
                ret = SetReturn<List<ObjectPermissionList>>(data);

            }
            else
            {
                ret = SetReturn<List<ObjectPermissionList>>(PERMISSION_CHECK_ENUM.READ);
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
                ObjectPermissionResult data = null;
                data = await Manager.IdentityModule
                        .Domainset.ObjectPermission.Get(new ObjectPermissionParam() { pObjectPermissionID = Int64.Parse(id) });

                ret = SetReturn<ObjectPermissionResult>(data);

            }
            else
            {
                ret = SetReturn<ObjectPermissionResult>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

           
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(ObjectPermissionEntry param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.SAVE, false);

            if (IsAllowed)
            {
                ObjectPermissionEntry data = null;
                data = await Manager.IdentityModule
                    .Domainset.ObjectPermission.Set(param, this.UserID);
                ret = SetReturn<ObjectPermissionEntry>(data);

            }
            else
            {
                ret = SetReturn<ObjectPermissionEntry>(PERMISSION_CHECK_ENUM.SAVE);
            }

            FinalizeManager();          

            return ret;
        }

    }
}
