using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Sys.Contracts.Repositories
{
    public interface IExceptionLogRepository :
        IRepository<ExceptionLogParam, ExceptionLogEntry, ExceptionLogList, ExceptionLogResult>
    {
      
    }
}
