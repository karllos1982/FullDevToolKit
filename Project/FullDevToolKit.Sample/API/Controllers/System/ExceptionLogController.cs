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
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                List<ExceptionLogResult> data = null;
                data = await Manager.IdentityModule.Domainset.ExceptionLog.Search(param);
                ret = SetReturn<List<ExceptionLogResult>>(data);

            }
            else
            {
                ret = SetReturn<List<ExceptionLogResult>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }

        [HttpPost]
        [Route("list")]
        public async Task<object> List(ExceptionLogParam param)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            if (IsAllowed)
            {
                List<ExceptionLogList> data = null;
                data = await Manager.IdentityModule.Domainset.ExceptionLog.List(param);
                ret = SetReturn<List<ExceptionLogList>>(data);

            }
            else
            {
                ret = SetReturn<List<ExceptionLogList>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("get")]
        public async Task<object> Get(string id)
        {
            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                ExceptionLogResult data = null;
                data = await Manager.IdentityModule
                        .Domainset.ExceptionLog.Get(new ExceptionLogParam() { pExceptionLogID = Int64.Parse(id) });

                ret = SetReturn<ExceptionLogResult>(data);

            }
            else
            {
                ret = SetReturn<ExceptionLogResult>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }
    
    }
}
