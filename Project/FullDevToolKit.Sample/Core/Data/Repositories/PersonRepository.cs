using FullDevToolKit.Common;
using FullDevToolKit.Core;
using MyApp.Contracts.Repositories;
using MyApp.Models;
using MyApp.Data.QueryBuilders;
using FullDevToolKit.Core.Common;
using FullDevToolKit.Sys.Models.Common;

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

        public async Task<PersonResult> ReadObject(PersonParam param)
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

        public async Task<List<PersonList>> ReadList(PersonParam param)
        {
            List<PersonList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<PersonList>(query.QueryForList(null), param);

            return ret;
        }

        public async Task<PagedList<PersonResult>> ReadSearch(PersonParam param)
        {
            PagedList<PersonResult> ret = new PagedList<PersonResult>()
            { RecordList = new List<PersonResult>() };

            List<PersonResult> recordlist = null;
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
                .ExecuteQueryToListAsync<PersonResult>(query.QueryForSearch(param), param);

                ret.PageCount = paginationSettings.PageCount;
                ret.TotalRecords = paglist.Count;
                ret.RecordList = recordlist;
                ret.RecordCount = recordlist.Count;
            }

            return ret;
        }
     

    }

}
