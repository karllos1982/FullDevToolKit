using FullDevToolKit.Core;
using MyApp.Contracts.Domains;

namespace MyApp.Contracts.Managers
{
    internal interface IMainBusinessModule : IBusinessModule
    {
        IContext Context { get; set; }

        IMyAppDomainSet DomainSet { get; set; }

        IMainBusinessModule BusinessModule { get; set; }

    }
}
