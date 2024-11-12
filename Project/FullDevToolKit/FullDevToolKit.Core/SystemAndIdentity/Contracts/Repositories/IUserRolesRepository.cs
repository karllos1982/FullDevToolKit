using FullDevToolKit.Core;
using FullDevToolKit.System.Models.Identity;

namespace FullDevToolKit.System.Contracts.Repositories
{
    public interface IUserRolesRepository :
        IRepository<UserRolesParam, UserRolesEntry, UserRolesResult, UserRolesResult>
    {

        Task AlterRole(UserRolesParam obj);
    }
}
