using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Contracts.Repositories
{
    public interface IPermissionRepository :
        IRepositorySearchPaged<PermissionParam, PermissionEntry, PermissionList, PermissionResult>
    {
         Task<List<PermissionResult>> GetPermissionsByRoleUser(object param);

    }
}
