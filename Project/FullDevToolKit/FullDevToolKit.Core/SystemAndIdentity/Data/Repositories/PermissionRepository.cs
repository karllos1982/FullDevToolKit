using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Data.QueryBuilders;

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
             
        public  async Task<List<PermissionResult>> ReadSearch(PermissionParam param)
        {
            List<PermissionResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<PermissionResult>(query.QueryForSearch(null),param);
                 

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
