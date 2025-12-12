using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Data.QueryBuilders;
using FullDevToolKit.Core.Common;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class DataLogRepository : IDataLogRepository        
    {
       
        public DataLogRepository(IContext context)
        {
            Context = context;
            TableName = "sysDataLog"; 
            PKFieldName = "DataLogID";
        }
         
        private DataLogQueryBuilder query = new DataLogQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(DataLogEntry model)
        {
            
            string sql = query.QueryForCreate(TableName, model,model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task<DataLogResult> ReadObject(DataLogParam param)
        {
            DataLogResult ret = null;
            
            string sql = query.QueryForGet(null);

            ret = await Context.ExecuteQueryFirstAsync<DataLogResult>(sql, param); 
                 
            return ret;
        }

        public async Task Update(DataLogEntry model)
        {            
            string sql = query.QueryForUpdate(TableName, model, model);
              await Context.ExecuteAsync(sql, model);
         
        }

        public async Task Delete(DataLogEntry model)
        {
            
            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

            
        }

        public async Task<List<DataLogList>> ReadList(DataLogParam param)
        {
            List<DataLogList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<DataLogList>(query.QueryForList(param),param); 
                 
            return ret;
        }

        public async Task<PagedList<DataLogResult>> ReadSearch(DataLogParam param)
        {
            PagedList<DataLogResult> ret = new PagedList<DataLogResult>()
            { RecordList = new List<DataLogResult>() };

            List<DataLogResult> recordlist = null;
            List<PaginationModel> paglist = null;
            int index = 1;
            
            paglist = await Context
            .ExecuteQueryToListAsync<PaginationModel>(query.QueryForPaginationSettings(param), param);

            if (paglist.Count > 0)
            {

                PaginationSettings paginationSettings
                    = query.GetPaginationSettings(paglist,
                    BaseParam.CalcPageCount(param.RecordsPerPage, paglist.Count), param.RecordsPerPage);

                if (param.PageIndex > 0)
                {
                    index = param.PageIndex;
                }
               
                param.Pagination = paginationSettings.GetItem(index);
                recordlist = await Context
                .ExecuteQueryToListAsync<DataLogResult>(query.QueryForSearch(param), param);

                ret.PageCount = paginationSettings.PageCount;
                ret.TotalRecords = paglist.Count;
                ret.RecordList = recordlist;
                ret.RecordCount = recordlist.Count;
            }

            return ret;
        }

        public async Task<List<DataLogTimelineModel>> GetDataLogTimeline(Int64 recordID)
        {
            List<DataLogTimelineModel> ret = null;
            DataLogParam param = new DataLogParam() { pID = recordID };

            ret = await Context
                .ExecuteQueryToListAsync<DataLogTimelineModel>(query.QueryForGetTimeLine(), param);

            return ret;
        }

        public async Task<List<TabelasValueModel>> GetTableList()
        {
            List<TabelasValueModel> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<TabelasValueModel>(query.QueryForGetTableList(),null);
                  

            return ret;
        }

    }

}
