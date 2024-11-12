using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.System.Models.Identity;

namespace FullDevToolKit.System.Contracts.Repositories
{
    public interface ISessionLogRepository :
        IRepository<SessionLogParam, SessionLogEntry, SessionLogResult, SessionLogList>
    {

        Task<ExecutionStatus> SetDateLogout(SessionLogParam obj);


    }
}
