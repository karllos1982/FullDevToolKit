using FullDevToolKit.Common;

namespace FullDevToolKit.Core
{
    public interface IRepository<TParam,TEntry, TList,TResult>
    {       
        string TableName { get; set; }

        string PKFieldName { get; set; }

        IContext Context { get; set; }

        Task Create(TEntry model);

        Task<TResult> Read(TParam model);

        Task Update(TEntry model);

        Task Delete(TEntry model);

        Task<List<TList>> List(TParam param);

        Task<List<TResult>> Search(TParam param);

    }

    public interface IRepositoryReadOnly<TParam, TResult>
    {

        IContext Context { get; set; }
              
        Task<List<TResult>> Read(TParam param);

    }



}


