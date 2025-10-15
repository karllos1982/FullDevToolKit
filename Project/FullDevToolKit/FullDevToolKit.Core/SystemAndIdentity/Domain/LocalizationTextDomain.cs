using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using System.Reflection;
using FullDevToolKit.Sys.Data.Repositories;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Domains
{
    public class LocalizationTextDomain
             : BaseDomain<LocalizationTextParam, LocalizationTextEntry, LocalizationTextList, LocalizationTextResult>, ILocalizationTextDomain
    {

        public LocalizationTextDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);
            this.TableName = _repositories.LocalizationText.TableName;
        }        

        private ISystemRepositorySet _repositories { get; set; }

        public override async Task<LocalizationTextResult> FillChields(LocalizationTextResult obj)
        {
            return obj;
        }

        public async Task<LocalizationTextResult> Get(LocalizationTextParam param)
        {
            LocalizationTextResult ret = null;

            ret = await _repositories.LocalizationText.ReadObject(param);

            return ret;
        }

        public async Task<List<LocalizationTextList>> List(LocalizationTextParam param)
        {
            List<LocalizationTextList> ret = null;

            ret = await _repositories.LocalizationText.ReadList(param);

            return ret;
        }

        public async Task<List<LocalizationTextResult>> Search(LocalizationTextParam param)
        {
            List<LocalizationTextResult> ret = null;

            ret = await _repositories.LocalizationText.ReadSearch(param);

            return ret;
        }

   
        public override async Task InsertValidation(LocalizationTextEntry obj)
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
            list = await _repositories.LocalizationText.ReadList(param);

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

        public override async Task UpdateValidation(LocalizationTextEntry obj)
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
            list = await _repositories.LocalizationText.ReadList(param);

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

        public override async Task DeleteValidation(LocalizationTextEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }


        public async Task<LocalizationTextEntry> Set(LocalizationTextEntry model, object userid)
        {
            LocalizationTextEntry ret = null;
            
            if (model.LocalizationTextID == 0)
            {
                model.LocalizationTextID = Helpers.Utilities.GenerateId();
            }
            this.PKValue = model.LocalizationTextID.ToString();


            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.LocalizationText.ReadObject(new LocalizationTextParam()
                             { pLocalizationTextID = model.LocalizationTextID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.LocalizationText.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.LocalizationText.Update(model);
                      }                      
                  );

            return ret;
        }


        public async Task<LocalizationTextEntry> Remove(LocalizationTextEntry model, object userid)
        {
            LocalizationTextEntry ret = null;
            this.PKValue = model.LocalizationTextID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.LocalizationText.ReadObject(new LocalizationTextParam()
                             { pLocalizationTextID = model.LocalizationTextID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.LocalizationText.Delete(model);
                      }

                  );

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
