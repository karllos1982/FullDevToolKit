using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Contracts.Managers;


namespace FullDevToolKit.Sys.Manager
{
    public class SystemManager : ISystemManager
    {
        
        public SystemManager(IContext context, ISystemDomainSet domainset)
        {
            Context = context;
            this.DomainSet = domainset;
            IdentityModule = new IdentityModule(domainset);
        }

        public IContext Context { get; set; }

        public ISystemDomainSet DomainSet { get; set; }

        public IIdentityModule IdentityModule { get; set; }

    }
}
