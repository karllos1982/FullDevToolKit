using FullDevToolKit.Core;
using MyApp.Models;

namespace MyApp.Contracts.Repositories
{
    public interface IPersonRepository:
        IRepository<PersonParam, PersonEntry, PersonList,PersonResult >
    {
     

    }
}
