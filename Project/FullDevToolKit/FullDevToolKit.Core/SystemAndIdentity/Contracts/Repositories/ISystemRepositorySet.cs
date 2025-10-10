using FullDevToolKit.Common;
using FullDevToolKit.Core;

namespace FullDevToolKit.Sys.Contracts.Repositories
{

    public interface ISystemRepositorySet : IRepositorySet
    {
     
        IDataLogRepository DataLog { get; set; }

        IInstanceRepository Instance { get; set; }

        IObjectPermissionRepository ObjectPermission { get; set; }

        IPermissionRepository Permission { get; set; }

        IRoleRepository Role { get; set; }

        ISessionLogRepository SessionLog { get; set; }

        IUserInstancesRepository UserInstances { get; set; }

        IUserRepository User { get; set; }

        IUserRolesRepository UserRoles { get; set; }

        ILocalizationTextRepository LocalizationText { get; set; }

        IGroupParameterRepository GroupParameter { get; set; }

        IParameterRepository Parameter { get; set; }

        IExceptionLogRepository ExceptionLog { get; set; }

        IConfigsRepository Configs { get; set; }
    }
}
