using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Data.QueryBuilders;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class SessionLogRepository : ISessionLogRepository        
    {
       
        public SessionLogRepository(IContext context)
        {
            Context = context;
            TableName = "sysSessionLog";
            PKFieldName = "SessionLogID";

        }
         
        private SessionLogQueryBuilder query = new SessionLogQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(SessionLogEntry model)
        {
            
            string sql = query.QueryForCreate(TableName, model,model);
            await Context.ExecuteAsync(sql, model);
            
        }

        public async Task<SessionLogResult> ReadObject(SessionLogParam param)
        {
            SessionLogResult ret = null;
            
            string sql = query.QueryForGet(null);

            ret = await Context
                .ExecuteQueryFirstAsync<SessionLogResult>(sql, param); 
                 
            return ret;
        }

        public async Task Update(SessionLogEntry model)
        {
            
            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
            
        }

        public async Task Delete(SessionLogEntry model)
        {
            
            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
 
        }

        public async Task<List<SessionLogList>> ReadList(SessionLogParam param)
        {
            List<SessionLogList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<SessionLogList>(query.QueryForList(param),param); 

            return ret;
        }
             
        public async Task<List<SessionLogResult>> ReadSearch(SessionLogParam param)
        {
            List<SessionLogResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<SessionLogResult>(query.QueryForSearch(param),  param);

            return ret;
        }

        public async Task<ExecutionStatus> SetDateLogout(SessionLogParam obj)
        {           
            string sql = query.QueryForSetDateLogout();
            await Context.ExecuteAsync(sql, obj);
          
            return Context.Status;
        }

    }

}
