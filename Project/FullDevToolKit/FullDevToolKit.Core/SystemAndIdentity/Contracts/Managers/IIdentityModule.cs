using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Contracts.Managers
{
    public interface IIdentityModule: IBusinessModule
    {

        ISystemDomainSet Domainset { get; set; }

        Task<UserEntry> CreateNewUser(NewUser data, bool gocommit, object userid);

        Task<UserResult> Login(UserLogin model);

        Task<List<UserPermissions>> GetUserPermissions(long roleid, long userid);

        Task<PERMISSION_STATE_ENUM> CheckPermission(List<UserPermissions> permissions,
            string objectcode, PERMISSION_CHECK_ENUM type);

        Task<PermissionsState> GetPermissionsState(List<UserPermissions> permissions,
            string objectcode, bool allownone);

        Task RegisterLoginState(UserLogin model, UpdateUserLogin stateinfo);


        Task Logout(long userid);

        Task<string> GetTemporaryPassword(ChangeUserPassword model);

        Task<string> GetActiveAccountCode(ActiveUserAccount model);

        Task<string> GetChangePasswordCode(ChangeUserPassword model);

        Task ChangeUserProfileImage(ChangeUserImage model);

        Task<bool> ChangeUserLanguage(ChangeUserLanguage model); 

    }
}
