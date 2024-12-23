using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;

namespace FullDevToolKit.Sys.Domains
{
    public class GroupParameterDomain : IGroupParameterDomain
    {

        public GroupParameterDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);
        }

        public IContext Context { get; set; }

        private ISystemRepositorySet _repositories { get; set; }

        public async Task<GroupParameterResult> FillChields(GroupParameterResult obj)
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
        public async Task EntryValidation(GroupParameterEntry obj)
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

        public async Task InsertValidation(GroupParameterEntry obj)
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

        public async Task UpdateValidation(GroupParameterEntry obj)
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

        public async Task DeleteValidation(GroupParameterEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task<GroupParameterEntry> Set(GroupParameterEntry model, object userid)
        {
            GroupParameterEntry ret = null;
            OPERATIONLOGENUM operation = OPERATIONLOGENUM.INSERT;

            await EntryValidation(model);

            if (Context.Status.Success)
            {

                GroupParameterResult old
                    = await _repositories.GroupParameter.Read(new GroupParameterParam() { pGroupParameterID = model.GroupParameterID });

                if (old == null)
                {
                    await InsertValidation(model);

                    if (Context.Status.Success)
                    {                        
                        if (model.GroupParameterID == 0) { model.GroupParameterID = Utilities.GenerateId(); }
                        await _repositories.GroupParameter.Create(model);
                    }
                }
                else
                {                    
                    operation = OPERATIONLOGENUM.UPDATE;

                    await UpdateValidation(model);

                    if (Context.Status.Success)
                    {
                        await _repositories.GroupParameter.Update(model);
                    }

                }

                if (Context.Status.Success && userid != null)
                {
                    await Context
                         .RegisterDataLogAsync(userid.ToString(), operation, "SYSGROUPPARAMETER",
                         model.GroupParameterID.ToString(), old, model);

                    ret = model;
                }

            }

            return ret;
        }

        public async Task<GroupParameterEntry> Delete(GroupParameterEntry model, object userid)
        {
            GroupParameterEntry ret = null;

            GroupParameterResult old
                = await _repositories.GroupParameter.Read(new GroupParameterParam() { pGroupParameterID = model.GroupParameterID });

            if (old != null)
            {
                await DeleteValidation(model);

                if (Context.Status.Success)
                {
                    await _repositories.GroupParameter.Delete(model);
                    if (Context.Status.Success && userid != null)
                    {
                        await Context
                            .RegisterDataLogAsync(userid.ToString(), OPERATIONLOGENUM.DELETE, "SYSGROUPPARAMETER",
                                 model.GroupParameterID.ToString(), old, model);

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



    }
}
