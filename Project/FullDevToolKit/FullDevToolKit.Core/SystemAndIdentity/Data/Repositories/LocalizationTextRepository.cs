using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Data.QueryBuilders;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class LocalizationTextRepository : ILocalizationTextRepository
    {

        public LocalizationTextRepository(IContext context)
        {
            Context = context;
            TableName = "sysLocalizationText";
            PKFieldName = "LocalizationTextID";
        }

        private LocalizationTextQueryBuilder query = new LocalizationTextQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(LocalizationTextEntry model)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            string sql = query.QueryForCreate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
        }

        public async Task<LocalizationTextResult> Read(LocalizationTextParam param)
        {
            LocalizationTextResult ret = null;

            string sql = query.QueryForGet(null);

            ret = await Context.ExecuteQueryFirstAsync<LocalizationTextResult>(sql, param);

            return ret;
        }

        public async Task Update(LocalizationTextEntry model)
        {

            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task Delete(LocalizationTextEntry model)
        {

            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task<List<LocalizationTextList>> List(LocalizationTextParam param)
        {
            List<LocalizationTextList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<LocalizationTextList>(query.QueryForList(null), param);

            return ret;
        }

        public async Task<List<LocalizationTextResult>> Search(LocalizationTextParam param)
        {
            List<LocalizationTextResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<LocalizationTextResult>(query.QueryForSearch(null), param);


            return ret;
        }

        public async Task<List<LocalizationTextList>> GetListOfLanguages()
        {
            List<LocalizationTextList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<LocalizationTextList>(query.QueryForListOfLanguages(null), null);

            return ret;
        }

    }

}
