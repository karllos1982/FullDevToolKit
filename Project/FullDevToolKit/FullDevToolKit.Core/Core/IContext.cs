using FullDevToolKit.Common;

using System.Data;

namespace FullDevToolKit.Core
{
    public interface IContext
    {
        ISettings Settings { get; set; }

        string LocalizationLanguage { get; set; }

        ExecutionStatus ConnStatus { get; set; }

        ExecutionStatus Status { get; set; }

        // dbconnection metods
        IDbConnection Connection { get; set; }

        IDbTransaction Transaction { get; set; }

        IsolationLevel Isolation { get; set; }

        ExecutionStatus Commit();

        ExecutionStatus Rollback();

        ExecutionStatus Begin(int connindex, object isolationlavel);

        ExecutionStatus End();

        // executors methods

        void Execute(string sql, object data);

        T ExecuteQueryFirst<T>(string sql, object filter = null);

        List<T> ExecuteQueryToList<T>(string sql, object filter = null);


        // asyncs:

        Task ExecuteAsync(string sql, object data);

        Task<T> ExecuteQueryFirstAsync<T>(string sql, object filter = null);

        Task<List<T>> ExecuteQueryToListAsync<T>(string sql, object filter = null);


        // especial methods 

        Task RegisterExceptionLog(object exceptioninfo );

        void RegisterDataLog(string userid, OPERATIONLOGENUM operation,
          string tableaname, string objID, object olddata, object currentdata);

        Task RegisterDataLogAsync(string userid, OPERATIONLOGENUM operation,
             string tableaname, string objID, object olddata, object currentdata);

        Task<List<LocalizationTextItem>> GetLocalizationTextsAsync();

        Task<bool> CheckUniqueValueForInsert(string tablename, string fieldname, string fieldvalue);

        Task<bool> CheckUniqueValueForUpdate(string tablename, string fieldname,
                   string fieldvalue, string pkfieldname, string pkvalue); 

        void Dispose(); 

    }

}