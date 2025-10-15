using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Data.QueryBuilders;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class GroupParameterRepository : IGroupParameterRepository
    {

        public GroupParameterRepository(IContext context)
        {
            Context = context;
            TableName = "sysGroupParameter";
            PKFieldName = "GroupParameterID";
        }

        private GroupParameterQueryBuilder query = new GroupParameterQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(GroupParameterEntry model)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            string sql = query.QueryForCreate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
        }

        public async Task<GroupParameterResult> ReadObject(GroupParameterParam param)
        {
            GroupParameterResult ret = null;

            string sql = query.QueryForGet(null);

            ret = await Context.ExecuteQueryFirstAsync<GroupParameterResult>(sql, param);

            return ret;
        }

        public async Task Update(GroupParameterEntry model)
        {

            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task Delete(GroupParameterEntry model)
        {

            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task<List<GroupParameterList>> ReadList(GroupParameterParam param)
        {
            List<GroupParameterList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<GroupParameterList>(query.QueryForList(null), param);

            return ret;
        }

        public async Task<List<GroupParameterResult>> ReadSearch(GroupParameterParam param)
        {
            List<GroupParameterResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<GroupParameterResult>(query.QueryForSearch(null), param);


            return ret;
        }


    }

}
