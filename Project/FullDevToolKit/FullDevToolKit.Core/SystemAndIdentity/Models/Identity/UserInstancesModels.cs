using FullDevToolKit.Common;
using FullDevToolKit.Core.Common;

namespace FullDevToolKit.Sys.Models.Identity
{

    public class UserInstancesBaseModel:BaseModel
    {
        public long UserInstanceID { get; set; }

        public long UserID { get; set; }

        public long InstanceID { get; set; }

        public RECORDSTATEENUM RecordState { get; set; }
    }

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

    public class UserInstancesEntry : UserInstancesBaseModel
    {
        public UserInstancesEntry()
        {
                
        }

        public UserInstancesEntry(UserInstancesResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);

        }
       
    }


    public class UserInstancesList : UserInstancesBaseModel
    {
          

    }

    public class UserInstancesResult : UserInstancesBaseModel
    {
   
        public string UserName { get; set; } = string.Empty;     

        public string InstanceName { get; set; } = string.Empty;

    }

}
