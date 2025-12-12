using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Contracts.Domains
{
    public interface IUserDomain :
        IDomainSearchPaged<UserParam, UserEntry, UserList, UserResult>
    {
        Task<UserResult> GetByEmail(string email);

        Task UpdateUserLogin(UpdateUserLogin model);

        Task<string> SetPasswordRecoveryCode(ChangeUserPassword model);

        Task ChangeUserPassword(ChangeUserPassword model);

        Task ActiveUserAccount(ActiveUserAccount model);

        Task ChangeUserProfileImage(ChangeUserImage model);

        Task UpdateLoginFailCounter(UpdateUserLoginFailCounter model);

        Task ChangeState(UserChangeState model);

        Task<ExecutionStatus> SetDateLogout(Int64 userid);

        Task<bool> ChangeUserLanguage(ChangeUserLanguage model);        

        Task<UserRolesEntry> AddRoleToUser(Int64 userid, Int64 roleid);

        Task<UserRolesEntry> RemoveRoleFromUser(Int64 userid, Int64 roleid);

        Task<UserInstancesEntry> AddInstanceToUser(Int64 userid, Int64 instanceid);

        Task<UserInstancesEntry> RemoveInstanceFromUser(Int64 userid, Int64 instanceid);

    }
}
