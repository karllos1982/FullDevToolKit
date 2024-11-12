using FullDevToolKit.Core;
using FullDevToolKit.System.Models.Identity;

namespace FullDevToolKit.System.Contracts.Repositories
{
    public interface IUserInstancesRepository :
        IRepository<UserInstancesParam, UserInstancesEntry, UserInstancesResult, UserInstancesResult>
    {

        Task AlterInstance(UserInstancesParam obj);


    }
}
