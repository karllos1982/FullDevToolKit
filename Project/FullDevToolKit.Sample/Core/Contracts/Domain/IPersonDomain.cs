using FullDevToolKit.Core;
using MyApp.Models;

namespace MyApp.Contracts.Domains
{
    public interface IPersonDomain :
        IDomain<PersonParam, PersonEntry, PersonList, PersonResult>
    {
     
        PersonContactEntry ContactEntryValidation(PersonContactEntry entry);
        
    }
}
