using FullDevToolKit.Common;
using FullDevToolKit.Core;
using MyApp.Contracts.Repositories;
using MyApp.Models;
using MyApp.Data.QueryBuilders;
using FullDevToolKit.Core.Common;
using FullDevToolKit.Sys.Models.Common;

namespace MyApp.Data.Repositories
{
    public class SimpleEntityRepository : ISimpleEntityRepository
    {
        public SimpleEntityRepository(IContext context)
        {
            Context = context;
            TableName = "SimpleEntity";
            PKFieldName = "SimpleEntityID";
        }

        private SimpleEntityQueryBuilder query = new SimpleEntityQueryBuilder();

        public IContext Context { get; set; }

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public async Task Create(SimpleEntityEntry model)
        {
            string sql = query.QueryForCreate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
        }

        public async Task<SimpleEntityResult> ReadObject(SimpleEntityParam param)
        {
            SimpleEntityResult ret = null;

            string sql = query.QueryForGet(null);

            ret = await Context.ExecuteQueryFirstAsync<SimpleEntityResult>(sql, param);

            return ret;
        }

        public async Task Update(SimpleEntityEntry model)
        {
            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
        }

        public async Task Delete(SimpleEntityEntry model)
        {
            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
        }

        public async Task<List<SimpleEntityList>> ReadList(SimpleEntityParam param)
        {
            List<SimpleEntityList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<SimpleEntityList>(query.QueryForList(null), param);

            return ret;
        }

        public async Task<PagedList<SimpleEntityResult>> ReadSearch(SimpleEntityParam param)
        {
            PagedList<SimpleEntityResult> ret = new PagedList<SimpleEntityResult>()
            { RecordList = new List<SimpleEntityResult>() };

            List<SimpleEntityResult> recordlist = null;
            List<PaginationModel> paglist = null;

            paglist = await Context
            .ExecuteQueryToListAsync<PaginationModel>(query.QueryForPaginationSettings(param), param);

            if (paglist.Count > 0)
            {
                BaseParam b_param = (param as BaseParam);
                PaginationSettings paginationSettings
                     = PaginationFeatures.BuildPaginationSettings(query, ref b_param, paglist);

                recordlist = await Context
                .ExecuteQueryToListAsync<SimpleEntityResult>(query.QueryForSearch(param), param);

                ret.PageCount = paginationSettings.PageCount;
                ret.TotalRecords = paglist.Count;
                ret.RecordList = recordlist;
                ret.RecordCount = recordlist.Count;
            }

            return ret;
        }
    }
}