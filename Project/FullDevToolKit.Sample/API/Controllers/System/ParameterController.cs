using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;


namespace MyApp.Controllers
{
    [Route("system/[controller]")]
    [ApiController]
    [Authorize]
    public class ParameterController : APIControllerBase
    {

        public ParameterController(IContext context)
        {
            Init(context, "SYSPARAMETER");
        }
      

        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(ParameterParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<ParameterResult> data
                    = await Manager.IdentityModule.Domainset.Parameter.Search(param);
                ret = SetReturn(data);
            });
                     
            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(ParameterParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<ParameterList> data
                    = await Manager.IdentityModule.Domainset.Parameter.List(param);
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
                ParameterResult data
                    = await Manager.IdentityModule
                         .Domainset.Parameter.Get(new ParameterParam() { pParameterID = Int64.Parse(id) });
                ret = SetReturn(data);
            });           
            
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(ParameterEntry param)
        {
            await ExecuteForSave(param, async (param) =>
            {
                ParameterEntry data
                    = await Manager.IdentityModule
                        .Domainset.Parameter.Set(param, this.UserID);
                ret = SetReturn(data);
            });
                
            return ret;
        }

    }
}
