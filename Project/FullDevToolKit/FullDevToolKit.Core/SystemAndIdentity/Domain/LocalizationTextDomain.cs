using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using System.Reflection;
using FullDevToolKit.Sys.Data.Repositories;

namespace FullDevToolKit.Sys.Domains
{
    public class LocalizationTextDomain : ILocalizationTextDomain
    {

        public LocalizationTextDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);

        }

        public IContext Context { get; set; }

        private ISystemRepositorySet _repositories { get; set; }

        public async Task<LocalizationTextResult> FillChields(LocalizationTextResult obj)
        {
            return obj;
        }

        public async Task<LocalizationTextResult> Get(LocalizationTextParam param)
        {
            LocalizationTextResult ret = null;

            ret = await _repositories.LocalizationText.Read(param);

            return ret;
        }

        public async Task<List<LocalizationTextList>> List(LocalizationTextParam param)
        {
            List<LocalizationTextList> ret = null;

            ret = await _repositories.LocalizationText.List(param);

            return ret;
        }

        public async Task<List<LocalizationTextResult>> Search(LocalizationTextParam param)
        {
            List<LocalizationTextResult> ret = null;

            ret = await _repositories.LocalizationText.Search(param);

            return ret;
        }

        public async Task EntryValidation(LocalizationTextEntry obj)
        {
            ExecutionStatus ret = null;

            ret = PrimaryValidation.Execute(obj, new List<string>(), Context.LocalizationLanguage);

            if (!ret.Success)
            {
                ret.SetFailStatus("Error",
                   LocalizationText.Get("Validation-Error",
                       Context.LocalizationLanguage).Text);
            }

            Context.Status = ret;

        }

        public async Task InsertValidation(LocalizationTextEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                  await Context.CheckUniqueValueForInsert(_repositories.LocalizationText.TableName, "Code", obj.Code);

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "Code",
                    string.Format(LocalizationText.Get("Validation-Unique-Value",
                        Context.LocalizationLanguage).Text, "Code"));
            }
         

            LocalizationTextParam param = null;
            List<LocalizationTextList> list = null;
          
            // verificar por name na mesma linguagem

            param = new LocalizationTextParam() { pName = obj.Name };
            list = await _repositories.LocalizationText.List(param);

            if (list != null)
            {
                if (list.Count > 0 && list[0].Language == obj.Language)
                {
                    ret.Success = false;
                    string msg
                        = string.Format(LocalizationText.Get("Validation-Unique-Value",
                        Context.LocalizationLanguage).Text, "Name");
                        ret.Exceptions.AddException(0, "Name", msg); 
                    
                }
            }

            Context.Status = ret;

        }

        public async Task UpdateValidation(LocalizationTextEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);
            LocalizationTextParam param = null;
            List<LocalizationTextList> list = null;

            // verificar por code

            bool check =
            await Context.CheckUniqueValueForUpdate(_repositories.LocalizationText.TableName, "Code",
                  obj.Code, _repositories.User.PKFieldName, obj.LocalizationTextID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "Code",
                    string.Format(LocalizationText.Get("Validation-Unique-Value",
                            Context.LocalizationLanguage).Text, "Code"));
            }      

            // verificar por name na mesma linguagem

            param = new LocalizationTextParam() { pName = obj.Name };
            list = await _repositories.LocalizationText.List(param);

            if (list != null)
            {
                if (list.Count > 0 && list[0].LocalizationTextID != obj.LocalizationTextID 
                    && list[0].Language == obj.Language)
                {
                    
                    ret.Success = false;
                    string msg
                        = string.Format(LocalizationText.Get("Validation-Unique-Value",
                                 Context.LocalizationLanguage).Text, "Name");
                                     ret.Exceptions.AddException(0, "Name", msg);
                }
            }

            Context.Status = ret;

        }

        public async Task DeleteValidation(LocalizationTextEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }

        public async Task<LocalizationTextEntry> Set(LocalizationTextEntry model, object userid)
        {
            LocalizationTextEntry ret = null;
            OPERATIONLOGENUM operation = OPERATIONLOGENUM.INSERT;

            await EntryValidation(model);

            if (Context.Status.Success)
            {

                LocalizationTextResult old
                    = await _repositories.LocalizationText.Read(new LocalizationTextParam() { pLocalizationTextID = model.LocalizationTextID });

                if (old == null)
                {
                    await InsertValidation(model);

                    if (Context.Status.Success)
                    {                       
                        if (model.LocalizationTextID == 0) { model.LocalizationTextID = FullDevToolKit.Helpers.Utilities.GenerateId(); }
                        await _repositories.LocalizationText.Create(model);
                    }
                }
                else
                {                   
                    operation = OPERATIONLOGENUM.UPDATE;

                    await UpdateValidation(model);

                    if (Context.Status.Success)
                    {
                        await _repositories.LocalizationText.Update(model);
                    }

                }

                if (Context.Status.Success && userid != null)
                {
                    await Context
                         .RegisterDataLogAsync(userid.ToString(), operation, "SYSLOCALIZATIONTEXT",
                            model.LocalizationTextID.ToString(), old, model);

                    ret = model;
                }

            }

            return ret;
        }

        public async Task<LocalizationTextEntry> Delete(LocalizationTextEntry model, object userid)
        {
            LocalizationTextEntry ret = null;

            LocalizationTextResult old
                = await _repositories.LocalizationText.Read(new LocalizationTextParam() { pLocalizationTextID = model.LocalizationTextID });

            if (old != null)
            {
                await DeleteValidation(model);

                if (Context.Status.Success)
                {
                    await _repositories.LocalizationText.Delete(model);

                    if (Context.Status.Success && userid != null)
                    {
                        await Context
                                .RegisterDataLogAsync(userid.ToString(), OPERATIONLOGENUM.DELETE, "SYSLOCALIZATIONTEXT",
                                model.LocalizationTextID.ToString(), old, model);

                        ret = model;
                    }

                }
            }
            else
            {
                 Context.Status
                    .SetFailStatus("Error", LocalizationText.Get("Record-NotFound",
                        Context.LocalizationLanguage).Text);

            }

            return ret;
        }

        public async Task<List<LocalizationTextList>> GetListOfLanguages()
        {
            List<LocalizationTextList> ret = null;

            ret = await _repositories.LocalizationText.GetListOfLanguages();

            return ret;
        }


    }


    public class LocalizationServiceBase
    {

        public void FillTexts(List<LocalizationTextResult> textLists,
                string language)
        {
            Type t = this.GetType();
            PropertyInfo[] prop = t.GetProperties();

            string text = "";
            string auxname = "";

            foreach (PropertyInfo p in prop)
            {

                auxname = p.Name;
                auxname = auxname.Replace("_", "-");

                text = GetText(textLists, auxname, language);

                p.SetValue(this, text, null);

            }

        }

        private string GetText(List<LocalizationTextResult> textLists,
                string name, string lang)
        {
            string ret = "";

            var aux = textLists
                .Where(t => t.Name == name && t.Language == lang)
                .FirstOrDefault();

            if (aux != null)
            {
                ret = aux.Text;
            }

            return ret;

        }        

    }

}
