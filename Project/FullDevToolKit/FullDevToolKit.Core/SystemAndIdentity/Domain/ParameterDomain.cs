using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;
using FullDevToolKit.Sys.Models.Identity;

namespace FullDevToolKit.Sys.Domains
{
    public class ParameterDomain
              : BaseDomain<ParameterParam, ParameterEntry, ParameterList, ParameterResult>, IParameterDomain
    {

        public ParameterDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);
            this.TableName = _repositories.Parameter.TableName;
        }
        

        private ISystemRepositorySet _repositories { get; set; }

        public override async Task<ParameterResult> FillChields(ParameterResult obj)
        {
            return obj;
        }

        public async Task<ParameterResult> Get(ParameterParam param)
        {
            ParameterResult ret = null;

            ret = await _repositories.Parameter.ReadObject(param);

            return ret;
        }

        public async Task<List<ParameterList>> List(ParameterParam param)
        {
            List<ParameterList> ret = null;

            ret = await _repositories.Parameter.ReadList(param);

            return ret;
        }

        public async Task<List<ParameterResult>> Search(ParameterParam param)
        {
            List<ParameterResult> ret = null;

            ret = await _repositories.Parameter.ReadSearch(param);

            return ret;
        }
     

        public override async Task InsertValidation(ParameterEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForInsert(_repositories.Parameter.TableName,
                        "ParameterName", obj.ParameterName);

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "ParameterName",
                   string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Parameter Name"));
            }

            Context.Status = ret;

        }

        public override async Task UpdateValidation(ParameterEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForUpdate(_repositories.Parameter.TableName, "ParameterName",
                 obj.ParameterName, _repositories.User.PKFieldName, obj.ParameterID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "ParameterName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Parameter Name"));
            }

            Context.Status = ret;

        }

        public override async Task DeleteValidation(ParameterEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task<ParameterEntry> Set(ParameterEntry model, object userid)
        {
            ParameterEntry ret = null;
            
            if (model.ParameterID == 0)
            {
                model.ParameterID = Helpers.Utilities.GenerateId();
            }
            this.PKValue = model.ParameterID.ToString();

            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.Parameter.ReadObject(new ParameterParam()
                             { pParameterID = model.ParameterID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Parameter.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Parameter.Update(model);
                      }                      
                  );

            return ret;
        }


        public async Task<ParameterEntry> Remove(ParameterEntry model, object userid)
        {
            ParameterEntry ret = null;
            this.PKValue = model.ParameterID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.Parameter.ReadObject(new ParameterParam()
                             { pParameterID = model.ParameterID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Parameter.Delete(model);
                      }

                  );

            return ret;
        }



    }
}
