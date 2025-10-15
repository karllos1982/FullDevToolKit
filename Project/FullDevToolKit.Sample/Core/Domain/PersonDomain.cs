using FullDevToolKit.Common;
using FullDevToolKit.Core;
using MyApp.Contracts.Domains;
using MyApp.Models;
using MyApp.Contracts.Repositories;
using FullDevToolKit.Helpers;
using MyApp.Data.Repositories;
using FullDevToolKit.Sys.Models.Common;
using System.Runtime.Intrinsics.X86;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Core.Common;


namespace MyApp.Domain
{
    public class PersonDomain 
                   : BaseDomain<PersonParam, PersonEntry, PersonList, PersonResult>, IPersonDomain
    {
        
        public PersonDomain( IContext context)
        {            
            Context = context;
            RepositorySet = new MyAppRepositorySet(context);
            this.TableName = RepositorySet.Person.TableName;
        }
        

        private IMyAppRepositorySet RepositorySet { get; set; }

        public override async Task<PersonResult> FillChields(PersonResult obj)
        {
            PersonContactParam param = new PersonContactParam();

            param.pPersonID = obj.PersonID;

            List<PersonContactResult> list
                = await RepositorySet.PersonContact.ReadSearch(param);

            obj.Contacts = (list as List<PersonContactResult>);

            return obj;
        }

        public async Task<PersonResult> Get(PersonParam param)
        {
            PersonResult ret = null;

            ret = await RepositorySet.Person.ReadObject(param);

            if (ret != null) {

               await FillChields(ret); 
                   
            }

            return ret;
        }

        public async Task<List<PersonList>> List(PersonParam param)
        {
            List<PersonList> ret = null;

            ret = await RepositorySet.Person.ReadList(param);

            return ret;
        }

        public async Task<List<PersonResult>> Search(PersonParam param)
        {
            List<PersonResult> ret = null;

            ret = await RepositorySet.Person.ReadSearch(param);

            return ret;
        }

        public async Task EntryValidation_(PersonEntry obj)
        {
            ExecutionStatus ret = null;

            ret = PrimaryValidation.Execute(obj, new List<string>(), Context.LocalizationLanguage);

            ExecutionStatus aux = ContactsEntriesValidation(obj.Contacts);

            if (ret.Success)
            {
                ret = aux; 
            }
            else
            {
                if (!aux.Success)
                {
                    ret.Exceptions.AddException("Contacts", 
                        aux.Exceptions.Messages[0].Description); 
                }                
            }

            if (!ret.Success)
            {
                ret.SetFailStatus("Error", LocalizationText.Get("Validation-Error", Context.LocalizationLanguage).Text);                 
            }
          
            Context.Status = ret;

        }

        public ExecutionStatus ContactsEntriesValidation(List<PersonContactEntry> entries)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            entries = entries
                .Where(e => e.RecordState != RECORDSTATEENUM.DELETED).ToList();

            if (entries == null)
            {
                ret.Success = false;
            }
            else
            {
                if (entries.Count == 0) { ret.Success = false; }                                                                                                   
            }

            if (!ret.Success)
            {

                ret.Exceptions.AddException("Contacts",
                   "Contacts: " + LocalizationText.Get("Validation-NotNull", Context.LocalizationLanguage).Text);               
            }
            else
            {               

                foreach (PersonContactEntry item in entries)
                {
                     if (PrimaryValidation.Execute(item, new List<string>(), Context.LocalizationLanguage).Success ==false)
                     {

                        ret.SetFailStatus("Contacts", LocalizationText.Get("Validation-Error", Context.LocalizationLanguage).Text);
                        
                        break;

                     }                 
                }

            }
        
            return ret;
        }

        public override async Task InsertValidation(PersonEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForInsert(RepositorySet.Person.TableName, "ContactName", obj.PersonName);

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "ContactName",
                  string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Person Name"));

            }          

            Context.Status = ret;

        }

        public override async Task UpdateValidation(PersonEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
              await Context.CheckUniqueValueForUpdate(RepositorySet.Person.TableName, "ContactName",
              obj.PersonName, RepositorySet.Person.PKFieldName, obj.PersonID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "ContactName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Person Name"));
            }

           
            Context.Status = ret;

        }

        public override async Task DeleteValidation(PersonEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task<PersonEntry> Set(PersonEntry model, object userid)
        {
            PersonEntry ret = null;

            if (model.PersonID == 0)
            {
                model.PersonID = Utilities.GenerateId();
            }
            this.PKValue = model.PersonID.ToString();

            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await RepositorySet.Person.ReadObject(new PersonParam()
                             { pPersonID = model.PersonID });
                      }
                      ,
                      async (model) =>
                      {
                          await RepositorySet.Person.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await RepositorySet.Person.Update(model);
                      }
                      ,
                      async (model) =>
                      {
                          return ContactsEntriesValidation(model.Contacts);
                      }
                      ,
                      async (model) =>
                      {

                          if (model.Contacts != null)
                          {
                              foreach (PersonContactEntry u in model.Contacts)
                              {
                                  if (u.RecordState != RECORDSTATEENUM.NONE)
                                  {
                                      u.PersonID = model.PersonID;

                                      if (u.RecordState == RECORDSTATEENUM.ADD)
                                      {
                                          await RepositorySet.PersonContact.Create(u);
                                      }

                                      if (u.RecordState == RECORDSTATEENUM.EDITED)
                                      {
                                          await RepositorySet.PersonContact.Update(u);
                                      }

                                      if (u.RecordState == RECORDSTATEENUM.DELETED)
                                      {
                                          await RepositorySet.PersonContact.Delete(u);
                                      }

                                  }
                              }
                          }
                      }
                  );

            return ret;
        }

        public async Task<PersonEntry> Remove(PersonEntry model, object userid)
        {
            PersonEntry ret = null;
            this.PKValue = model.PersonID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                            await RepositorySet.Person.ReadObject(new PersonParam()
                            { pPersonID = model.PersonID });
                      }
                      ,
                      async (model) =>
                      {
                          await RepositorySet.Person.Delete(model);
                      }
                      ,
                      async (model) =>
                      {
                          if (model.Contacts != null)
                          {
                              PersonContactEntry aux = null; 

                              foreach (PersonContactResult u in model.Contacts)
                              {
                                  aux = new PersonContactEntry();
                                  BaseModel.ConvertTo(u, aux);
                                  await RepositorySet.PersonContact.Delete(aux);

                              }
                          }
                      }
                  );

            return ret;
        }


        public PersonContactEntry ContactEntryValidation(PersonContactEntry entry)
        {
            PersonContactEntry ret = entry;

            Context.Status 
                = PrimaryValidation.Execute(entry, new List<string>(), Context.LocalizationLanguage);

            if (!Context.Status.Success)
            {
                Context.Status.Exceptions.AddException(0, "Contacts",
                    LocalizationText.Get("Validation-Error", Context.LocalizationLanguage).Text);
                                   
            }

            return ret;

        }

    }
}
