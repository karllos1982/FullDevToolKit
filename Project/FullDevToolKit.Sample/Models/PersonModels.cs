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

    public class PersonEntry
    {

        public PersonEntry()
        {

        }

        public PersonEntry(PersonResult fromobj)
        {
            PersonID = fromobj.PersonID;
            PersonName = fromobj.PersonName;
            Email = fromobj.Email;
            PhoneNamber = fromobj.PhoneNamber;
            IsActive = fromobj.IsActive;
            CreateDate = fromobj.CreateDate;
            Contacts = new List<PersonContactEntry>();

            foreach (PersonContactResult c in fromobj.Contacts)
            {
                Contacts.Add(new PersonContactEntry(c));
            }

        }

        public Int64 PersonID { get; set; }

        [PrimaryValidationConfig("PersonName", "Person Name", FieldType.TEXT, false, 50)]
        public string PersonName { get; set; }

        [PrimaryValidationConfig("Email", "E-mail", FieldType.EMAIL, false, 255)]
        public string Email { get; set; }

        [PrimaryValidationConfig("PhoneNamber", "Phone Number", FieldType.PHONENUMBER, false, 15)]
        public string PhoneNamber { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public List<PersonContactEntry> Contacts { get; set; }


    }

    public class PersonList
    {
        public Int64 PersonID { get; set; }

        public string PersonName { get; set; } = string.Empty;

    }

    public class PersonResult
    {
        public Int64 PersonID { get; set; }

        public string PersonName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNamber { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public List<PersonContactResult> Contacts { get; set; } = new List<PersonContactResult>();  
    }
}
