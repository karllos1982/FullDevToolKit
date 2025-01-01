using FullDevToolKit.Common;
using MyApp.Models;
using MyApp.Proxys;
using FullDevToolKit.Sys.Models.Identity
using FullDevToolKit.Helpers;

namespace MyApp.ViewModel
{
    public class ClientViewModel : BaseViewModel
    {

        private MyAppProxys _Proxys;
        private DataCacheProxy _cache;

        public ContactSummaryManager ContactSummary;

        public ClientViewModel(MyAppProxys service, DataCacheProxy cache,
            UserAuthenticated user)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            ContactSummary = new ContactSummaryManager(); 
            this.InitializeView(user);
        }

        UserAuthenticated _user;

        public ClientResult result = new ClientResult();
        public ClientParam param = new ClientParam() { };
        public List<ClientResult> searchresult = new List<ClientResult>();

        public DefaultLocalization texts = null;

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                new ExceptionMessage("ClientName",""),
                new ExceptionMessage("Email",""),
                new ExceptionMessage("PhoneNamber",""),
                new ExceptionMessage("Contacts","")
            };

        }

        public override async Task InitializeModels()
        {

            await ClearSummaryValidation();

            this.texts = new DefaultLocalization();
            this.texts.FillTexts(await _cache.ListLocalizationTexts(), _user.LocalizationLanguage);

            ContactSummary.ClearSummaryValidation();
        }


        public override async Task Set()
        {
            ExecutionStatus = new ExecutionStatus(true);

            ClientEntry entry = new ClientEntry(result);

            ClientEntry ret = await _Proxys.Client.Set(entry);

            if (ret != null)
            {
                ExecutionStatus.Returns = ret;
            }
            else
            {
                ExecutionStatus.ExceptionMessages = _Proxys.Client.GetExceptionMessages(ref ExecutionStatus.Error);
                ExecutionStatus.Status = false;
                this.ShowSummaryValidation(ExecutionStatus.ExceptionMessages);
            }


        }

        public override async Task Get(object id)
        {

            ExecutionStatus = new ExecutionStatus(true);

            result = await _Proxys.Client.Get(id.ToString());

            if (result == null)
            {
                ExecutionStatus.ExceptionMessages = _Proxys.Client.GetExceptionMessages(ref ExecutionStatus.Error);
                ExecutionStatus.Status = false;
            }

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
            result = new ClientResult();
            result.CreateDate = DateTime.Now;
            result.ClientID = Utilities.GenerateId();
            result.Contacts = new List<ClientContactsResult>();

        }

        public override async Task Search()
        {

            ExecutionStatus = new ExecutionStatus(true);

            searchresult = await _Proxys.Client.Search(param);

            if (searchresult == null)
            {
                ExecutionStatus.ExceptionMessages = _Proxys.Client.GetExceptionMessages(ref ExecutionStatus.Error);
                ExecutionStatus.Status = false;
            }

        }

        // contacts functions        
        public ClientContactsResult contact = new ClientContactsResult();
        public string contactstate = ""; 

        public void GetContactToEdit(Int64 id)
        {
            ContactSummary.ClearSummaryValidation();  

            contact = result.Contacts.Where(c => c.ClientContactID == id).FirstOrDefault();
            contactstate = "Contact Editing";
        }      

        public async Task InitNewContact()
        {
             await ClearSummaryValidation();
            ContactSummary.ClearSummaryValidation();

            contactstate = "Inserting Contact";
            contact = new ClientContactsResult();
            contact.RecordState = RECORDSTATEENUM.ADD;
            contact.ClientContactID = 0; 
        }

        public async Task SaveContact()
        {
            ExecutionStatus = new ExecutionStatus(true);

            ContactSummary.ClearSummaryValidation();

            ClientContactsEntry ret 
                    = await _Proxys.Client.ContactEntryValidation(new ClientContactsEntry(contact));

            if (ret != null)
            {

                if (result.Contacts == null)
                {
                    result.Contacts = new List<ClientContactsResult>();
                }

                if (contact.RecordState == RECORDSTATEENUM.NONE)
                {
                    contact.RecordState = RECORDSTATEENUM.EDITED;
                }

                if (contact.RecordState == RECORDSTATEENUM.ADD)
                {
                    if (contact.ClientContactID == 0)
                    {
                        contact.ClientContactID= Utilities.GenerateId();

                        result.Contacts.Add(contact);
                    }
                }
            }
            else
            {
                ExecutionStatus.ExceptionMessages = _Proxys.Client.GetExceptionMessages(ref ExecutionStatus.Error);
                ExecutionStatus.Status = false;
                ContactSummary.ShowSummaryValidation(ExecutionStatus.ExceptionMessages);
            }


        }

        public async Task RemoveContact(Int64 id)
        {
            ExecutionStatus = new ExecutionStatus(true);

            ContactSummary.ClearSummaryValidation();

            contact = result.Contacts.Where(c => c.ClientContactID == id).FirstOrDefault();            

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
            ExecutionStatus = new ExecutionStatus(true);
           
            contact = result.Contacts.Where(c => c.ClientContactID == id).FirstOrDefault();

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


