using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;

namespace FullDevToolKit.Sys.Domains
{
    public class ParameterDomain : IParameterDomain
    {

        public ParameterDomain(ISystemRepositorySet repositorySet)
        {
            RepositorySet = repositorySet;
            Context = RepositorySet.Parameter.Context;
        }

        public IContext Context { get; set; }

        public ISystemRepositorySet RepositorySet { get; set; }

        public async Task<ParameterResult> FillChields(ParameterResult obj)
        {
            return obj;
        }

        public async Task<ParameterResult> Get(ParameterParam param)
        {
            ParameterResult ret = null;

            ret = await RepositorySet.Parameter.Read(param);

            return ret;
        }

        public async Task<List<ParameterList>> List(ParameterParam param)
        {
            List<ParameterList> ret = null;

            ret = await RepositorySet.Parameter.List(param);

            return ret;
        }

        public async Task<List<ParameterResult>> Search(ParameterParam param)
        {
            List<ParameterResult> ret = null;

            ret = await RepositorySet.Parameter.Search(param);

            return ret;
        }
        public async Task EntryValidation(ParameterEntry obj)
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

        public async Task InsertValidation(ParameterEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForInsert(RepositorySet.Parameter.TableName,
                        "ParameterName", obj.ParameterName);

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "ParameterName",
                   string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Parameter Name"));
            }

            Context.Status = ret;

        }

        public async Task UpdateValidation(ParameterEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForUpdate(RepositorySet.Parameter.TableName, "ParameterName",
                 obj.ParameterName, RepositorySet.User.PKFieldName, obj.ParameterID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "ParameterName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Parameter Name"));
            }

            Context.Status = ret;

        }

        public async Task DeleteValidation(ParameterEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task<ParameterEntry> Set(ParameterEntry model, object userid)
        {
            ParameterEntry ret = null;
            OPERATIONLOGENUM operation = OPERATIONLOGENUM.INSERT;

            await EntryValidation(model);

            if (Context.Status.Success)
            {

                ParameterResult old
                    = await RepositorySet.Parameter.Read(new ParameterParam() { pParameterID = model.ParameterID });

                if (old == null)
                {
                    await InsertValidation(model);

                    if (Context.Status.Success)
                    {
                        if (model.ParameterID == 0) { model.ParameterID = Utilities.GenerateId(); }
                        await RepositorySet.Parameter.Create(model);
                    }
                }
                else
                {
                    operation = OPERATIONLOGENUM.UPDATE;

                    await UpdateValidation(model);

                    if (Context.Status.Success)
                    {
                        await RepositorySet.Parameter.Update(model);
                    }

                }

                if (Context.Status.Success && userid != null)
                {
                    await Context
                         .RegisterDataLogAsync(userid.ToString(), operation, "SYSPARAMETER",
                             model.ParameterID.ToString(), old, model);

                    ret = model;
                }

            }

            return ret;
        }

        public async Task<ParameterEntry> Delete(ParameterEntry model, object userid)
        {
            ParameterEntry ret = null;

            ParameterResult old
                = await RepositorySet.Parameter.Read(new ParameterParam() { pParameterID = model.ParameterID });

            if (old != null)
            {
                await DeleteValidation(model);

                if (Context.Status.Success)
                {
                    await RepositorySet.Parameter.Delete(model);
                    if (Context.Status.Success && userid != null)
                    {
                        await Context
                                .RegisterDataLogAsync(userid.ToString(), OPERATIONLOGENUM.DELETE, "SYSPARAMETER",
                                model.ParameterID.ToString(), old, model);

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
