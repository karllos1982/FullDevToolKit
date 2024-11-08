using FullDevToolKit.Common;

namespace FullDevToolKit.Core
{
    public interface IManager
    {
        IContext Context { get; set; }

        void InitializeDomains(IContext context, IRepositorySet repositorySet);

    }

}