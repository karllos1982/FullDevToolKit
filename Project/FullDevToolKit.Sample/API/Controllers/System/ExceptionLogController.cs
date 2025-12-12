using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;
using FullDevToolKit.Core.Common;


namespace MyApp.Controllers
{
    [Route("system/[controller]")]
    [ApiController]
    [Authorize]
    public class ExceptionLogController : APIControllerBase
    {
        public ExceptionLogController(IContext context)             
        {
            Init(context, "SYSEXCEPTIONLOG"); 

		}


        [HttpPost]
        [Route("search")]
        public async Task<object> Search(ExceptionLogParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                PagedList<ExceptionLogResult> data
                    = await Manager.IdentityModule.Domainset.ExceptionLog.Search(param);
                ret = SetReturn(data);
            });
          
            return ret;
        }

        [HttpPost]
        [Route("list")]
        public async Task<object> List(ExceptionLogParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<ExceptionLogList> data
                    = await Manager.IdentityModule.Domainset.ExceptionLog.List(param);
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
                ExceptionLogResult data
                    = await Manager.IdentityModule
                        .Domainset.ExceptionLog.Get(new ExceptionLogParam() { pExceptionLogID = Int64.Parse(id) });
                ret = SetReturn(data);
            });
         
            return ret;
        }
    
    }
}
