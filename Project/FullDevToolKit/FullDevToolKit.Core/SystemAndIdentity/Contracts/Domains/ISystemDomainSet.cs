using FullDevToolKit.Core;
using FullDevToolKit.System.Contracts.Repositories;


namespace FullDevToolKit.System.Contracts.Domains
{
    public interface ISystemDomainSet: IDomainSet
    {        
        IInstanceDomain Instance { get; set; }

        IRoleDomain Role { get; set; }

        IUserDomain User { get; set; }

        IObjectPermissionDomain ObjectPermission { get; set; }

        IPermissionDomain Permission { get; set; }
                
        IDataLogDomain DataLog { get; set; }

        ISessionLogDomain SessionLog { get; set; }

        ILocalizationTextRepository LocalizationText { get; set; }

        IGroupParameterDomain GroupParameter { get; set; }

        IParameterDomain Parameter { get; set; }

    }
}
