using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;


namespace FullDevToolKit.Sys.Contracts.Domains
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

        ILocalizationTextDomain LocalizationText { get; set; }

        IGroupParameterDomain GroupParameter { get; set; }

        IParameterDomain Parameter { get; set; }

        IExceptionLogDomain ExceptionLog { get; set; }

        IConfigsDomain Configs { get; set; }
    }
}
