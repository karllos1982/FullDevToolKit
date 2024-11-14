using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.System.Models.Identity;

namespace FullDevToolKit.System.Contracts.Domains
{
    public interface IPermissionDomain :
        IDomain<PermissionParam, PermissionEntry, PermissionList, PermissionResult>
    {

       Task<List<PermissionResult>> GetPermissionsByRoleUser(Int64 roleid, Int64 userid);

    }
}
