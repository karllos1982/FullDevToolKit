using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Data.QueryBuilders;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class RoleRepository : IRoleRepository        
    {
       
        public RoleRepository(IContext context)
        {
            Context = context;
            TableName = "sysRole";
            PKFieldName = "RoleID";
        }
         
        private RoleQueryBuilder query = new RoleQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(RoleEntry model)
        {
            
            string sql = query.QueryForCreate(TableName, model,model);
            await Context.ExecuteAsync(sql, model);
            
        }

        public async Task<RoleResult> Read(RoleParam param)
        {
            RoleResult ret = null;
            
            string sql = query.QueryForGet(null);

            ret = await Context.ExecuteQueryFirstAsync<RoleResult>(sql, param); 
                 
            return ret;
        }

        public async Task Update(RoleEntry model)
        {
            
            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
         
         }

        public async Task Delete(RoleEntry model)
        {
            
            string sql = query.QueryForDelete(TableName, model, model);
           await Context.ExecuteAsync(sql, model);
            
        }

        public async Task<List<RoleList>> List(RoleParam param)
        {
            List<RoleList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<RoleList>(query.QueryForList(null), param); 
                
            return ret;
        }
             
        public async Task<List<RoleResult>> Search(RoleParam param)
        {
            List<RoleResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<RoleResult>(query.QueryForSearch(null),param);
                 

            return ret;
        }

        
    }

}
