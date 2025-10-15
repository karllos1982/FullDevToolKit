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
    public class ConfigsDomain
              : BaseDomain<ConfigsParam, ConfigsEntry, ConfigsList, ConfigsResult>, IConfigsDomain
    {

        public ConfigsDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);
            this.TableName = _repositories.Configs.TableName;
        }
        

        private ISystemRepositorySet _repositories { get; set; }

        public override async Task<ConfigsResult> FillChields(ConfigsResult obj)
        {
            return obj;
        }

        public async Task<ConfigsResult> Get(ConfigsParam param)
        {
            ConfigsResult ret = null;

            ret = await _repositories.Configs.ReadObject(param);

            return ret;
        }

        public async Task<ConfigsResult> GetConfigByName(string name)
        {
            ConfigsResult ret = null; 

            List<ConfigsResult> list 
                = await _repositories.Configs.ReadSearch(new ConfigsParam() { pConfigName=name});

            if (list != null)
            {
                if (list.Count > 0)
                {
                    ret = list[0];
                }
            }
                        
            return ret;
        }
        

        public async Task<List<ConfigsList>> List(ConfigsParam param)
        {
            List<ConfigsList> ret = null;

            ret = await _repositories.Configs.ReadList(param);

            return ret;
        }

        public async Task<List<ConfigsResult>> Search(ConfigsParam param)
        {
            List<ConfigsResult> ret = null;

            ret = await _repositories.Configs.ReadSearch(param);

            return ret;
        }
     

        public override async Task InsertValidation(ConfigsEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForInsert(_repositories.Configs.TableName,
                        "ConfigName", obj.ConfigName);

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "ConfigName",
                   string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Config Name"));
            }

            Context.Status = ret;

        }

        public override async Task UpdateValidation(ConfigsEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForUpdate(_repositories.Configs.TableName, "ConfigName",
                 obj.ConfigName, _repositories.User.PKFieldName, obj.ConfigID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "ConfigName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Config Name"));
            }

            Context.Status = ret;

        }

        public override async Task DeleteValidation(ConfigsEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task<ConfigsEntry> Set(ConfigsEntry model, object userid)
        {
            ConfigsEntry ret = null;
            
            if (model.ConfigID == 0)
            {
                model.ConfigID = Helpers.Utilities.GenerateId();
            }
            this.PKValue = model.ConfigID.ToString();

            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.Configs.ReadObject(new ConfigsParam()
                             { pConfigID = model.ConfigID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Configs.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Configs.Update(model);
                      }                      
                  );

            return ret;
        }


        public async Task<ConfigsEntry> Remove(ConfigsEntry model, object userid)
        {
            ConfigsEntry ret = null;
            this.PKValue = model.ConfigID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.Configs.ReadObject(new ConfigsParam()
                             { pConfigID = model.ConfigID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Configs.Delete(model);
                      }

                  );

            return ret;
        }



    }
}
