using FullDevToolKit.Sys.Contracts;
using System.Data;
using System.Data.SqlClient;
using FullDevToolKit.Common;
using FullDevToolKit.Core;
using Newtonsoft.Json;
using Dapper;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Data.QueryBuilders;

namespace MyApp.Context
{
    public class DapperContext : IContext
    {
        public DapperContext(ISettings settings)
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
        public string LocalizationLanguage { get; set ; }

        public ExecutionStatus Begin(int sourceindex, object isolationlavel)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            Connection = new SqlConnection(Settings.Sources[sourceindex].SourceValue);
            
            try
            {
                Connection.Open();
                Transaction = Connection.BeginTransaction(Isolation);                
               
            }
            catch (Exception ex)
            {               
                ret.SetFailStatus("Error" , ex.Message);
                ConnStatus = ret; 

            }

            return ret;
        }

        public ExecutionStatus Commit()
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            if (this.Connection != null)
            {
                if (this.Connection.State == ConnectionState.Open)
                {

                    if (this.Transaction != null)
                    {
                       
                        try
                        {
                            this.Transaction.Commit();

                        }
                        catch (System.Exception ex)
                        {
                            ret.SetFailStatus("Error", ex.Message);

                        }
                                              
                    }                   

                }

            }

            Status = ret;

            return ret;
        }

        public ExecutionStatus Rollback()
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            if (this.Connection != null)
            {
                if (this.Connection.State == ConnectionState.Open)
                {

                    if (this.Transaction != null)
                    {

                        try
                        {
                            this.Transaction.Rollback();

                        }
                        catch (System.Exception ex)
                        {
                            ret.SetFailStatus("Error", ex.Message);

                        }

                    }

                }

            }

            Status = ret;
            return ret;
        }

        public ExecutionStatus End()
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            if (this.Connection != null)
            {
                if (this.Connection.State == ConnectionState.Open)
                {

                    if (this.Transaction != null)
                    {
                        if (Status.Success)
                        {
                            ret = this.Commit(); 
                        }
                        else
                        {
                            ret = this.Rollback();
                        }
                    }

                }

            }

            this.Dispose();

            Status = ret;

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

        public T ExecuteQueryFirst<T>(string sql,object filter = null)                
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
                  await  this.Connection.ExecuteAsync(sql, data, Transaction);

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

        public async Task<T> ExecuteQueryFirstAsync<T>( string sql, object filter = null)            
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

            DataLogObject obj = new DataLogObject();

            string s_olddata = "";
            string s_currentdata = "";

            obj.DataLogID = Utilities.GenerateId();
            obj.UserID = Int64.Parse(userid);
            obj.Date = DateTime.Now;

            switch (operation)
            {
                case OPERATIONLOGENUM.INSERT:
                    obj.Operation = "I";

                    s_currentdata = JsonConvert.SerializeObject(currentdata);

                    break;

                case OPERATIONLOGENUM.UPDATE:
                    obj.Operation = "U";

                    s_olddata = JsonConvert.SerializeObject(olddata);
                    s_currentdata = JsonConvert.SerializeObject(currentdata);

                    break;

                case OPERATIONLOGENUM.DELETE:
                    obj.Operation = "D";

                    s_olddata = JsonConvert.SerializeObject(olddata);
                    break;
            }

            obj.TableName = tableaname;
            obj.ID = Int64.Parse(objID);
            obj.LogOldData = s_olddata;
            obj.LogCurrentData = s_currentdata;
            
            DataLogQueryBuilder qw = new DataLogQueryBuilder(); 

            string sqltext =                
                    qw.BuildInsertCommand("[sysdatalog]", obj, null);

            Execute(sqltext, obj);


        }

        public async Task RegisterDataLogAsync(string userid, OPERATIONLOGENUM operation,
              string tableaname, string objID, object olddata, object currentdata)
        {

            DataLogObject obj = new DataLogObject();

            string s_olddata = "";
            string s_currentdata = "";

            obj.DataLogID = Utilities.GenerateId();
            obj.UserID = Int64.Parse(userid);
            obj.Date = DateTime.Now;

            switch (operation)
            {
                case OPERATIONLOGENUM.INSERT:
                    obj.Operation = "I";

                    s_currentdata = JsonConvert.SerializeObject(currentdata);

                    break;

                case OPERATIONLOGENUM.UPDATE:
                    obj.Operation = "U";

                    s_olddata = JsonConvert.SerializeObject(olddata);
                    s_currentdata = JsonConvert.SerializeObject(currentdata);

                    break;

                case OPERATIONLOGENUM.DELETE:
                    obj.Operation = "D";

                    s_olddata = JsonConvert.SerializeObject(olddata);
                    break;
            }

            obj.TableName = tableaname;
            obj.ID = Int64.Parse(objID);
            obj.LogOldData = s_olddata;
            obj.LogCurrentData = s_currentdata;

            DataLogQueryBuilder qw = new DataLogQueryBuilder();

            string sqltext =
                    qw.BuildInsertCommand("[sysdatalog]", obj, null);

            await ExecuteAsync(sqltext, obj);


        }

        public async Task<List<LocalizationTextItem>> GetLocalizationTextsAsync()
        {
            List<LocalizationTextItem> ret = new List<LocalizationTextItem>();

            string sqltext = "select * from [sysLocalizationText] ";

            ret = await this.ExecuteQueryToListAsync<LocalizationTextItem>(sqltext);

            return ret;
        }

        public async Task<bool> CheckUniqueValueForInsert(string tablename, string fieldname, string fieldvalue)
        {
            bool ret = true;

            string sqltext = $"exec CheckUniqueValueForInsert '{tablename}','{fieldname}','{fieldvalue}'";


            int cnt = await ExecuteQueryFirstAsync<int>(sqltext);

            if (cnt > 0) { ret = false; }

            return ret;
        }

        public async Task<bool> CheckUniqueValueForUpdate(string tablename, string fieldname, string fieldvalue, string pkfieldname, string pkvalue)
        {
            bool ret = true;

            string sqltext = $"exec CheckUniqueValueForUpdate " +
                $"'{tablename}','{fieldname}','{fieldvalue}','{pkfieldname}',{pkvalue}";

            int cnt = await ExecuteQueryFirstAsync<int>(sqltext);

            if (cnt > 0) { ret = false; }

            return ret;
        }

      
    }


}
