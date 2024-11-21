using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Sys.Contracts.Repositories
{
    public interface IDataLogRepository :
        IRepository<DataLogParam, DataLogEntry, DataLogList, DataLogResult>
    {
        Task<List<DataLogTimelineModel>> GetDataLogTimeline(Int64 recordID);

        Task<List<TabelasValueModel>> GetTableList();

    }
}
