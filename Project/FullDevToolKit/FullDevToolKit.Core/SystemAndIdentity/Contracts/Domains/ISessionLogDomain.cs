using FullDevToolKit.Core;
using FullDevToolKit.System.Models.Identity;

namespace FullDevToolKit.System.Contracts.Domains
{
    public interface ISessionLogDomain :
        IDomain<SessionLogParam, SessionLogEntry, SessionLogList, SessionLogResult>
    {

    }
}
