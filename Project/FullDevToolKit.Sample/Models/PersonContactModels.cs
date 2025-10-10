using FullDevToolKit.Common;
using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;


namespace MyApp.Models
{
    public class PersonContactParam
    {
        public PersonContactParam()
        {
			pContactName = "";
            pEmail = "";

        }

        public Int64 pPersonContactID { get; set; }

        public Int64 pPersonID { get; set; }

        public string pContactName { get; set; }

        public string pEmail { get; set; }

    }

    public class PersonContactBaseModel: BaseModel
    {
        public Int64 PersonContactID { get; set; }

        public Int64 PersonID { get; set; }

        [PrimaryValidationConfig("ContactName", "Contact Name", FieldType.TEXT, false, 50)]
        public string ContactName { get; set; }

        [PrimaryValidationConfig("Email", "E-mail", FieldType.EMAIL, false, 255)]
        public string Email { get; set; }

        [PrimaryValidationConfig("CellPhoneNumber", "CellPhoneNumber", FieldType.CELLPHONENUMBER, false, 15)]
        public string CellPhoneNumber { get; set; }

        public RECORDSTATEENUM RecordState { get; set; }
    }

    public class PersonContactEntry: PersonContactBaseModel
    {
        public PersonContactEntry()
        {

        }

        public PersonContactEntry(PersonContactBaseModel fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);
        }              

    }

    public class PersonContactList : PersonContactBaseModel
    {        

    }

    public class PersonContactResult : PersonContactBaseModel
    {

     
    }

}
