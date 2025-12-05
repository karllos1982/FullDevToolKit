using System.Data;
using Microsoft.Data.SqlClient;
using FullDevToolKit.Common;
using FullDevToolKit.Core;
using Dapper;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Core.Helpers;

namespace MyApp.Context
{
    public interface ISystemContext: IContext
    {

    }

    public class SystemContext : ISystemContext
    {
        public SystemContext(ISettings settings)
        {
            Settings = settings;
            ConnStatus = new ExecutionStatus(true);
            Status = new ExecutionStatus(true);
            Isolation = IsolationLevel.ReadUncommitted;
        }

        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public IsolationLevel Isolation { get; set; }
        public ISettings Settings { get; set; }
        public ExecutionStatus ConnStatus { get; set; }

        public ExecutionStatus Status { get; set; }
        public string LocalizationLanguage { get; set; }

        public ExecutionStatus Begin(ConnectionStringItem conn, object isolationlavel)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            Connection = new SqlConnection(conn.Value);

            return ret;
        }

        public ExecutionStatus Commit()
        {
            ExecutionStatus ret = new ExecutionStatus(true);

          
            return ret;
        }

        public ExecutionStatus Rollback()
        {
            ExecutionStatus ret = new ExecutionStatus(true);

       
            return ret;
        }

        public ExecutionStatus End()
        {
            ExecutionStatus ret = new ExecutionStatus(true);

       
            return ret;
        }

        //

        public void Execute(string sql, object data)
        {
            Status.Success = true;
            Status.Exceptions = null;
            Status.Returns = null;

            if (this.Connection.State == System.Data.ConnectionState.Open)
            {

                try
                {
                    this.Connection.ExecuteAsync(sql, data, Transaction);

                }
                catch (SqlException ex)
                {
                    Status.SetFailStatus("Error", ex.Message);

                }
                catch (System.Exception ex)
                {
                    Status.SetFailStatus("Error", ex.Message);

                }

            }
            else
            {
                Status.SetFailStatus("Error", "The connection is closed!");

            }

        }

        public T ExecuteQueryFirst<T>(string sql, object filter = null)
        {
            Status.Success = true;
            Status.Exceptions = null;
            Status.Returns = null;

            T ret = (default);

            if (this.Connection.State == System.Data.ConnectionState.Open)
            {

                try
                {
                    ret = this.Connection.QueryFirst<T>(sql, filter, Transaction);

                }
                catch (SqlException ex)
                {
                    Status.SetFailStatus("Error", ex.Message);

                }
                catch (System.Exception ex)
                {
                    Status.SetFailStatus("Error", ex.Message);

                }

            }
            else
            {
                Status.SetFailStatus("Error", "The connection is closed!");
            }

            return ret;

        }

        public List<T> ExecuteQueryToList<T>(string sql, object filter = null)
        {
            List<T> ret = new List<T>();

            Status.Success = true;
            Status.Exceptions = null;
            Status.Returns = null;

            if (this.Connection.State == System.Data.ConnectionState.Open)
            {

                try
                {
                    ret = this.Connection
                        .Query<T>(sql, filter, Transaction).AsList<T>();
                }
                catch (SqlException ex)
                {
                    Status.SetFailStatus("Error", ex.Message);

                }
                catch (System.Exception ex)
                {
                    Status.SetFailStatus("Error", ex.Message);

                }

            }
            else
            {
                Status.SetFailStatus("Error", "The connection is closed!");
            }


            return ret;

        }


        // asyncs:

        public async Task ExecuteAsync(string sql, object data)
        {
            Status.Success = true;
            Status.Exceptions = null;
            Status.Returns = null;

            if (this.Connection.State == System.Data.ConnectionState.Open)
            {

                try
                {
                    await this.Connection.ExecuteAsync(sql, data, Transaction);

                }
                catch (SqlException ex)
                {
                    Status.SetFailStatus("Error", ex.Message);

                }
                catch (System.Exception ex)
                {
                    Status.SetFailStatus("Error", ex.Message);

                }

            }
            else
            {
                Status.SetFailStatus("Error", "The connection is closed!");
            }

        }

        public async Task<T> ExecuteQueryFirstAsync<T>(string sql, object filter = null)
        {
            Status.Success = true;
            Status.Exceptions = null;
            Status.Returns = null;

            T ret = (default);


            if (this.Connection.State == System.Data.ConnectionState.Open)
            {

                try
                {

                    ret = await this.Connection.QueryFirstAsync<T>(sql, filter, Transaction);

                }
                catch (SqlException ex)
                {
                    Status.SetFailStatus("Error", ex.Message);

                }
                catch (System.Exception ex)
                {
                    Status.SetFailStatus("Error", ex.Message);

                }

            }
            else
            {
                Status.SetFailStatus("Error", "The connection is closed!");
            }

            return ret;

        }

        public async Task<List<T>> ExecuteQueryToListAsync<T>(string sql, object filter = null)
        {
            List<T> ret = new List<T>();

            Status.Success = true;
            Status.Exceptions = null;
            Status.Returns = null;

            if (this.Connection.State == System.Data.ConnectionState.Open)
            {

                try
                {
                    IEnumerable<T> list = await this.Connection
                        .QueryAsync<T>(sql, filter, Transaction);

                    ret = list.AsList();

                }
                catch (SqlException ex)
                {
                    Status.SetFailStatus("Error", ex.Message);

                }
                catch (System.Exception ex)
                {
                    Status.SetFailStatus("Error", ex.Message);

                }

            }
            else
            {
                Status.SetFailStatus("Error", "The connection is closed!");
            }


            return ret;

        }


        //


        public void Dispose()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Dispose();
                this.Transaction = null;
            }

            if (this.Connection != null)
            {
                this.Connection.Close();
                this.Connection.Dispose();
                this.Connection = null;
            }
        }


        public void RegisterDataLog(string userid, OPERATIONLOGENUM operation,
                 string tableaname, string objID, object olddata, object currentdata)
        {


        }

        public async Task RegisterDataLogAsync(string userid, OPERATIONLOGENUM operation,
              string tableaname, string objID, object olddata, object currentdata)
        {


        }

        public async Task<List<LocalizationTextItem>> GetLocalizationTextsAsync()
        {
            List<LocalizationTextItem> ret = new List<LocalizationTextItem>();

            return ret;
        }

        public async Task<bool> CheckUniqueValueForInsert(string tablename, string fieldname, string fieldvalue)
        {
            bool ret = true;

            return ret;
        }

        public async Task<bool> CheckUniqueValueForUpdate(string tablename, string fieldname, string fieldvalue, string pkfieldname, string pkvalue)
        {
            bool ret = true;

            return ret;
        }

        public async Task RegisterExceptionLog(object exceptioninfo)
        {
            ConnectionStringItem conn
                = Settings.Connections.GetConnection("MASTERDB");

            Connection = new SqlConnection(conn.Value);

            try
            {
                Connection.Open();
                Transaction = Connection.BeginTransaction(Isolation);

                ExceptionLogEntry entry = (ExceptionLogEntry)exceptioninfo;

                string sql = "";
                sql = @"insert into [sysExceptionLog] values (
                       @ExceptionLogID,
                       @UserID,
                       @Date,
                       @Origin,
                       @TargetSite,
                       @ErrMessage,
                       @StackTrace,
                       @ClientIP
                       )";

                await ExecuteAsync(sql, entry);                

            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                Transaction.Commit();
                this.Dispose();
            }

        }
    }


}
