using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Contracts.Domains
{
    public interface IPermissionDomain :
        IDomain<PermissionParam, PermissionEntry, PermissionList, PermissionResult>
    {

       Task<List<PermissionResult>> GetPermissionsByRoleUser(Int64 roleid, Int64 userid);

    }
}
