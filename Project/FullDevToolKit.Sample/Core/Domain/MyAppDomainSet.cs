using FullDevToolKit.Core;
using MyApp.Contracts.Domains;


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
