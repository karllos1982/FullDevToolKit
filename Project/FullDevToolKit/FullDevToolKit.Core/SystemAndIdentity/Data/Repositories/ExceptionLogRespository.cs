using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Data.QueryBuilders;
using FullDevToolKit.Core.Common;

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

        public async Task<ExceptionLogResult> ReadObject(ExceptionLogParam param)
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

        public async Task<List<ExceptionLogList>> ReadList(ExceptionLogParam param)
        {
            List<ExceptionLogList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<ExceptionLogList>(query.QueryForList(param), param);

            return ret;
        }

     
        public async Task<PagedList<ExceptionLogResult>> ReadSearch(ExceptionLogParam param)
        {
            PagedList<ExceptionLogResult> ret = new PagedList<ExceptionLogResult>()
            { RecordList = new List<ExceptionLogResult>() };

            List<ExceptionLogResult> recordlist = null;
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
                .ExecuteQueryToListAsync<ExceptionLogResult>(query.QueryForSearch(param), param);

                ret.PageCount = paginationSettings.PageCount;
                ret.TotalRecords = paglist.Count;
                ret.RecordList = recordlist;
                ret.RecordCount = recordlist.Count;
            }

            return ret;
        }

    }

}
