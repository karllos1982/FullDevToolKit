using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.System.Models.Identity;

namespace FullDevToolKit.System.Contracts.Domains
{
    public interface IUserDomain :
        IDomain<UserParam, UserEntry, UserList, UserResult>
    {
        Task<UserEntry> GetByEmail(string email);

        Task<ExecutionStatus> UpdateUserLogin(UpdateUserLogin model);

        Task<ExecutionStatus> SetPasswordRecoveryCode(ChangeUserPassword model);

        Task<ExecutionStatus> ChangeUserPassword(ChangeUserPassword model);

        Task<ExecutionStatus> ActiveUserAccount(ActiveUserAccount model);

        Task<ExecutionStatus> ChangeUserProfileImage(ChangeUserImage model);

        Task<ExecutionStatus> UpdateLoginFailCounter(UpdateUserLoginFailCounter model);

        Task<ExecutionStatus> ChangeState(UserChangeState model);

        Task<ExecutionStatus> SetDateLogout(Int64 userid);

        Task<UserRolesEntry> AddRoleToUser(Int64 userid, Int64 roleid, bool gocommit);

        Task<UserRolesEntry> RemoveRoleFromUser(Int64 userid, Int64 roleid, bool gocommit);

        Task<UserInstancesEntry> AddInstanceToUser(Int64 userid, Int64 instanceid, bool gocommit);

        Task<UserInstancesEntry> RemoveInstanceFromUser(Int64 userid, Int64 instanceid, bool gocommit);

    }
}
