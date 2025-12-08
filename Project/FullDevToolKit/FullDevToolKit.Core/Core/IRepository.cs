using FullDevToolKit.Common;
using FullDevToolKit.Core.Common;

namespace FullDevToolKit.Core
{
    public interface IRepository<TParam,TEntry, TList,TResult>
    {       
        string TableName { get; set; }

        string PKFieldName { get; set; }

        IContext Context { get; set; }

        Task Create(TEntry model);

        Task<TResult> ReadObject(TParam model);

        Task Update(TEntry model);

        Task Delete(TEntry model);

        Task<List<TList>> ReadList(TParam param);

        Task<List<TResult>> ReadSearch(TParam param);

    }

    public interface IRepositorySearchPaged<TParam, TEntry, TList, TResult>
    {
        string TableName { get; set; }

        string PKFieldName { get; set; }

        IContext Context { get; set; }

        Task Create(TEntry model);

        Task<TResult> ReadObject(TParam model);

        Task Update(TEntry model);

        Task Delete(TEntry model);

        Task<List<TList>> ReadList(TParam param);

        Task<PagedList<TResult>> ReadSearch(TParam param);

    }

    public interface IRepositoryReadOnly<TParam, TResult>
    {

        IContext Context { get; set; }
              
        Task<List<TResult>> Read(TParam param);

    }



}


