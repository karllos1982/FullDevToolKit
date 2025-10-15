using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Data.QueryBuilders;

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
             
        public async Task<List<DataLogResult>> ReadSearch(DataLogParam param)
        {
            List<DataLogResult> ret = null;

            ret = await  Context
                .ExecuteQueryToListAsync<DataLogResult>(query.QueryForSearch(param), param);
                

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
