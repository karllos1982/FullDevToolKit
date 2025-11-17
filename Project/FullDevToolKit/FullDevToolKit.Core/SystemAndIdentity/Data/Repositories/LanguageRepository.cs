using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Data.QueryBuilders;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {

        public LanguageRepository(IContext context)
        {
            Context = context;
            TableName = "sysLanguage";
            PKFieldName = "LanguageID";
        }

        private LanguageQueryBuilder query = new LanguageQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(LanguageEntry model)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            string sql = query.QueryForCreate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
        }

        public async Task<LanguageResult> ReadObject(LanguageParam param)
        {
            LanguageResult ret = null;

            string sql = query.QueryForGet(null);

            ret = await Context.ExecuteQueryFirstAsync<LanguageResult>(sql, param);

            return ret;
        }

        public async Task Update(LanguageEntry model)
        {

            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task Delete(LanguageEntry model)
        {

            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task<List<LanguageList>> ReadList(LanguageParam param)
        {
            List<LanguageList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<LanguageList>(query.QueryForList(null), param);

            return ret;
        }

        public async Task<List<LanguageResult>> ReadSearch(LanguageParam param)
        {
            List<LanguageResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<LanguageResult>(query.QueryForSearch(null), param);


            return ret;
        }


    }

}
