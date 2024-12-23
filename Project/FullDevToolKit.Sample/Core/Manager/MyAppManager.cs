using MyApp.Contracts.Managers;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Managers;
using FullDevToolKit.Sys.Manager;

namespace MyApp.Managers
{
    public class MyAppManager: IMyAppManager
    {

        public MyAppManager(IContext context)
        {
            Context = context;            
            MainBusinessModule = new MainBusinessModule(context);
            IdentityModule = new IdentityModule(context);    
        }

        public IContext Context { get; set; }

        public IIdentityModule IdentityModule { get; set; }

        public IMainBusinessModule MainBusinessModule { get; set; }

    }
}
