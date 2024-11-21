using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Data.QueryBuilders;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class UserInstancesRepository : IUserInstancesRepository        
    {
       
        public UserInstancesRepository(IContext context)
        {
            Context = context;

            TableName = "sysUserInstances";
            PKFieldName = "UserInstanceID";

        }
         
        private UserInstancesQueryBuilder query = new UserInstancesQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(UserInstancesEntry model)
        {
            
            string sql = query.QueryForCreate(TableName, model,model);
            await Context.ExecuteAsync(sql, model);
           
        }

        public async Task<UserInstancesResult> Read(UserInstancesParam param)
        {
            UserInstancesResult ret = null;
            
            string sql = query.QueryForGet(null);

            ret = await Context
                .ExecuteQueryFirstAsync<UserInstancesResult>(sql, param); 
                 
            return ret;
        }

        public async Task Update(UserInstancesEntry model)
        {
            
            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
            
        }

        public async Task Delete(UserInstancesEntry model)
        {
            
            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

          }

        public async Task<List<UserInstancesList>> List(UserInstancesParam param)
        {
            List<UserInstancesList> ret = null;

            ret =await  Context
                .ExecuteQueryToListAsync<UserInstancesList>(query.QueryForList(null), param); 

            return ret;
        }
             
        public async Task<List<UserInstancesResult>> Search(UserInstancesParam param)
        {
            List<UserInstancesResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<UserInstancesResult>(query.QueryForSearch(null), param);

            return ret;
        }


        public async Task AlterInstance(UserInstancesParam obj)
        {

            string sql = query.QueryForAlterInstance (null);
            await Context.ExecuteAsync(sql, obj);

        }

    }

}
