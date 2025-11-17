using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;
using FullDevToolKit.Sys.Models.Identity;
using System.Net.Http.Headers;

namespace FullDevToolKit.Sys.Domains
{
    public class LanguageDomain
              : BaseDomain<LanguageParam, LanguageEntry, LanguageList, LanguageResult>, ILanguageDomain
    {

        public LanguageDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);
            this.TableName = _repositories.Language.TableName;
        }
        

        private ISystemRepositorySet _repositories { get; set; }

        public override async Task<LanguageResult> FillChields(LanguageResult obj)
        {
            return obj;
        }

        public async Task<LanguageResult> Get(LanguageParam param)
        {
            LanguageResult ret = null;

            ret = await _repositories.Language.ReadObject(param);

            return ret;
        }

        public async Task<LanguageResult> GetLanguageByName(string name)
        {
            LanguageResult ret = null; 

            List<LanguageResult> list 
                = await _repositories.Language.ReadSearch(new LanguageParam() { pLanguageName=name});

            if (list != null)
            {
                if (list.Count > 0)
                {
                    ret = list[0];
                }
            }
                        
            return ret;
        }
        

        public async Task<List<LanguageList>> List(LanguageParam param)
        {
            List<LanguageList> ret = null;

            ret = await _repositories.Language.ReadList(param);

            return ret;
        }

        public async Task<List<LanguageResult>> Search(LanguageParam param)
        {
            List<LanguageResult> ret = null;

            ret = await _repositories.Language.ReadSearch(param);

            return ret;
        }
     

        public override async Task InsertValidation(LanguageEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForInsert(_repositories.Language.TableName,
                        "LanguageName", obj.LanguageName);

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "LanguageName",
                   string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Language Name"));
            }

            Context.Status = ret;

        }

        public override async Task UpdateValidation(LanguageEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForUpdate(_repositories.Language.TableName, "LanguageName",
                 obj.LanguageName, _repositories.User.PKFieldName, obj.LanguageID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "LanguageName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Language Name"));
            }

            Context.Status = ret;

        }

        public override async Task DeleteValidation(LanguageEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task<LanguageEntry> Set(LanguageEntry model, object userid)
        {
            LanguageEntry ret = null;
            
            if (model.LanguageID == 0)
            {
                model.LanguageID = Helpers.Utilities.GenerateId();
            }
            this.PKValue = model.LanguageID.ToString();

            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.Language.ReadObject(new LanguageParam()
                             { pLanguageID = model.LanguageID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Language.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Language.Update(model);
                      }                      
                  );

            return ret;
        }


        public async Task<LanguageEntry> Remove(LanguageEntry model, object userid)
        {
            LanguageEntry ret = null;
            this.PKValue = model.LanguageID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.Language.ReadObject(new LanguageParam()
                             { pLanguageID = model.LanguageID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Language.Delete(model);
                      }

                  );

            return ret;
        }



    }
}
