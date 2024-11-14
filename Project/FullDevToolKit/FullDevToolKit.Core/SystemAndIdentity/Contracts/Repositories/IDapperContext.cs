using FullDevToolKit.Common;
using FullDevToolKit.Core;
using System.Data;
using System.Data.Common;

namespace FullDevToolKit.System.Contracts
{
    public interface IDapperContext : IContext
    {

         IDbConnection Connection { get; set; }

         IDbTransaction Transaction { get; set; }

         IsolationLevel Isolation { get; set; }

         ExecutionStatus Commit();

         ExecutionStatus Rollback();

         void Execute(string sql,object data);

         T ExecuteQueryFirst<T>(string sql,  object filter = null);

         List<T> ExecuteQueryToList<T>(string sql,  object filter = null);


        // asyncs:

        Task ExecuteAsync(string sql, object data);

        Task<T> ExecuteQueryFirstAsync<T>(string sql, object filter = null);

        Task<List<T>> ExecuteQueryToListAsync<T>(string sql, object filter = null);


    }
}
