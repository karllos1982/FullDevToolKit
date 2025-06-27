
namespace FullDevToolKit.Sys.Models.Common
{

    public class ExceptionLogParam
    {
        public long pExceptionLogID { get; set; }

        public long pUserID { get; set; }        

        public DateTime pDate_Start { get; set; }

        public DateTime pData_End { get; set; }

        public string pOrigin { get; set; }        

        public bool SearchByDate { get; set; }        

        public ExceptionLogParam()
        {
            pExceptionLogID = 0;
            pUserID = 0;            
            pOrigin = "";            
            SearchByDate = false;
        }

    }

    public class ExceptionLogEntry
    {

        public ExceptionLogEntry()
        {

        }

        public ExceptionLogEntry(ExceptionLogResult fromobj)
        {
            this.ExceptionLogID= fromobj.ExceptionLogID;
            this.UserID= fromobj.UserID;
            this.Date = fromobj.Date;
            this.Origin = fromobj.Origin;            
            this.TargetSite = fromobj.TargetSite;
            this.ErrMessage = fromobj.ErrMessage;
            this.StackTrace = fromobj.StackTrace;
            this.ClientIP = fromobj.ClientIP;
        }

        public long ExceptionLogID { get; set; }

        public string UserID { get; set; }
        
        public DateTime Date { get; set; }

        public string Origin { get; set; }

        public string TargetSite { get; set; }

        public string ErrMessage { get; set; }

        public string StackTrace { get; set; }

        public string ClientIP { get; set; }
    }

    public class ExceptionLogList
    {
       
        public long ExceptionLogID { get; set; } 

        public DateTime Date { get; set; }
       
    }


    public class ExceptionLogResult
    {
        public ExceptionLogResult()
        {

        }

        public long ExceptionLogID { get; set; }

        public string UserID { get; set; }

		public string Email { get; set; } = string.Empty;

		public DateTime Date { get; set; }

        public string Origin { get; set; }

        public string TargetSite { get; set; }

        public string ErrMessage { get; set; }

        public string StackTrace { get; set; }

        public string ClientIP { get; set; }
    }
}
