using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Contracts.Repositories;
using System.Runtime.InteropServices;
using System.Security;

namespace FullDevToolKit.Sys.Domains
{
    public class SystemDomainSet: ISystemDomainSet
    {

        public SystemDomainSet(IContext context)         
        {            
            this.Instance = new InstanceDomain(context);
            this.Role = new RoleDomain(context);
            this.User = new UserDomain(context);
            this.ObjectPermission = new ObjectPermissionDomain(context);
            this.Permission = new PermissionDomain(context);
            this.DataLog = new DataLogDomain(context);
            this.SessionLog = new SessionLogDomain(context);
            this.LocalizationText = new LocalizationTextDomain(context);
            this.GroupParameter = new GroupParameterDomain(context);
            this.Parameter = new ParameterDomain(context);
            this.ExceptionLog = new ExceptionLogDomain(context);    
        }
     

        public IInstanceDomain Instance { get; set; }

        public IRoleDomain Role { get; set; }

        public IUserDomain User { get; set; }

        public IObjectPermissionDomain ObjectPermission { get; set; }

        public IPermissionDomain Permission { get; set; }

        public IDataLogDomain DataLog { get; set; }

        public ISessionLogDomain SessionLog { get; set; }

        public ILocalizationTextDomain LocalizationText { get; set; }

        public IGroupParameterDomain GroupParameter { get; set; }

        public IParameterDomain Parameter { get; set; }

        public IExceptionLogDomain ExceptionLog { get; set; }
    }
}
