using Microsoft.AspNetCore.Mvc;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Common;
using MyApp.API;
using Microsoft.AspNetCore.Authorization;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Managers;
using FullDevToolKit.Sys.Models.Identity;


namespace MyApp.Controllers
{
    [Route("SysManager/[controller]")]
    [ApiController]
    [Authorize]
    public class DataLogController : APIControllerBase
    {
        public DataLogController(IContext context,
             IContextBuilder contextbuilder)
        {
            Init(context, contextbuilder, "SYSDATALOG");
        }
      

        [HttpPost]
        [Route("search")]        
        public async Task<object> Search(DataLogParam param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                List<DataLogResult> data = null;
                data = await Manager.IdentityModule.Domainset.DataLog.Search(param);
                ret = SetReturn<List<DataLogResult>>(data);

            }
            else
            {
                ret = SetReturn<List<DataLogResult>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }

        [HttpPost]
        [Route("list")]        
        public async Task<object> List(DataLogParam param)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, true);

            if (IsAllowed)
            {
                List<DataLogList> data = null;
                data = await Manager.IdentityModule.Domainset.DataLog.List(param);
                ret = SetReturn<List<DataLogList>>(data);

            }
            else
            {
                ret = SetReturn<List<DataLogList>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();            

            return ret;
        }

        [HttpGet]
        [Route("get")]        
        public async Task<object> Get(string id)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                DataLogResult data = null;
                data = await Manager.IdentityModule
                        .Domainset.DataLog.Get(new DataLogParam() { pDataLogID = Int64.Parse(id) });

                ret = SetReturn<DataLogResult>(data);

            }
            else
            {
                ret = SetReturn<DataLogResult>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();

            return ret;
        }

        [HttpGet]
        [Route("gettimeline")]       
        public async Task<object> GetTimeLine(string id)
        {
            CheckPermission(PERMISSION_CHECK_ENUM.READ, false);

            if (IsAllowed)
            {
                List<DataLogTimelineModel> data = null;
                data = await Manager.IdentityModule.Domainset.DataLog.GetTimeLine(Int64.Parse(id));
                ret = SetReturn<List<DataLogTimelineModel>>(data);

            }
            else
            {
                ret = SetReturn<List<DataLogTimelineModel>>(PERMISSION_CHECK_ENUM.READ);
            }

            FinalizeManager();          

            return ret;
        }


        [HttpGet]
        [Route("gettablelist")]        
        public async Task<object> GetTableList()
        {
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
