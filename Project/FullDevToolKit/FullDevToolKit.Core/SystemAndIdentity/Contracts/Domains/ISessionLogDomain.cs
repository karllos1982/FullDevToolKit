using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Contracts.Domains
{
    public interface ISessionLogDomain :
        IDomain<SessionLogParam, SessionLogEntry, SessionLogList, SessionLogResult>
    {
       
    }
}
