using FullDevToolKit.Core;
using MyApp.Models;

namespace MyApp.Contracts.Repositories
{
    public interface ISimpleEntityRepository :
        IRepositorySearchPaged<SimpleEntityParam, SimpleEntityEntry, SimpleEntityList, SimpleEntityResult>
    {
    }
}