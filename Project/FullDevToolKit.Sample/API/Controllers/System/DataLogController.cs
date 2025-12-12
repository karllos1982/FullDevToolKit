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
    public class DataLogController : APIControllerBase
    {
        public DataLogController(IContext context)             
        {
            Init(context, "SYSDATALOG");
        }
      

        [HttpPost]
        [Route("search")]        
        public async Task<object> Search(DataLogParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                PagedList<DataLogResult> data
                    = await Manager.IdentityModule.Domainset.DataLog.Search(param);
                ret = SetReturn(data);
            });
           
            return ret;
        }

        [HttpPost]
        [Route("list")]        
        public async Task<object> List(DataLogParam param)
        {
            await ExecuteForRead(param, async (param) =>
            {
                List<DataLogList> data
                    = await Manager.IdentityModule.Domainset.DataLog.List(param);
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
                DataLogResult data
                    = await Manager.IdentityModule
                        .Domainset.DataLog.Get(new DataLogParam() { pDataLogID = Int64.Parse(id) });
                ret = SetReturn(data);
            });


            return ret;
        }

        [HttpGet]
        [Route("gettimeline")]       
        public async Task<object> GetTimeLine(string id)
        {
            await ExecuteForRead(id, async (param) =>
            {
                List<DataLogTimelineModel> data
                    = await Manager.IdentityModule.Domainset.DataLog.GetTimeLine(Int64.Parse(id));
                ret = SetReturn(data);
            });               

            return ret;
        }


        [HttpGet]
        [Route("gettablelist")]        
        public async Task<object> GetTableList()
        {

            BeginManager();
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                List<TabelasValueModel> data = null;
                data = await Manager.IdentityModule.Domainset.DataLog.GetTableList();
                ret = SetReturn<List<TabelasValueModel>>(data);

            }
            else
            {
                ret = SetReturn<List<TabelasValueModel>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();          

            return ret;
        }
        

    }
}
