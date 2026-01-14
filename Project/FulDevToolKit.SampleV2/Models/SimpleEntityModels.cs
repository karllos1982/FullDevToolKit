using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;

namespace MyApp.Models
{
    public class SimpleEntityParam : BaseParam
    {
        public SimpleEntityParam()
        {
            pSimpleEntityID = 0;
            pSimpleEntityName = "";
            pSimpleEntityDescription = "";
        }

        public long pSimpleEntityID { get; set; }

        public string pSimpleEntityName { get; set; }

        public string pSimpleEntityDescription { get; set; }
    }

    public class SimpleEntityBaseModel : BaseModel
    {
        public long SimpleEntityID { get; set; }

        [PrimaryValidationConfig("SimpleEntityName", "Simple Entity Name", FieldType.TEXT, false, 50)]
        public string SimpleEntityName { get; set; }

        [PrimaryValidationConfig("SimpleEntityDescription", "Simple Entity Description", FieldType.TEXT, false, 100)]
        public string SimpleEntityDescription { get; set; }

        public DateTime? SimpleEntityDate { get; set; }
    }

    public class SimpleEntityEntry : SimpleEntityBaseModel
    {
        public SimpleEntityEntry()
        {
        }

        public SimpleEntityEntry(SimpleEntityResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);
        }
    }

    public class SimpleEntityList : SimpleEntityBaseModel
    {
    }

    public class SimpleEntityResult : SimpleEntityBaseModel
    {
    }
}