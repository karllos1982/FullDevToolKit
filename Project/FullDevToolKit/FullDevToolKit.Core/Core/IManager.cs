using FullDevToolKit.Common;

namespace FullDevToolKit.Core
{
    public interface IManager
    {
        IContext Context { get; set; }

        void Initialize(IContext context, IRepositorySet repositorySet);


    }

}