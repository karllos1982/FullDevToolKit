using FullDevToolKit.Helpers;
using FullDevToolKit.Common;
using FullDevToolKit.Core.Common;

namespace FullDevToolKit.Sys.Models.Identity
{

    public class InstanceBaseModel: BaseModel
    {

        [PrimaryValidationConfig("InstanceID", "Instance ID", FieldType.NUMERIC, false, 0)]
        public long InstanceID { get; set; }

        [PrimaryValidationConfig("InstanceTypeName", "Instance Type Name", FieldType.TEXT, false, 50)]
        public string InstanceTypeName { get; set; } = string.Empty;

        [PrimaryValidationConfig("InstanceName", "Instance Name", FieldType.TEXT, false, 100)]
        public string InstanceName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }
    }

    public class InstanceParam: BaseParam
    {
        public InstanceParam()
        {        
            pInstanceID = 0;
            pInstanceName = "";
            pInstanceTypeName = "";
        }


        public long pInstanceID { get; set; }
        
        public string pInstanceTypeName { get; set; }

        public string pInstanceName { get; set; }

        
    }

    public class InstanceEntry: InstanceBaseModel
    {

        public InstanceEntry()
        {
            
        }

        public InstanceEntry(InstanceResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);
        }

    }


    public class InstanceList: InstanceBaseModel
    {        

    }

    public class InstanceResult: InstanceBaseModel
    {
               

    }

}
