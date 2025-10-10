using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;

namespace FullDevToolKit.Sys.Domains
{
    public class GroupParameterDomain  
        : BaseDomain<GroupParameterParam, GroupParameterEntry, GroupParameterList, GroupParameterResult>, IGroupParameterDomain
    {

        public GroupParameterDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);
            this.TableName = _repositories.GroupParameter.TableName;
        }        

        private ISystemRepositorySet _repositories { get; set; }

        public override async Task<GroupParameterResult> FillChields(GroupParameterResult obj)
        {
            return obj;
        }

        public async Task<GroupParameterResult> Get(GroupParameterParam param)
        {
            GroupParameterResult ret = null;

            ret = await _repositories.GroupParameter.Read(param);

            return ret;
        }

        public async Task<List<GroupParameterList>> List(GroupParameterParam param)
        {
            List<GroupParameterList> ret = null;

            ret = await _repositories.GroupParameter.List(param);

            return ret;
        }

        public async Task<List<GroupParameterResult>> Search(GroupParameterParam param)
        {
            List<GroupParameterResult> ret = null;

            ret = await _repositories.GroupParameter.Search(param);

            return ret;
        }
     
        public override async Task InsertValidation(GroupParameterEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForInsert(_repositories.GroupParameter.TableName, "GroupParameterName", obj.GroupParameterName);

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "GroupParameterName",
                   string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "GroupParameter Name"));
            }

            Context.Status = ret;

        }

        public override async Task UpdateValidation(GroupParameterEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForUpdate(_repositories.GroupParameter.TableName, "GroupParameterName",
                obj.GroupParameterName, _repositories.User.PKFieldName, obj.GroupParameterID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "GroupParameterName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "GroupParameter Name"));
            }

            Context.Status = ret;

        }

        public override async Task DeleteValidation(GroupParameterEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task<GroupParameterEntry> Set(GroupParameterEntry model, object userid)
        {
            GroupParameterEntry ret = null;
            
            if (model.GroupParameterID == 0)
            {
                model.GroupParameterID = Helpers.Utilities.GenerateId();
            }
            this.PKValue = model.GroupParameterID.ToString();

            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.GroupParameter.Read(new GroupParameterParam()
                             { pGroupParameterID = model.GroupParameterID });
                      }
                      ,
                      async (model) =>
                      {                          
                          await _repositories.GroupParameter.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.GroupParameter.Update(model);
                      }                      
                  );

            return ret;
        }


        public async Task<GroupParameterEntry> Delete(GroupParameterEntry model, object userid)
        {
            GroupParameterEntry ret = null;
            this.PKValue = model.GroupParameterID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.GroupParameter.Read(new GroupParameterParam()
                             { pGroupParameterID = model.GroupParameterID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.GroupParameter.Delete(model);
                      }

                  );

            return ret;
        }


    }
}
