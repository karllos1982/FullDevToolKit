
namespace FullDevToolKit.Sys.Models.Common
{
    public class DataLogParam
    {
        public long pDataLogID { get; set; }

        public long pUserID { get; set; }
        
        public string pEmail { get; set; }

        public DateTime pDate_Start { get; set; }

        public DateTime pData_End { get; set; }

        public string pOperation { get; set; }

        public string pTableName { get; set; }

        public bool SearchByDate { get; set; }

        public long pID { get; set; }

        public DataLogParam()
        {
            pDataLogID = 0;
            pUserID = 0;
            pID = 0;
            pEmail = "";
            pOperation = "0";
            pTableName = "0";
            SearchByDate = false;
        }

    }

    public class DataLogEntry
    {
        public DataLogEntry()
        {
            
        }

        public DataLogEntry(DataLogResult fromobj)
        {
            DataLogID = fromobj.DataLogID;  
            UserID = fromobj.UserID;
            Date = fromobj.Date;
            Operation = fromobj.Operation;  
            TableName = fromobj.TableName;
            ID = fromobj.ID;
            LogOldData = fromobj.LogOldData;
            LogCurrentData  = fromobj.LogCurrentData;
        }

        public long DataLogID { get; set; }

        public long UserID { get; set; }        

        public DateTime Date { get; set; }

        public string Operation { get; set; } = string.Empty;

        public string TableName { get; set; } = string.Empty;

        public long ID { get; set; }

        public string LogOldData { get; set; } = string.Empty;

        public string LogCurrentData { get; set; } = string.Empty;

    }

    public class DataLogTimelineModel
    {

        public long LogID { get; set; }

        public string Operation { get; set; } = string.Empty;

        public string OperationText
        {
            get
            {
                string aux = "";

                switch (Operation)
                {
                    case "I":
                        aux = "INSERT";
                        break;

                    case "U":
                        aux = "UPDATE";
                        break;

                    case "D":
                        aux = "DELETE";
                        break;

                }

                return aux;
            }

        }

        public string UserEmail { get; set; } = string.Empty;

        public DateTime Date { get; set; }

    }

    public class TipoOperacaoValueModel
    {
        public string Value { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;
    }

    public class TabelasValueModel
    {
        public string Value { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;
    }

    public class DataLogItem
    {
        public string ItemName { get; set; } = string.Empty;

        public string ItemValue { get; set; } = string.Empty;

        public bool IsDifferent { get; set; }
    }

    public class DataLogList
    {
        public long DataLogID { get; set; }

        public long UserID { get; set; }

        public DateTime Date { get; set; }

        public string Operation { get; set; } = string.Empty;

        public string TableName { get; set; } = string.Empty;

        public long ID { get; set; }
      
    }

    public class DataLogResult
    {
        public long DataLogID { get; set; }

        public long UserID { get; set; }

        public string Email { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Operation { get; set; } = string.Empty;

        public string OperationText
        {
            get
            {
                string aux = "";

                switch (Operation)
                {
                    case "I":
                        aux = "INSERT";
                        break;

                    case "U":
                        aux = "UPDATE";
                        break;

                    case "D":
                        aux = "DELETE";
                        break;

                }

                return aux;
            }

        }

        public string TableName { get; set; } = string.Empty;

        public long ID { get; set; }

        public string LogOldData { get; set; } = string.Empty;

        public string LogCurrentData { get; set; } = string.Empty;
    }

}
