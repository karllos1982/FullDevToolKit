using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Data.QueryBuilders;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class ParameterRepository : IParameterRepository
    {

        public ParameterRepository(IContext context)
        {
            Context = context;
            TableName = "sysParameter";
            PKFieldName = "ParameterID";
        }

        private ParameterQueryBuilder query = new ParameterQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(ParameterEntry model)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            string sql = query.QueryForCreate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
        }

        public async Task<ParameterResult> Read(ParameterParam param)
        {
            ParameterResult ret = null;

            string sql = query.QueryForGet(null);

            ret = await Context.ExecuteQueryFirstAsync<ParameterResult>(sql, param);

            return ret;
        }

        public async Task Update(ParameterEntry model)
        {

            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task Delete(ParameterEntry model)
        {

            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task<List<ParameterList>> List(ParameterParam param)
        {
            List<ParameterList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<ParameterList>(query.QueryForList(null), param);

            return ret;
        }

        public async Task<List<ParameterResult>> Search(ParameterParam param)
        {
            List<ParameterResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<ParameterResult>(query.QueryForSearch(null), param);


            return ret;
        }


    }

}
