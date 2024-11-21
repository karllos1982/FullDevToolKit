using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Data.QueryBuilders; 

namespace FullDevToolKit.Sys.Data.Repositories
{

    public class UserRolesRepository : IUserRolesRepository
    {

        public UserRolesRepository(IContext context)
        {
            Context = context;

            TableName = "sysUserRoles";
            PKFieldName = "UserRoleD";

        }

        private UserRolesQueryBuilder query = new UserRolesQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(UserRolesEntry model)
        {

            string sql = query.QueryForCreate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task<UserRolesResult> Read(UserRolesParam param)
        {
            UserRolesResult ret = null;

            string sql = query.QueryForGet(null);

            ret = await Context
                .ExecuteQueryFirstAsync<UserRolesResult>(sql, param);

            return ret;
        }

        public async Task Update(UserRolesEntry model)
        {

            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task Delete(UserRolesEntry model)
        {

            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task<List<UserRolesList>> List(UserRolesParam param)
        {
            List<UserRolesList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<UserRolesList>(query.QueryForList(null), param);

            return ret;
        }

        public async Task<List<UserRolesResult>> Search(UserRolesParam param)
        {
            List<UserRolesResult> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<UserRolesResult>(query.QueryForSearch(null), param);

            return ret;
        }

        public async Task AlterRole(UserRolesParam obj)
        {

            string sql = query.QueryForAlterRole(null);
            await Context.ExecuteAsync(sql, obj);

        }

    }
}