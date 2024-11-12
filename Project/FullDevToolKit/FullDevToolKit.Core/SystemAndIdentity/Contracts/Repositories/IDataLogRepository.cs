using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.System.Models.Common;

namespace FullDevToolKit.System.Contracts.Repositories
{
    public interface IDataLogRepository :
        IRepository<DataLogParam, DataLogEntry, DataLogResult, DataLogList>
    {
        Task<List<DataLogTimelineModel>> GetDataLogTimeline(Int64 recordID);

        Task<List<TabelasValueModel>> GetTableList();

    }
}
