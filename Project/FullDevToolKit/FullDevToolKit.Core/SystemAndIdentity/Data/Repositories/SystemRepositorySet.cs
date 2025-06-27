using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class SystemRepositorySet: ISystemRepositorySet
    {
        public SystemRepositorySet(IContext context)
        {
            this.DataLog = new DataLogRepository(context);
            this.Instance = new InstanceRepository(context);
            this.ObjectPermission = new ObjectPermissionRepository(context);
            this.Permission = new PermissionRepository(context);
            this.Role = new RoleRepository(context);
            this.SessionLog = new SessionLogRepository(context);
            this.UserInstances = new UserInstancesRepository(context);
            this.User = new UserRepository(context);
            this.UserRoles = new UserRolesRepository(context);
            this.LocalizationText = new LocalizationTextRepository(context);
            this.GroupParameter = new GroupParameterRepository(context);
            this.Parameter = new ParameterRepository(context);
            this.ExceptionLog = new ExceptionLogRepository(context);
        }
    
        
        public IDataLogRepository DataLog { get; set; }

        public IInstanceRepository Instance { get; set; }

        public IObjectPermissionRepository ObjectPermission { get; set; }

        public IPermissionRepository Permission { get; set; }

        public IRoleRepository Role { get; set; }

        public ISessionLogRepository SessionLog { get; set; }

        public IUserInstancesRepository UserInstances { get; set; }

        public IUserRepository User { get; set; }

        public IUserRolesRepository UserRoles { get; set; }

        public ILocalizationTextRepository LocalizationText { get; set; }

        public IGroupParameterRepository GroupParameter { get; set; }   

        public IParameterRepository Parameter { get; set; }

        public IExceptionLogRepository ExceptionLog { get; set; }

    }

}
