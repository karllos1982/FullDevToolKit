using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Data.QueryBuilders;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class ConfigsRepository : IConfigsRepository
    {

        public ConfigsRepository(IContext context)
        {
            Context = context;
            TableName = "sysConfigs";
            PKFieldName = "ConfigID";
        }

        private ConfigsQueryBuilder query = new ConfigsQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(ConfigsEntry model)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            string sql = query.QueryForCreate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
        }

        public async Task<ConfigsResult> ReadObject(ConfigsParam param)
        {
            ConfigsResult ret = null;

            string sql = query.QueryForGet(null);

            ret = await Context.ExecuteQueryFirstAsync<ConfigsResult>(sql, param);

            return ret;
        }

        public async Task Update(ConfigsEntry model)
        {

            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task Delete(ConfigsEntry model)
        {

            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task<List<ConfigsList>> ReadList(ConfigsParam param)
        {
            List<ConfigsList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<ConfigsList>(query.QueryForList(null), param);

            return ret;
        }

        public async Task<List<ConfigsResult>> ReadSearch(ConfigsParam param)
        {
            List<ConfigsResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<ConfigsResult>(query.QueryForSearch(null), param);


            return ret;
        }


    }

}
