using FullDevToolKit.Core;
using MyApp.Contracts.Domains;
using MyApp.Contracts.Repositories;


namespace MyApp.Domain
{
    public class MyAppDomainSet: IMyAppDomainSet
    {
        public MyAppDomainSet(IContext context)
        {
            Person = new PersonDomain(context); 
        }

        public IPersonDomain Person { get; set; }

    }
}
