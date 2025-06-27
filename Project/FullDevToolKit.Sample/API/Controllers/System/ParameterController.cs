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

        public ParameterController(IContext context,
             IContextBuilder contextbuilder)
        {
            Init(context, contextbuilder, "SYSPARAMETER");
        }
      

        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(ParameterParam param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                List<ParameterResult> data = null;
                data = await Manager.IdentityModule.Domainset.Parameter.Search(param);
                ret = SetReturn<List<ParameterResult>>(data);

            }
            else
            {
                ret = SetReturn<List<ParameterResult>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

          
            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(ParameterParam param)
        {
           
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            if (IsAllowed)
            {
                List<ParameterList> data = null;
                data = await Manager.IdentityModule.Domainset.Parameter.List(param);
                ret = SetReturn<List<ParameterList>>(data);

            }
            else
            {
                ret = SetReturn<List<ParameterList>>(PERMISSION_CHECK_ENUM.READ);
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
                ParameterResult data = null;
                data = await Manager.IdentityModule
                        .Domainset.Parameter.Get(new ParameterParam() { pParameterID = Int64.Parse(id) });
            
                ret = SetReturn<ParameterResult>(data);
            }
            else
            {
                ret = SetReturn<ParameterResult>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();
            
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(ParameterEntry param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.SAVE, false);

            if (IsAllowed)
            {
                ParameterEntry data = null;
                data = await Manager.IdentityModule
                    .Domainset.Parameter.Set(param, this.UserID);
                ret = SetReturn<ParameterEntry>(data);

            }
            else
            {
                ret = SetReturn<ParameterEntry>(PERMISSION_CHECK_ENUM.SAVE);
            }

            FinalizeManager();
      
            return ret;
        }

    }
}
