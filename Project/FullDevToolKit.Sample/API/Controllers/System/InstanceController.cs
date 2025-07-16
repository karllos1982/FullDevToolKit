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
            await ExecuteForRead(param, async (param) =>
            {
                List<InstanceResult> data
                    = await Manager.IdentityModule.Domainset.Instance.Search(param);
                ret = SetReturn(data);
            });
          
            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(InstanceParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<InstanceList> data
                    = await Manager.IdentityModule.Domainset.Instance.List(param);
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
                InstanceResult data
                    = await Manager.IdentityModule
                          .Domainset.Instance.Get(new InstanceParam() { pInstanceID = Int64.Parse(id) });
                ret = SetReturn(data);
            });
                  
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(InstanceEntry param)
        {
            await ExecuteForSave(param, async (param) =>
            {
                InstanceEntry data
                    = await Manager.IdentityModule
                         .Domainset.Instance.Set(param, this.UserID);
                ret = SetReturn(data);
            });
                
            return ret;
        }

    }
}
