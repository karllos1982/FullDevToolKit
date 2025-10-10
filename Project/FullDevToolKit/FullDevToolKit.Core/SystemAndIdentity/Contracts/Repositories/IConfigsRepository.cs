using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Common;


namespace FullDevToolKit.Sys.Contracts.Repositories
{
    public interface IConfigsRepository :
        IRepository<ConfigsParam, ConfigsEntry,
            ConfigsList, ConfigsResult >
    {


    }
}