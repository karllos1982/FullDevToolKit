using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Contracts.Repositories
{
    public interface ISessionLogRepository :
        IRepository<SessionLogParam, SessionLogEntry, SessionLogList, SessionLogResult>
    {

        Task<ExecutionStatus> SetDateLogout(SessionLogParam obj);


    }
}
