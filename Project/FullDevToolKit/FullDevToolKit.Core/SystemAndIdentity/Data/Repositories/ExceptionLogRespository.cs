using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Data.QueryBuilders;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class ExceptionLogRepository : IExceptionLogRepository
    {

        public ExceptionLogRepository(IContext context)
        {
            Context = context;
            TableName = "sysExceptionLog";
            PKFieldName = "ExceptionLogID";
        }

        private ExceptionLogQueryBuilder query = new ExceptionLogQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(ExceptionLogEntry model)
        {

            string sql = query.QueryForCreate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task<ExceptionLogResult> Read(ExceptionLogParam param)
        {
            ExceptionLogResult ret = null;

            string sql = query.QueryForGet(null);

            ret = await Context.ExecuteQueryFirstAsync<ExceptionLogResult>(sql, param);

            return ret;
        }

        public async Task Update(ExceptionLogEntry model)
        {
            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task Delete(ExceptionLogEntry model)
        {

            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task<List<ExceptionLogList>> List(ExceptionLogParam param)
        {
            List<ExceptionLogList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<ExceptionLogList>(query.QueryForList(param), param);

            return ret;
        }

        public async Task<List<ExceptionLogResult>> Search(ExceptionLogParam param)
        {
            List<ExceptionLogResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<ExceptionLogResult>(query.QueryForSearch(param), param);

            return ret;
        }

    }

}
