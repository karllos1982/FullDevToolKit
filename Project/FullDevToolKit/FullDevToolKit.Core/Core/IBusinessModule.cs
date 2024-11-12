using FullDevToolKit.Common;

namespace FullDevToolKit.Core
{
    public interface IBusinessModule
    {
        IContext Context { get; set; }

        void InitializeDomainSet(IContext context, IRepositorySet repositorySet);

    }
}
