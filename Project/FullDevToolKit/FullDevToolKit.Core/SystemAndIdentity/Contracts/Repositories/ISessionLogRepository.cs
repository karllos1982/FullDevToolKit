using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Core.Common;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Contracts.Repositories
{
    public interface ISessionLogRepository :
        IRepositorySearchPaged<SessionLogParam, SessionLogEntry, SessionLogList, SessionLogResult>
    {

        Task<ExecutionStatus> SetDateLogout(SessionLogParam obj);


    }
}
