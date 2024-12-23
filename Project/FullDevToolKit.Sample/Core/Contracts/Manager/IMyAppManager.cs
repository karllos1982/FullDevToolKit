using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Managers;

namespace MyApp.Contracts.Managers
{
    public interface IMyAppManager: IManager
    {
        
        IIdentityModule IdentityModule { get; set; }

        IMainBusinessModule MainBusinessModule { get; set; }
    }

}
