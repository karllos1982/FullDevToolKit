using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Contracts.Repositories
{
    public interface IUserRepository :
        IRepository<UserParam, UserEntry, UserList, UserResult>
    {

        Task<UserResult> GetByEmail(string email);

        Task UpdateUserLogin(UpdateUserLogin model);

        Task SetPasswordRecoveryCode(SetPasswordRecoveryCode model);

        Task ChangeUserPassword(ChangeUserPassword model);

        Task ActiveUserAccount(ActiveUserAccount model);

        Task ChangeUserProfileImage(ChangeUserImage model);

        Task UpdateLoginFailCounter(UpdateUserLoginFailCounter model);

        Task ChangeState(UserChangeState model);

        Task ChangeUserLanguage(ChangeUserLanguage model);

    }
}
