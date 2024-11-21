using FullDevToolKit.Core;

namespace MyApp.Contracts.Repositories
{
    public interface IMyAppRepositorySet : IRepositorySet
    {

        IPersonRepository Person { get; set; } 

        IPersonContactRepository PersonContact { get; set; }

    }
}
