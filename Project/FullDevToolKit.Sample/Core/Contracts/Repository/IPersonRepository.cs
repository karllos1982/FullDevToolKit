using FullDevToolKit.Core;
using MyApp.Models;


namespace MyApp.Contracts.Repositories
{
    public interface IPersonRepository:
        IRepositorySearchPaged<PersonParam, PersonEntry, PersonList,PersonResult >
    {
     

    }
}
