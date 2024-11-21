using FullDevToolKit.Core;

namespace MyApp.Contracts.Domains
{
    public interface IMyAppDomainSet : IDomainSet
    {
        IPersonDomain Person { get; set; }
    }
}
