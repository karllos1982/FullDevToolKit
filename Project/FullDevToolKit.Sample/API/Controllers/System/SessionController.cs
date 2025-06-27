using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Identity ;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;


namespace MyApp.Controllers
{
    [Route("system/[controller]")]
    [ApiController]
    [Authorize]
    public class SessionLogController : APIControllerBase
    {

        public SessionLogController(IContext context,
                IContextBuilder contextbuilder)
        {
            Init(context, contextbuilder, "SYSSESSION");
        }
      

        [HttpPost]
        [Route("search")]        
        public async Task<object> Search(SessionLogParam param)
        {

            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                List<SessionLogResult> data = null;
                data = await Manager.IdentityModule.Domainset.SessionLog.Search(param);
                ret = SetReturn<List<SessionLogResult>>(data);

            }
            else
            {
                ret = SetReturn<List<SessionLogResult>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }

        [HttpPost]
        [Route("list")]        
        public async Task<object> List(SessionLogParam param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            if (IsAllowed)
            {
                List<SessionLogList> data = null;
                data = await Manager.IdentityModule.Domainset.SessionLog.List(param);
                ret = SetReturn<List<SessionLogList>>(data);

            }
            else
            {
                ret = SetReturn<List<SessionLogList>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("get")]        
        public async Task<object> Get(string id)
        {
            CheckPermission( PERMISSION_CHECK_ENUM.READ,false);

            if (IsAllowed)
            {
                SessionLogResult data = null;
                data = await Manager.IdentityModule
                        .Domainset.SessionLog.Get(new SessionLogParam() { pUserID = Int64.Parse(id) });                

                ret = SetReturn<SessionLogResult>(data);

            }
            else
            {
                ret = SetReturn<SessionLogResult>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }
      

    }
}
