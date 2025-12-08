using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Identity ;
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
    public class SessionLogController : APIControllerBase
    {

        public SessionLogController(IContext context)
        {
            Init(context, "SYSSESSION");
        }
      

        [HttpPost]
        [Route("search")]        
        public async Task<object> Search(SessionLogParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                PagedList<SessionLogResult> data
                    = await Manager.IdentityModule.Domainset.SessionLog.Search(param);
                ret = SetReturn(data);
            });
         
            return ret;
        }

        [HttpPost]
        [Route("list")]        
        public async Task<object> List(SessionLogParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<SessionLogList> data
                    = await Manager.IdentityModule.Domainset.SessionLog.List(param);
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
                SessionLogResult data
                    = await Manager.IdentityModule
                         .Domainset.SessionLog.Get(new SessionLogParam() { pUserID = Int64.Parse(id) });
                ret = SetReturn(data);
            });           

            return ret;
        }
      

    }
}
