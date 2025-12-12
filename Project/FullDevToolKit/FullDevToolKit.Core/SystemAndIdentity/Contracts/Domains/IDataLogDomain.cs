using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Sys.Contracts.Domains
{
    public interface IDataLogDomain :
        IDomainSearchPaged<DataLogParam, DataLogEntry, DataLogList, DataLogResult>
    {
       
       Task<List<DataLogTimelineModel>> GetTimeLine(Int64 recordID);

       Task<List<TabelasValueModel>> GetTableList();

    }

}

