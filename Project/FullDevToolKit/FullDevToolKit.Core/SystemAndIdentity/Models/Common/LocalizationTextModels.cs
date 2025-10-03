using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;

namespace FullDevToolKit.Sys.Models.Common
{

    public class LocalizationTextBaseModel: BaseModel
    {
        [PrimaryValidationConfig("LocalizationTextID", "Text ID", FieldType.NUMERIC, false, 0)]
        public long LocalizationTextID { get; set; }

        [PrimaryValidationConfig("Language", "Language", FieldType.TEXT, false, 5)]
        public string Language { get; set; } = string.Empty;

        [PrimaryValidationConfig("Name", "Name", FieldType.TEXT, false, 50)]
        public string Name { get; set; } = string.Empty;

        [PrimaryValidationConfig("Code", "Code", FieldType.TEXT, false, 10)]
        public string Code { get; set; } = string.Empty;

        [PrimaryValidationConfig("Text", "Text", FieldType.TEXT, false, 255)]
        public string Text { get; set; } = string.Empty;
    }

    public class LocalizationTextParam
    {
        public LocalizationTextParam()
        {
            pLocalizationTextID= 0;
            pLanguage = "";
            pCode = "";
            pName = "";
            pText = ""; 
        }

        public long pLocalizationTextID { get; set; }

        public string pLanguage { get; set; }

        public string pName { get; set; }

        public string pCode { get; set; }

        public string pText { get; set; }

        
    }

    public class LocalizationTextEntry: LocalizationTextBaseModel
    {
        public LocalizationTextEntry()
        {
            
        }

        public LocalizationTextEntry(LocalizationTextResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);
        }        

    }

    public class LocalizationTextList: LocalizationTextBaseModel
    {
                
    }

    public class LocalizationTextResult: LocalizationTextBaseModel
    {

   

    }

}
