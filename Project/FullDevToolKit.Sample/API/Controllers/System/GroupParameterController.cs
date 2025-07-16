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

        public GroupParameterController(IContext context)
        {
            Init(context, "SYSGROUPPARAMETER");
        }
      

        [HttpPost]
        [Route("search")]
        [Authorize]
        public async Task<object> Search(GroupParameterParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<GroupParameterResult> data
                    = await Manager.IdentityModule.Domainset.GroupParameter.Search(param);
                ret = SetReturn(data);
            });
          
            return ret;
        }

        [HttpPost]
        [Route("list")]
        [Authorize]
        public async Task<object> List(GroupParameterParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<GroupParameterList> data
                    = await Manager.IdentityModule.Domainset.GroupParameter.List(param);
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
                GroupParameterResult data
                    = await Manager.IdentityModule
                         .Domainset.GroupParameter.Get(new GroupParameterParam() { pGroupParameterID = Int64.Parse(id) });
                ret = SetReturn(data);
            });
                      
            return ret;
        }

        [HttpPost]
        [Route("set")]
        [Authorize]
        public async Task<object> Set(GroupParameterEntry param)
        {
            await ExecuteForSave(param, async (param) =>
            {
                GroupParameterEntry data 
                    = await Manager.IdentityModule
                        .Domainset.GroupParameter.Set(param, this.UserID);
                    ret = SetReturn(data);
            });
                  
            return ret;
        }

    }
}
