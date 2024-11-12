using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.System.Models.Identity;

namespace FullDevToolKit.System.Contracts.Repositories
{
    public interface IPermissionRepository :
        IRepository<PermissionParam, PermissionEntry, PermissionResult, PermissionList>
    {
         Task<List<PermissionResult>> GetPermissionsByRoleUser(object param);

    }
}
