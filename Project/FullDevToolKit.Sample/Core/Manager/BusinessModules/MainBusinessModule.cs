using FullDevToolKit.Core;
using MyApp.Contracts.Domains;
using MyApp.Contracts.Managers;
using MyApp.Domain;


namespace MyApp.Managers
{
    public class MainBusinessModule : IMainBusinessModule
    {
        public MainBusinessModule(IContext context) 
        {

            DomainSet = new MyAppDomainSet(context); 
        }

        public IContext Context { get; set; }

        public IMyAppDomainSet DomainSet { get; set; }


    }
}
