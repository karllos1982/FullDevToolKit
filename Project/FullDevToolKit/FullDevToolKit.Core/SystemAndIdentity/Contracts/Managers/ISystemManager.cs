using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.System.Contracts.Domains;

namespace FullDevToolKit.System.Contracts.Managers
{
    public interface ISystemManager: IManager
    {
       
        ISystemDomainSet DomainSet { get; }

        IIdentityModule IdentityModule { get; }

        //



    }
}
