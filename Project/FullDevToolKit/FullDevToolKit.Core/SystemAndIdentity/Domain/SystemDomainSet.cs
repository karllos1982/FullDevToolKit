using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Contracts.Repositories;
using System.Runtime.InteropServices;
using System.Security;

namespace FullDevToolKit.Sys.Domains
{
    public class SystemDomainSet: ISystemDomainSet
    {

        public SystemDomainSet(ISystemRepositorySet repositorySet)         
        {            
            this.Instance = new InstanceDomain(repositorySet);
            this.Role = new RoleDomain(repositorySet);
            this.User = new UserDomain(repositorySet);
            this.ObjectPermission = new ObjectPermissionDomain(repositorySet);
            this.Permission = new PermissionDomain(repositorySet);
            this.DataLog = new DataLogDomain(repositorySet);
            this.SessionLog = new SessionLogDomain(repositorySet);
            this.LocalizationText = new LocalizationTextDomain(repositorySet);
            this.GroupParameter = new GroupParameterDomain(repositorySet);
            this.Parameter = new ParameterDomain(repositorySet);
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


    }
}
