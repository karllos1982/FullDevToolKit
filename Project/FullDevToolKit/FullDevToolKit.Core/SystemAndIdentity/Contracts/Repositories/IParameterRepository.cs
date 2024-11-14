using FullDevToolKit.Core;
using FullDevToolKit.System.Models.Common;


namespace FullDevToolKit.System.Contracts.Repositories
{
    public interface IParameterRepository :
        IRepository<ParameterParam, ParameterEntry,
            ParameterResult, ParameterList>
    {


    }
}