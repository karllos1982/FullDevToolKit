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
    public class InstanceController : APIControllerBase
    {
        public InstanceController(IContext context)
        {
            Init(context, "SYSINSTANCE");
        }
     

        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(InstanceParam param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                List<InstanceResult> data = null;
                data = await Manager.IdentityModule.Domainset.Instance.Search(param);
                ret = SetReturn<List<InstanceResult>>(data);

            }
            else
            {
                ret = SetReturn<List<InstanceResult>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(InstanceParam param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            if (IsAllowed)
            {
                List<InstanceList> data = null;
                data = await Manager.IdentityModule.Domainset.Instance.List(param);
                ret = SetReturn<List<InstanceList>>(data);

            }
            else
            {
                ret = SetReturn<List<InstanceList>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();
           
            return ret;
        }

        [HttpGet]
        [Route("get")]
        [Authorize]
        public async Task<object> Get(string id)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                InstanceResult data = null;
                data = await Manager.IdentityModule
                        .Domainset.Instance.Get(new InstanceParam() { pInstanceID = Int64.Parse(id) });

                ret = SetReturn<InstanceResult>(data);
            }
            else
            {
                ret = SetReturn<InstanceResult>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();
         
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(InstanceEntry param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.SAVE, false);

            if (IsAllowed)
            {
                InstanceEntry data = null;
                data = await Manager.IdentityModule
                    .Domainset.Instance.Set(param, this.UserID);
                ret = SetReturn<InstanceEntry>(data);

            }
            else
            {
                ret = SetReturn<InstanceEntry>(PERMISSION_CHECK_ENUM.SAVE);
            }

            FinalizeManager();
      
            return ret;
        }

    }
}
