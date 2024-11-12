using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.System.Models.Identity;

namespace FullDevToolKit.System.Contracts.Repositories
{
    public interface IUserRepository :
        IRepository<UserParam, UserEntry, UserResult, UserList>
    {

        Task<UserResult> GetByEmail(string email);

        Task<ExecutionStatus> UpdateUserLogin(UpdateUserLogin model);

        Task<ExecutionStatus> SetPasswordRecoveryCode(SetPasswordRecoveryCode model);

        Task<ExecutionStatus> ChangeUserPassword(ChangeUserPassword model);

        Task<ExecutionStatus> ActiveUserAccount(ActiveUserAccount model);

        Task<ExecutionStatus> ChangeUserProfileImage(ChangeUserImage model);

        Task<ExecutionStatus> UpdateLoginFailCounter(UpdateUserLoginFailCounter model);

        Task<ExecutionStatus> ChangeState(UserChangeState model);

    }
}
