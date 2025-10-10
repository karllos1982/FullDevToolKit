
using FullDevToolKit.Core.Common;

namespace FullDevToolKit.Sys.Models.Common
{
    public class ExceptionLogBaseModel: BaseModel
    {
        public long ExceptionLogID { get; set; }

        public string UserID { get; set; }

        public DateTime Date { get; set; }

        public string Origin { get; set; }

        public string TargetSite { get; set; }

        public string ErrMessage { get; set; }

        public string StackTrace { get; set; }

        public string ClientIP { get; set; }
    }

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

    public class ExceptionLogEntry: ExceptionLogBaseModel
    {

        public ExceptionLogEntry()
        {

        }

        public ExceptionLogEntry(ExceptionLogResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);
        }

        
    }

    public class ExceptionLogList: ExceptionLogBaseModel
    {
              
       
    }


    public class ExceptionLogResult: ExceptionLogBaseModel
    {
        public ExceptionLogResult()
        {

        }


        public string Email { get; set; }

    }
}
