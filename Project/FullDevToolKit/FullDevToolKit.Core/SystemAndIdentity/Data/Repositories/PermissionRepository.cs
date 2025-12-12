using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Data.QueryBuilders;
using FullDevToolKit.Core.Common;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class PermissionRepository : IPermissionRepository        
    {
       
        public PermissionRepository(IContext context)
        {
            Context = context;
            TableName = "sysPermission";
            PKFieldName = "PermissionID";
        }
         
        private PermissionQueryBuilder query = new PermissionQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(PermissionEntry model)
        {
            
            string sql = query.QueryForCreate(TableName, model,model);
            await Context.ExecuteAsync(sql, model);
            
        }

        public async Task<PermissionResult> ReadObject(PermissionParam param)
        {
            PermissionResult ret = null;
            
            string sql = query.QueryForGet(null);

            ret = await Context
                .ExecuteQueryFirstAsync<PermissionResult>(sql, param); 
                 
            return ret;
        }

        public async Task Update(PermissionEntry model)
        {
            
            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task Delete(PermissionEntry model)
        {
            
            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

          }

        public async Task<List<PermissionList>> ReadList(PermissionParam param)
        {
            List<PermissionList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<PermissionList>(query.QueryForList(null),param); 
                 

            return ret;
        }
             
   
        public async Task<PagedList<PermissionResult>> ReadSearch(PermissionParam param)
        {
            PagedList<PermissionResult> ret = new PagedList<PermissionResult>()
            { RecordList = new List<PermissionResult>() };

            List<PermissionResult> recordlist = null;
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
                .ExecuteQueryToListAsync<PermissionResult>(query.QueryForSearch(param), param);

                ret.PageCount = paginationSettings.PageCount;
                ret.TotalRecords = paglist.Count;
                ret.RecordList = recordlist;
                ret.RecordCount = recordlist.Count;
            }

            return ret;
        }

        public async Task<List<PermissionResult>> GetPermissionsByRoleUser(object param)
        {
            List<PermissionResult> ret = null;

            ret = await Context.ExecuteQueryToListAsync<PermissionResult>(
                query.QueryForGetPermissionsByRoleUser(param), param);

            return ret;
        }

    }

}
