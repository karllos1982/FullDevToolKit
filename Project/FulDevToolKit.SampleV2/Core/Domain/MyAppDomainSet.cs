using FullDevToolKit.Core;
using MyApp.Contracts.Domains;

namespace MyApp.Domain
{
    public class MyAppDomainSet : IMyAppDomainSet
    {
        public MyAppDomainSet(IContext context)
        {
            Person = new PersonDomain(context);
            SimpleEntity = new SimpleEntityDomain(context);
        }

        public IPersonDomain Person { get; set; }

        public ISimpleEntityDomain SimpleEntity { get; set; }
    }
}