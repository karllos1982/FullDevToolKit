
namespace FullDevToolKit.Common
{
    // OBSOLITE INTERFACE
    public interface IDataWorker
    {        

        List<T> ExecuteQueryToList<T>(ref ExecutionStatus status,
            string sql, object filter = null);

        T ExecuteQueryFirst<T>(ref ExecutionStatus status,
            string sql, object filter = null);

        ExecutionStatus Execute(ref ExecutionStatus status,
            string sql, object data);


    }
}
