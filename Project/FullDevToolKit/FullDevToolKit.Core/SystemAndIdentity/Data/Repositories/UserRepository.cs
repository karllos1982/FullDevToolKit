using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Data.QueryBuilders;
using FullDevToolKit.Core.Common;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class UserRepository : IUserRepository        
    {
       
        public UserRepository(IContext context)
        {
            Context = context;
            TableName = "sysUser";
            PKFieldName = "UserID";

        }
         
        private UserQueryBuilder query = new UserQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(UserEntry model)
        {
           
            string sql = query.QueryForCreate(TableName, model,model);
            await Context.ExecuteAsync(sql, model);
         
        }

        public async Task<UserResult> ReadObject(UserParam param)
        {
            UserResult ret = null;
            
            string sql = query.QueryForGet(null);

            ret = await Context.ExecuteQueryFirstAsync<UserResult>(sql, param); 
                 
            return ret;
        }

        public async Task Update(UserEntry model)
        {
           
            string sql = query.QueryForUpdate(TableName, model, model);
             await Context.ExecuteAsync(sql, model);
        
        }

        public async Task Delete(UserEntry model)
        {
           
            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
           
        }

        public async Task<List<UserList>> ReadList(UserParam param)
        {
            List<UserList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<UserList>(query.QueryForList(null), param); 
               
            return ret;
        }
                   

        public async Task<PagedList<UserResult>> ReadSearch(UserParam param)
        {
            PagedList<UserResult> ret = new PagedList<UserResult>()
            { RecordList = new List<UserResult>() };

            List<UserResult> recordlist = null;
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
                .ExecuteQueryToListAsync<UserResult>(query.QueryForSearch(param), param);

                ret.PageCount = paginationSettings.PageCount;
                ret.TotalRecords = paglist.Count;
                ret.RecordList = recordlist;
                ret.RecordCount = recordlist.Count;
            }

            return ret;
        }

        //

        public async Task<UserResult> GetByEmail(string email)
        {
            UserResult ret = null;

            string sql = query.QueryForGetByEmail();

            ret = await Context.ExecuteQueryFirstAsync<UserResult>(sql,
                 new UserParam { pEmail = email });

            return ret;
        }

        public async Task UpdateUserLogin(UpdateUserLogin model)
        {            
            string sql = query.QueryForUpdateUserLogin();
            await Context.ExecuteAsync(sql, model);
           
        }

        public async Task SetPasswordRecoveryCode(SetPasswordRecoveryCode model)
        {            
            string sql = query.QueryForSetPasswordRecoveryCode();
            await Context.ExecuteAsync(sql, model);
          
        }

        public async Task ChangeUserPassword(ChangeUserPassword model)
        {            
            string sql = query.QueryForChangeUserPassword();
            await Context.ExecuteAsync(sql, model);
           
        }

        public async Task ActiveUserAccount(ActiveUserAccount model)
        {            
            string sql = query.QueryForActiveAccount();
            await Context.ExecuteAsync(sql, model);
             
        }

        public async Task ChangeUserProfileImage(ChangeUserImage model)
        {            
            string sql = query.QueryForChangeUserProfileImage();
            await Context.ExecuteAsync(sql, model);

        }
        public async Task ChangeUserLanguage(ChangeUserLanguage model)
        {
            string sql = query.QueryForChangeUserLanguage();
            await Context.ExecuteAsync(sql, model);

        }

        public async Task UpdateLoginFailCounter(UpdateUserLoginFailCounter model)
        {            
            string sql = query.QueryForSetLoginFailCounter(model.Reset);
            await Context.ExecuteAsync(sql, model);

        }

        public async Task ChangeState(UserChangeState model)
        {
            string sql = query.QueryForChangeUserState();
            await Context.ExecuteAsync(sql, model);
           
        }

        public async Task SetAuthToken(AuthTokenModel model)
        {
            string sql = query.QueryForSetAuthToken();
			await Context.ExecuteAsync(sql, model);
		}
	}

}
