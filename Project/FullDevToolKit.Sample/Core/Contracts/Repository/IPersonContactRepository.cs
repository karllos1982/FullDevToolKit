using FullDevToolKit.Core;
using MyApp.Models;

namespace MyApp.Contracts.Repositories
{
    public interface IPersonContactRepository :
        IRepository<PersonContactParam, PersonContactEntry,
            PersonContactList, PersonContactResult >
    {


    }
}