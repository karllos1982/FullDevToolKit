using FullDevToolKit.Core;
using MyApp.Contracts.Repositories;

namespace MyApp.Data.Repositories
{
    public class MyAppRepositorySet: IMyAppRepositorySet
    {
        public MyAppRepositorySet(IContext context) 
        { 
            this.Person = new PersonRepository(context);
            this.PersonContact = new PersonContactRespository(context);
        }

        public IPersonRepository Person { get; set; }

        public IPersonContactRepository PersonContact { get; set; }


    }
}
