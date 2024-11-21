using FullDevToolKit.Common;

namespace FullDevToolKit.Sys.Models.Identity
{
    public class UserInstancesParam
    {

        public UserInstancesParam()
        {
            pUserInstanceID = 0;
            pUserID = 0;
            pInstanceID = 0;
        }

        public long pUserInstanceID { get; set; }

        public long pUserID { get; set; }

        public long pInstanceID { get; set; }
    }

    public class UserInstancesEntry
    {
        public UserInstancesEntry()
        {
                
        }

        public UserInstancesEntry(UserInstancesResult fromobj)
        {
            UserInstanceID = fromobj.UserInstanceID;   
            UserID = fromobj.UserID;
            InstanceID = fromobj.InstanceID;

        }

        public long UserInstanceID { get; set; }

        public long UserID { get; set; }

        public long InstanceID { get; set; }      

        public RECORDSTATEENUM RecordState { get; set; }
    }


    public class UserInstancesList
    {
        public long UserInstanceID { get; set; }

        public long InstanceID { get; set; }

        public string InstanceName { get; set; } = string.Empty;    

    }

    public class UserInstancesResult
    {
        public long UserInstanceID { get; set; }

        public long UserID { get; set; }

        public string UserName { get; set; } = string.Empty;    

        public long InstanceID { get; set; }

        public string InstanceName { get; set; } = string.Empty;

        public RECORDSTATEENUM RecordState { get; set; }

    }

}
