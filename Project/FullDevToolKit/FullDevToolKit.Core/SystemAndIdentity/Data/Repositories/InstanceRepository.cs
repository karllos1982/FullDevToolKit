using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Data.QueryBuilders;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class InstanceRepository : IInstanceRepository        
    {
       
        public InstanceRepository(IContext context)
        {
            Context = context;
            TableName = "sysInstance";
            PKFieldName = "InstanceID";
        }
         
        private InstanceQueryBuilder query = new InstanceQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(InstanceEntry model)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            string sql = query.QueryForCreate(TableName, model,model);
            await Context.ExecuteAsync(sql, model);
        }

        public async Task<InstanceResult> Read(InstanceParam param)
        {
            InstanceResult ret = null;
            
            string sql = query.QueryForGet(null);

            ret = await Context.ExecuteQueryFirstAsync<InstanceResult>(sql, param); 
                 
            return ret;
        }

        public async Task Update(InstanceEntry model)
        {
            
            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task Delete(InstanceEntry model)
        {
           
            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
      
        }

        public async Task<List<InstanceList>> List(InstanceParam param)
        {
            List<InstanceList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<InstanceList>(query.QueryForList(null), param); 
                
            return ret;
        }
             
        public async Task<List<InstanceResult>> Search(InstanceParam param)
        {
            List<InstanceResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<InstanceResult>(query.QueryForSearch(null),  param);
               

            return ret;
        }

        
    }

}
