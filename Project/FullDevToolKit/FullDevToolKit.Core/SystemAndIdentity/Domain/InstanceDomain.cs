using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;
using FullDevToolKit.Sys.Models.Common;


namespace FullDevToolKit.Sys.Domains
{
    public class InstanceDomain
                : BaseDomain<InstanceParam, InstanceEntry, InstanceList, InstanceResult>, IInstanceDomain
    {
       
        public InstanceDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);
            this.TableName = _repositories.Instance.TableName;
        }
        

        private ISystemRepositorySet _repositories { get; set; }

        public override async Task<InstanceResult> FillChields(InstanceResult obj)
        {
            return obj;
        }

        public async Task<InstanceResult> Get(InstanceParam param)
        {
            InstanceResult ret = null;

            ret = await _repositories.Instance.Read(param);

            return ret;
        }

        public async Task<List<InstanceList>> List(InstanceParam param)
        {
            List<InstanceList> ret = null;

            ret = await _repositories.Instance.List(param);

            return ret;
        }

        public async Task<List<InstanceResult>> Search(InstanceParam param)
        {
            List<InstanceResult> ret = null;

            ret = await _repositories.Instance.Search(param);

            return ret;
        }

      
        public override async Task InsertValidation(InstanceEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);


            bool check =
              await Context.CheckUniqueValueForInsert(_repositories.Instance.TableName, "InstanceName", obj.InstanceName);

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "InstanceName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Instance Name"));
              
            }

            Context.Status = ret;

        }

        public override async Task UpdateValidation(InstanceEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
              await Context.CheckUniqueValueForUpdate(_repositories.Instance.TableName, "InstanceName",
                    obj.InstanceName, _repositories.User.PKFieldName,obj.InstanceID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "InstanceName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Instance Name"));

            }
       
            Context.Status = ret;

        }

        public override async Task DeleteValidation(InstanceEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }

        public async Task<InstanceEntry> Set(InstanceEntry model, object userid)
        {
            InstanceEntry ret = null;
            this.PKValue = model.InstanceID.ToString();

            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.Instance.Read(new InstanceParam()
                             { pInstanceID = model.InstanceID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Instance.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Instance.Update(model);
                      }
                      
                  );

            return ret;
        }


        public async Task<InstanceEntry> Delete(InstanceEntry model, object userid)
        {
            InstanceEntry ret = null;
            this.PKValue = model.InstanceID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.Instance.Read(new InstanceParam()
                             { pInstanceID = model.InstanceID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Instance.Delete(model);
                      }

                  );

            return ret;
        }



    }
}
