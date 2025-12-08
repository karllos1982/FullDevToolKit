using FullDevToolKit.Common;
using FullDevToolKit.Core.Common;

namespace FullDevToolKit.Core
{
    public interface IDomain<TParam, TEntry, TList,TResult>
    {
        IContext Context { get; }

        Task<TResult> Get(TParam param);        

        Task<List<TList>> List(TParam param);

        Task<List<TResult>> Search(TParam param);

        Task<TEntry> Set(TEntry model, object userid);

        Task<TEntry> Remove(TEntry model, object userid);

    }

    public interface IDomainSearchPaged<TParam, TEntry, TList, TResult>
    {
        IContext Context { get; }

        Task<TResult> Get(TParam param);

        Task<List<TList>> List(TParam param);

        Task<PagedList<TResult>> Search(TParam param);

        Task<TEntry> Set(TEntry model, object userid);

        Task<TEntry> Remove(TEntry model, object userid);

    }

    public interface IDomainSync<TParam, TEntry, TResult, TList>
    {
        
        TResult Get(TParam param);

        TResult FillChields(TResult obj);

        List<TList> List(TParam param);

        List<TResult> Search(TParam param);

        TEntry Set(TEntry model, object userid);

        TEntry Delete(TEntry model, object userid);

        void EntryValidation(TEntry obj);

        void InsertValidation(TEntry obj);

        void UpdateValidation(TEntry obj);

        void DeleteValidation(TEntry obj);

    }

}