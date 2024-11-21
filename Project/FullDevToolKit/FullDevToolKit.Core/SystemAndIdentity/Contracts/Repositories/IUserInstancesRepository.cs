using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Contracts.Repositories
{
    public interface IUserInstancesRepository :
        IRepository<UserInstancesParam, UserInstancesEntry, 
             UserInstancesList, UserInstancesResult>
    {

        Task AlterInstance(UserInstancesParam obj);


    }
}
