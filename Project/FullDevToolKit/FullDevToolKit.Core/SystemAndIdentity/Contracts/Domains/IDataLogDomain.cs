using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.System.Contracts.Repositories;
using FullDevToolKit.System.Models.Common;

namespace FullDevToolKit.System.Contracts.Domains
{
    public interface IDataLogDomain :
        IDomain<DataLogParam, DataLogEntry, DataLogList, DataLogResult>
    {
       
       Task<List<DataLogTimelineModel>> GetTimeLine(Int64 recordID);

       Task<List<TabelasValueModel>> GetTableList();

    }

}

