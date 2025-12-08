using FullDevToolKit.Core.Common;

namespace FullDevToolKit.Sys.Models.Identity
{

    public class SessionLogBaseModel: BaseModel
    {
        public long SessionLogID { get; set; }

        public long UserID { get; set; }

        public DateTime Date { get; set; }

        public string IP { get; set; } = string.Empty;

        public string BrowserName { get; set; } = string.Empty;

        public DateTime? DateLogout { get; set; }
    }

    public class SessionLogParam: BaseParam
    {

        public SessionLogParam()
        {
            pSessionLogID = 0;
            pUserID = 0;
            pEmail = "";
            SearchByDate = false; 
        }

        public long pSessionLogID { get; set; }

        public long pUserID { get; set; }

        public string pEmail { get; set; }

        public DateTime pDate_Start { get; set; }

        public DateTime pData_End { get; set; }

        public bool SearchByDate { get; set; }

    }

    public class SessionLogEntry : SessionLogBaseModel
    {
        public SessionLogEntry()
        {
            
        }

        public SessionLogEntry(SessionLogResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);
        }
      
    }

    public class SessionLogList : SessionLogBaseModel
    {      

    }

    public class SessionLogResult : SessionLogBaseModel
    {
        

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
               
    }



}
