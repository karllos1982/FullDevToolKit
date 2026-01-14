using FullDevToolKit.Core;
using MyApp.Models;

namespace MyApp.Contracts.Domains
{
    public interface ISimpleEntityDomain :
        IDomainSearchPaged<SimpleEntityParam, SimpleEntityEntry, SimpleEntityList, SimpleEntityResult>
    {
    }
}