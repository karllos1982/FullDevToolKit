using FullDevToolKit.Common;
using MyApp.Models;
using MyApp.Proxys;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Helpers;

namespace MyApp.ViewModel
{
    public class PersonViewModel : BaseViewModel
    {

        private MyAppProxy _proxys;
        private DataCacheProxy _cache;

        public ContactSummaryManager ContactSummary;

        public PersonViewModel(MyAppProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
			_proxys = service;
            _cache = cache;
            this.InitializeView(user);
            _proxys.Init(http, serviceurl, token);
            ContactSummary = new ContactSummaryManager(); 
            
        }

        UserAuthenticated _user;

        public PersonResult result = new PersonResult();
        public PersonParam param = new PersonParam() { };
        public List<PersonResult> searchresult = new List<PersonResult>();

        public DefaultLocalization texts = null;

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                new ExceptionMessage("PersonName",""),
                new ExceptionMessage("Email",""),
                new ExceptionMessage("PhoneNamber",""),
                new ExceptionMessage("Contacts","")
            };

        }

        public override async Task InitializeModels()
        {

            await ClearSummaryValidation();

            ContactSummary.ClearSummaryValidation();
        }


        public override async Task Set()
        {
			ServiceStatus = new ExecutionStatus(true);

            PersonEntry entry = new PersonEntry(result);

			APIResponse<PersonEntry> ret
			  = await _proxys.Person.Set(entry);

			SetResult<PersonEntry>(ret, ref entry, ref ServiceStatus);			

        }

		public override async Task Get(object id)
		{
			ServiceStatus = new ExecutionStatus(true);

			APIResponse<PersonResult> ret
				= await _proxys.Person.Get(id.ToString());

			SetResult<PersonResult>(ret, ref result, ref ServiceStatus);

		}
		

        public override void BackToSearch()
        {
            this.BaseBack();

        }

        public override void InitEdit()
        {
            this.BaseInitEdit();

        }

        public override void InitNew()
        {
            this.BaseInitNew();
            result = new PersonResult();
            result.CreateDate = DateTime.Now;
            result.PersonID = Utilities.GenerateId();
            result.Contacts = new List<PersonContactResult>();

        }

		public override async Task Search()
		{
			ServiceStatus = new ExecutionStatus(true);

			APIResponse<List<PersonResult>> ret
			   = await _proxys.Person.Search(param);

			SetResult<List<PersonResult>>(ret, ref searchresult, ref ServiceStatus);

		}
		

        // contacts functions        
        public PersonContactResult contact = new PersonContactResult();
        public string contactstate = ""; 

        public void GetContactToEdit(Int64 id)
        {
            ContactSummary.ClearSummaryValidation();  

            contact = result.Contacts.Where(c => c.PersonContactID == id).FirstOrDefault();
            contactstate = "Contact Editing";
        }      

        public async Task InitNewContact()
        {
             await ClearSummaryValidation();
            ContactSummary.ClearSummaryValidation();

            contactstate = "Inserting Contact";
            contact = new PersonContactResult();
            contact.RecordState = RECORDSTATEENUM.ADD;
            contact.PersonContactID = 0; 
        }

        public async Task SaveContact()
        {
			ServiceStatus = new ExecutionStatus(true);

			ContactSummary.ClearSummaryValidation();
            PersonContactEntry obj = new PersonContactEntry(contact);
			APIResponse<PersonContactEntry> ret
			        = await _proxys.Person.ContactEntryValidation(obj);
			
            if (ret.IsSuccess)
            {

                if (result.Contacts == null)
                {
                    result.Contacts = new List<PersonContactResult>();
                }

                if (contact.RecordState == RECORDSTATEENUM.NONE)
                {
                    contact.RecordState = RECORDSTATEENUM.EDITED;
                }

                if (contact.RecordState == RECORDSTATEENUM.ADD)
                {
                    if (contact.PersonContactID == 0)
                    {
                        contact.PersonContactID= Utilities.GenerateId();

                        result.Contacts.Add(contact);
                    }
                }
            }
            else
            {                
				SetResult<PersonContactEntry>(ret, ref obj, ref ServiceStatus, ContactSummary);
			}

        }

        public async Task RemoveContact(Int64 id)
        {
            ServiceStatus = new ExecutionStatus(true);

            ContactSummary.ClearSummaryValidation();

            contact = result.Contacts.Where(c => c.PersonContactID == id).FirstOrDefault();            

            if (contact != null)
            {              

                if (contact.RecordState == RECORDSTATEENUM.NONE)
                {
                    contact.RecordState = RECORDSTATEENUM.DELETED;
                }
                
                if (contact.RecordState == RECORDSTATEENUM.EDITED)
                {
                    contact.RecordState = RECORDSTATEENUM.DELETED;
                }

                if (contact.RecordState == RECORDSTATEENUM.ADD)
                {
                    result.Contacts.Remove(contact); 
                }
            }            

        }

        public async Task UnRemoveContact(Int64 id)
        {
			ServiceStatus = new ExecutionStatus(true);
           
            contact = result.Contacts.Where(c => c.PersonContactID == id).FirstOrDefault();

            if (contact != null)
            {

                if (contact.RecordState == RECORDSTATEENUM.DELETED)
                {
                    contact.RecordState = RECORDSTATEENUM.NONE;
                }
                
            }

        }

    }


    public class ContactSummaryManager : SummaryManager
    {
        public override void ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                new ExceptionMessage("ContactName",""),
                new ExceptionMessage("Email",""),
                new ExceptionMessage("CellPhoneNumber","")                
            };

        }
    }
}


