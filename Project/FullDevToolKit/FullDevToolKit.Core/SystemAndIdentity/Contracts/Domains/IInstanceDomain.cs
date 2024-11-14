using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.System.Models.Identity;

namespace FullDevToolKit.System.Contracts.Domains
{
    public interface IInstanceDomain :
        IDomain<InstanceParam, InstanceEntry, InstanceList, InstanceResult>
    {
      
    }
}
