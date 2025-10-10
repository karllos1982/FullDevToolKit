using FullDevToolKit.Core.Common;
using FullDevToolKit.Helpers;

namespace MyApp.Models
{
    public class PersonParam
    {
        public PersonParam()
        {
            pEmail = "";
            pPersonName = "";
            pPersonID = 0;
        }

        public Int64 pPersonID { get; set; }

        public string pPersonName { get; set; }

        public string pEmail { get; set; }

    }

    public class PersonBaseModel: BaseModel
    {
        public long PersonID { get; set; }

        [PrimaryValidationConfig("PersonName", "Person Name", FieldType.TEXT, false, 50)]
        public string PersonName { get; set; }

        [PrimaryValidationConfig("Email", "E-mail", FieldType.EMAIL, false, 255)]
        public string Email { get; set; }

        [PrimaryValidationConfig("PhoneNamber", "Phone Number", FieldType.PHONENUMBER, false, 15)]
        public string PhoneNamber { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }
        
    }

    public class PersonEntry: PersonBaseModel
    {

        public PersonEntry()
        {
            
        }

        public PersonEntry(PersonResult fromobj)
        {
            BaseModel.ConvertTo(fromobj, this);

            this.Contacts = new List<PersonContactEntry>(); 

            foreach (PersonContactResult c in fromobj.Contacts)
            {
                Contacts.Add(new PersonContactEntry(c));
            }

        }

        public List<PersonContactEntry> Contacts { get; set; }


    }

    public class PersonList: PersonBaseModel
    {

    }

    public class PersonResult: PersonBaseModel
    {

        public List<PersonContactResult> Contacts { get; set; }

    }
}
