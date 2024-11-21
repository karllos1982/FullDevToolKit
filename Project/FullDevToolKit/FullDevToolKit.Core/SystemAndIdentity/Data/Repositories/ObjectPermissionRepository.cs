using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Data.QueryBuilders;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class ObjectPermissionRepository : IObjectPermissionRepository        
    {
       
        public ObjectPermissionRepository(IContext context)
        {
            Context = context;
            TableName = "sysObjectPermission";
            PKFieldName = "ObjectPermissionID";
        }
         
        private ObjectPermissionQueryBuilder query = new ObjectPermissionQueryBuilder();

        public IContext Context { get; set; }

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public async Task Create(ObjectPermissionEntry model)
        {
            
            string sql = query.QueryForCreate(TableName, model,model);
            await Context.ExecuteAsync(sql, model);
            
        }

        public async Task<ObjectPermissionResult> Read(ObjectPermissionParam param)
        {
            ObjectPermissionResult ret = null;
            
            string sql = query.QueryForGet(null);

            ret = await Context
                .ExecuteQueryFirstAsync<ObjectPermissionResult>(sql, param); 
                 
            return ret;
        }

        public async Task Update(ObjectPermissionEntry model)
        {
            
            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

         }

        public async Task Delete(ObjectPermissionEntry model)
        {
            
            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task<List<ObjectPermissionList>> List(ObjectPermissionParam param)
        {
            List<ObjectPermissionList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<ObjectPermissionList>(query.QueryForList(null), param); 
                

            return ret;
        }
             
        public async Task<List<ObjectPermissionResult>> Search(ObjectPermissionParam param)
        {
            List<ObjectPermissionResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<ObjectPermissionResult>(query.QueryForSearch(null),param);
                 
            return ret;
        }

        
    }

}
