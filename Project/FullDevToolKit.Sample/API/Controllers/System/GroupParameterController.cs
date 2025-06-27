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
    public class GroupParameterController : APIControllerBase
    {

        public GroupParameterController(IContext context,
             IContextBuilder contextbuilder)
        {
            Init(context, contextbuilder, "SYSGROUPPARAMETER");
        }
      

        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(GroupParameterParam param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                List<GroupParameterResult> data = null;
                data = await Manager.IdentityModule.Domainset.GroupParameter.Search(param);
                ret = SetReturn<List<GroupParameterResult>>(data);

            }
            else
            {
                ret = SetReturn<List<GroupParameterResult>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

          
            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(GroupParameterParam param)
        {
           
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            if (IsAllowed)
            {
                List<GroupParameterList> data = null;
                data = await Manager.IdentityModule.Domainset.GroupParameter.List(param);
                ret = SetReturn<List<GroupParameterList>>(data);

            }
            else
            {
                ret = SetReturn<List<GroupParameterList>>(PERMISSION_CHECK_ENUM.READ);
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
                GroupParameterResult data = null;
                data = await Manager.IdentityModule
                        .Domainset.GroupParameter.Get(new GroupParameterParam() { pGroupParameterID = Int64.Parse(id) });
            
                ret = SetReturn<GroupParameterResult>(data);
            }
            else
            {
                ret = SetReturn<GroupParameterResult>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();
            
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(GroupParameterEntry param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.SAVE, false);

            if (IsAllowed)
            {
                GroupParameterEntry data = null;
                data = await Manager.IdentityModule
                    .Domainset.GroupParameter.Set(param, this.UserID);
                ret = SetReturn<GroupParameterEntry>(data);

            }
            else
            {
                ret = SetReturn<GroupParameterEntry>(PERMISSION_CHECK_ENUM.SAVE);
            }

            FinalizeManager();
      
            return ret;
        }

    }
}
