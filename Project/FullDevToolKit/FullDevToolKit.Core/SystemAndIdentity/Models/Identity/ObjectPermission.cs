using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;

namespace FullDevToolKit.Sys.Models.Identity
{
 
    public class ObjectPermssionBaseModel: BaseModel
    {
        public long ObjectPermissionID { get; set; }

        [PrimaryValidationConfig("ObjectName", "Object Name", FieldType.TEXT, false, 50)]
        public string ObjectName { get; set; } = string.Empty;

        [PrimaryValidationConfig("ObjectCode", "Object Code", FieldType.TEXT, false, 25)]
        public string ObjectCode { get; set; } = string.Empty;
    }


    public class ObjectPermissionParam
    {
        public ObjectPermissionParam()
        {
            pObjectCode = "";
            pObjectName = "";
            pObjectPermissionID = 0; 
        }

        public long pObjectPermissionID { get; set; }

        public string pObjectName { get; set; }

        public string pObjectCode{ get; set; }

    }

    public class ObjectPermissionEntry: ObjectPermssionBaseModel
    {
        public ObjectPermissionEntry()
        {
            
        }

        public ObjectPermissionEntry(ObjectPermissionResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);
        }
        
    }

    public class ObjectPermissionList: ObjectPermssionBaseModel
    {

    }

    public class ObjectPermissionResult: ObjectPermssionBaseModel
    {
  

    }

}
