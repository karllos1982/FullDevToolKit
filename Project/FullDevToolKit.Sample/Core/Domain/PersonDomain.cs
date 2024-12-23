using FullDevToolKit.Common;
using FullDevToolKit.Core;
using MyApp.Contracts.Domains;
using MyApp.Models;
using MyApp.Contracts.Repositories;
using FullDevToolKit.Helpers;
using MyApp.Data.Repositories;

namespace MyApp.Domain
{
    public class PersonDomain : IPersonDomain
    {
        
        public PersonDomain( IContext context)
        {            
            Context = context;
            RepositorySet = new MyAppRepositorySet(context);    
        }

        public IContext Context { get; set; }

        private IMyAppRepositorySet RepositorySet { get; set; }

        public async Task<PersonResult> FillChields(PersonResult obj)
        {
            PersonContactParam param = new PersonContactParam();

            param.pPersonID = obj.PersonID;

            List<PersonContactResult> list
                = await RepositorySet.PersonContact.Search(param);

            obj.Contacts = list;

            return obj;
        }

        public async Task<PersonResult> Get(PersonParam param)
        {
            PersonResult ret = null;

            ret = await RepositorySet.Person.Read(param);

            if (ret != null) {

               await FillChields(ret); 
                   
            }

            return ret;
        }

        public async Task<List<PersonList>> List(PersonParam param)
        {
            List<PersonList> ret = null;

            ret = await RepositorySet.Person.List(param);

            return ret;
        }

        public async Task<List<PersonResult>> Search(PersonParam param)
        {
            List<PersonResult> ret = null;

            ret = await RepositorySet.Person.Search(param);

            return ret;
        }

        public async Task EntryValidation(PersonEntry obj)
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

        public async Task InsertValidation(PersonEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForInsert(RepositorySet.Person.TableName, "PersonName", obj.PersonName);

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "PersonName",
                  string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Person Name"));

            }          

            Context.Status = ret;

        }

        public async Task UpdateValidation(PersonEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
              await Context.CheckUniqueValueForUpdate(RepositorySet.Person.TableName, "PersonName",
              obj.PersonName, RepositorySet.Person.PKFieldName, obj.PersonID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "PersonName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Person Name"));
            }

           
            Context.Status = ret;

        }

        public async Task DeleteValidation(PersonEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task<PersonEntry> Set(PersonEntry model, object userid)
        {
            PersonEntry ret = null;
            OPERATIONLOGENUM operation = OPERATIONLOGENUM.INSERT;

            await EntryValidation(model);

            if (Context.Status.Success)
            {

                PersonResult old
                    = await RepositorySet.Person.Read(new PersonParam() { pPersonID = model.PersonID });

                if (old == null)
                {
                    await InsertValidation(model);

                    if (Context.Status.Success)
                    {
                        model.CreateDate = DateTime.Now;
                        if (model.PersonID == 0) { model.PersonID = FullDevToolKit.Helpers.Utilities.GenerateId(); }
                        await RepositorySet.Person.Create(model);
                    }
                }
                else
                {
                    model.CreateDate = old.CreateDate;
                    operation = OPERATIONLOGENUM.UPDATE;

                    await UpdateValidation(model);

                    if (Context.Status.Success)
                    {
                        await RepositorySet.Person.Update(model);
                    }

                }

                if (model.Contacts != null)
                {
                    foreach (PersonContactEntry u in model.Contacts )
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


                if (Context.Status.Success && userid != null)
                {
                    await RepositorySet.Person.Context
                        .RegisterDataLogAsync(userid.ToString(), operation, "Person",
                        model.PersonID.ToString(), old, model);

                    ret = model;
                }

            }

            return ret;
        }

       
        public async Task<PersonEntry> Delete(PersonEntry model, object userid)
        {
            PersonEntry ret = null;

            PersonResult old
                = await RepositorySet.Person.Read(new PersonParam() { pPersonID = model.PersonID });

            if (old != null)
            {
                await DeleteValidation(model);

                if (Context.Status.Success)
                {

                    if (model.Contacts != null)
                    {
                        foreach (PersonContactEntry u in model.Contacts)
                        {

                            await RepositorySet.PersonContact.Delete(u);

                        }
                    }

                    await RepositorySet.Person.Delete(model);

                    if (Context.Status.Success && userid != null)
                    {
                        await Context
                            .RegisterDataLogAsync(userid.ToString(),  OPERATIONLOGENUM.DELETE, "Person",
                            model.PersonID.ToString(), old, model);

                        ret = model;
                    }

                }
            }
            else
            {
                Context.Status.SetFailStatus("Error", LocalizationText.Get("Record-NotFound", Context.LocalizationLanguage).Text);
                
            }

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
