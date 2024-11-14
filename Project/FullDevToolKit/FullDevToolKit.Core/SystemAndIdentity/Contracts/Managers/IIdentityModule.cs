using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.System.Contracts.Domains;
using FullDevToolKit.System.Models.Identity;

namespace FullDevToolKit.System.Contracts.Managers
{
    public interface IIdentityModule: IBusinessModule
    {

      
        Task<UserEntry> CreateNewUser(NewUser data, bool gocommit, object userid);

        Task<UserEntry> Login(UserLogin model);

        Task<List<UserPermissions>> GetUserPermissions(Int64 roleid, Int64 userid);

        Task<PERMISSION_STATE_ENUM> CheckPermission(List<UserPermissions> permissions,
            string objectcode, PERMISSION_CHECK_ENUM type);

        Task<PermissionsState> GetPermissionsState(List<UserPermissions> permissions,
            string objectcode, bool allownone);

        Task RegisterLoginState(UserLogin model, UpdateUserLogin stateinfo);


        Task Logout(Int64 userid);

        Task<ExecutionStatus> GetTemporaryPassword(ChangeUserPassword model);

        Task<ExecutionStatus> GetActiveAccountCode(ActiveUserAccount model);

        Task<ExecutionStatus> GetChangePasswordCode(ChangeUserPassword model);

        Task<ExecutionStatus> ChangeUserProfileImage(ChangeUserImage model);

    }
}
