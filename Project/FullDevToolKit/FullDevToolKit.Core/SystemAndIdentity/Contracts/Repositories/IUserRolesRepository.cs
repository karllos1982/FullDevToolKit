using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Contracts.Repositories
{
    public interface IUserRolesRepository :
        IRepository<UserRolesParam, UserRolesEntry, 
            UserRolesList, UserRolesResult>
    {

        Task AlterRole(UserRolesParam obj);
    }
}
