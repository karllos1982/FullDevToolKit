using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Contracts.Repositories
{
    public interface IObjectPermissionRepository :
        IRepository<ObjectPermissionParam, ObjectPermissionEntry,
            ObjectPermissionList,ObjectPermissionResult >
    {
      

    }
}
