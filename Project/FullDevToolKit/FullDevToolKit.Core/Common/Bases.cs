using System;
using System.Collections;
using System.Text.Json.Serialization;


namespace FullDevToolKit.Common
{

    public class DefaultGetParam
    {
        public DefaultGetParam(string idvalue)
        {
            id = idvalue.Trim();
        }

        public string id { get; set; }
    }

    public class ExceptionMessage
    {
        public string Key { get; set; }

        public string Description { get; set; }

        public ExceptionMessage(string key, string description)
        {
            this.Key = key;
            this.Description = description;
        }
    }

  
    public class ExecutionExceptions
    {
        public ExecutionExceptions() 
        {
            Messages = new List<ExceptionMessage> { };
        }

        public List<ExceptionMessage> Messages { get; set; }

        public void AddException(string key, string description)
        {
            Messages.Add(new ExceptionMessage(key, description));         
        }

        public ArrayList InnerToArrayList()
        {
            ArrayList ret = new ArrayList();

            foreach (ExceptionMessage ie in this.Messages)
            {
                ret.Add(ie.Description);
            }
            return ret;
        }

        public string InnerToString()
        {
            string ret = "";

            foreach (ExceptionMessage ie in this.Messages)
            {
                if (ret == "")
                {
                    ret = ie.Description;
                }
                else
                {
                    ret = ret + ", " + ie.Description;
                }


            }
            return ret;
        }
    }

    public class ExecutionStatus
    {
        public ExecutionStatus()
        {

        }

        public ExecutionStatus(bool initialstatus)
        {
            this.Success = initialstatus;
            Exceptions = new ExecutionExceptions { };
        }

        public bool Success { get; set; }

        public ExecutionExceptions? Exceptions { get; set; }

        public object? Returns;

        public void Reset()
        {
            Success = false;
            Exceptions = null;
            Returns = null; 
        }          


    }

    public class APIResponse<TData>
    {
        private readonly int _code;

        [JsonConstructor]
        public APIResponse() => _code = 200;

        public APIResponse(           
           int code = 200,
           ExecutionExceptions? exceptions = null)
        {            
            Exceptions = exceptions;
            _code = code;
        }

        public APIResponse(
            TData? data,
            int code = 200,
            ExecutionExceptions? exceptions = null)
        {
            Data = data;
            Exceptions = exceptions;
            _code = code;
        }

        public TData? Data { get; set; }
        public ExecutionExceptions? Exceptions { get; set; }

        [JsonIgnore]
        public bool IsSuccess
            => _code is >= 200 and <= 299;
    }
    
  
    public enum RECORDSTATEENUM
    {
        NONE = 0,
        ADD = 1,
        EDITED = 2,
        DELETED = 3
    }

    public enum OPERATIONLOGENUM
    {
        INSERT = 1,
        UPDATE = 2,
        DELETE = 3
    }

    public class UserStatus
    {

    }

    public class AuthToken
    {
        public string TokenValue { get; set; }

        public DateTime ExpiresDate { get; set; }
    }


    public class AuthParams
    {
        public string ApplicationID { get; set; }

        public string ClientID { get; set; }

        public string SecretKey { get; set; }

        public string DeviceName { get; set; }

        public string IP { get; set; }

        public string UserIdentity { get; set; }

        public string UserPassword { get; set; }

    }

    public class SessionParams
    {
        public string SessionID { get; set; }

        public string AccessToken { get; set; }

    }

    public class DataLogObject
    {
        public Int64 DataLogID { get; set; }

        public Int64 UserID { get; set; }

        public DateTime Date { get; set; }

        public string Operation { get; set; }

        public string TableName { get; set; }

        public Int64 ID { get; set; }

        public string LogOldData { get; set; }

        public string LogCurrentData { get; set; }
    }

    public class SourceConfig
    {
        public string SourceName { get; set; }

        public string  SourceValue { get; set; }

    }

    public enum PERMISSION_CHECK_ENUM
    {
        READ = 1,
        SAVE = 2,
        DELETE = 3

    }

    public enum PERMISSION_STATE_ENUM
    {
        NONE = 0,
        ALLOWED = 1,
        DENIED = 2

    }

    public interface IUserPermissions
    {
       

    }

    public class UserPermissions : IUserPermissions
    {
        public Int64 PermissionID { get; set; }

        public Int64 ObjectPermissionID { get; set; }

        public string ObjectCode { get; set; }

        public int ReadStatus { get; set; }

        public int SaveStatus { get; set; }

        public int DeleteStatus { get; set; }

        public string TypeGrant { get; set; }

    }

    public class PermissionsState
    {
        public bool AllowRead { get; set; }

        public bool AllowSave { get; set; }

        public bool AllowDelete { get; set; }

        public PermissionsState(bool allowread, bool allowsave, bool allowdelete)
        {
            AllowRead = allowread;
            AllowSave = allowsave;  
            AllowDelete = allowdelete;  
        }

    }

}
