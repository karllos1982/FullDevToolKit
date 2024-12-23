using FullDevToolKit.Core;
using MyApp.Contracts.Domains;

namespace MyApp.Contracts.Managers
{
    public interface IMainBusinessModule : IBusinessModule
    {
       
        IMyAppDomainSet DomainSet { get; set; }
    }
}
