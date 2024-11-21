using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;

namespace FullDevToolKit.Sys.Contracts.Managers
{
    public interface ISystemManager: IManager
    {
       
        IContext Context { get; set; }
        ISystemDomainSet DomainSet { get; set; }

        IIdentityModule IdentityModule { get; set; }

        //



    }
}
