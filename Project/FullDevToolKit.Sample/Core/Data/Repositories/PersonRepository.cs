using FullDevToolKit.Common;
using FullDevToolKit.Core;
using MyApp.Contracts.Repositories;
using MyApp.Models;
using MyApp.Data.QueryBuilders;

namespace MyApp.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {

        public PersonRepository(IContext context)
        {
            Context = context;
            TableName = "Person";
            PKFieldName = "PersonID";
        }

        private PersonQueryBuilder query = new PersonQueryBuilder();

        public IContext Context { get; set; }
        
        public string TableName { get; set; }

        public string PKFieldName { get; set; }


        public async Task Create(PersonEntry model)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            string sql = query.QueryForCreate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
        }

        public async Task<PersonResult> Read(PersonParam param)
        {
            PersonResult ret = null;

            string sql = query.QueryForGet(null);

            ret = await Context.ExecuteQueryFirstAsync<PersonResult>(sql, param);

            return ret;
        }

        public async Task Update(PersonEntry model)
        {

            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task Delete(PersonEntry model)
        {

            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task<List<PersonList>> List(PersonParam param)
        {
            List<PersonList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<PersonList>(query.QueryForList(null), param);

            return ret;
        }

        public async Task<List<PersonResult>> Search(PersonParam param)
        {
            List<PersonResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<PersonResult>(query.QueryForSearch(null), param);


            return ret;
        }


    }

}
