using FullDevToolKit.Common;
using FullDevToolKit.Core;
using MyApp.Contracts.Repositories;
using MyApp.Models;
using MyApp.Data.QueryBuilders;

namespace MyApp.Data.Repositories
{
    public class PersonContactRespository : IPersonContactRepository
    {
        public PersonContactRespository(IContext context)
        {
            Context = context;
            TableName = "PersonContacts";
            PKFieldName = "PersonContactID";
        }

        private PersonContactQueryBuilder query = new PersonContactQueryBuilder();

        public IContext Context { get; set ; }
        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public  async Task Create(PersonContactEntry model)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            string sql = query.QueryForCreate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
        }

        public async Task Delete(PersonContactEntry model)
        {
            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
        }

        public async Task<List<PersonContactList>> ReadList(PersonContactParam param)
        {
            List<PersonContactList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<PersonContactList>(query.QueryForList(null), param);

            return ret;
        }

        public async Task<PersonContactResult> ReadObject(PersonContactParam param)
        {
            PersonContactResult ret = null;

            string sql = query.QueryForGet(null);

            ret = await Context.ExecuteQueryFirstAsync<PersonContactResult>(sql, param);

            return ret;
        }

        public async Task<List<PersonContactResult>> ReadSearch(PersonContactParam param)
        {
            List<PersonContactResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<PersonContactResult>(query.QueryForSearch(null), param);


            return ret;
        }

        public async Task Update(PersonContactEntry model)
        {
            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
        }
    }
}
