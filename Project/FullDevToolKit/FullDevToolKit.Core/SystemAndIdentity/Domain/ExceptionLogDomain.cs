using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;

namespace FullDevToolKit.Sys.Domains
{
    public class ExceptionLogDomain : IExceptionLogDomain
    {
        public ExceptionLogDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);

        }
        public IContext Context { get; set; }

        private ISystemRepositorySet _repositories { get; set; }

        public async Task<ExceptionLogResult> FillChields(ExceptionLogResult obj)
        {
            return obj;
        }

        public async Task<ExceptionLogResult> Get(ExceptionLogParam param)
        {
            ExceptionLogResult ret = null;

            ret = await _repositories.ExceptionLog.Read(param);

            return ret;
        }

        public async Task<List<ExceptionLogList>> List(ExceptionLogParam param)
        {
            List<ExceptionLogList> ret = null;

            ret = await _repositories.ExceptionLog.List(param);

            return ret;
        }

        public async Task<List<ExceptionLogResult>> Search(ExceptionLogParam param)
        {
            List<ExceptionLogResult> ret = null;

            ret = await _repositories.ExceptionLog.Search(param);

            return ret;
        }

        public async Task EntryValidation(ExceptionLogEntry obj)
        {
            ExecutionStatus ret = null;

            ret = PrimaryValidation.Execute(obj, new List<string>(), Context.LocalizationLanguage);

            if (!ret.Success)
            {
                ret.SetFailStatus("Error",
                   LocalizationText.Get("Validation-Error", Context.LocalizationLanguage).Text);

            }

            Context.Status = ret;

        }

        public async Task InsertValidation(ExceptionLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task UpdateValidation(ExceptionLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);

        }

        public async Task DeleteValidation(ExceptionLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task<ExceptionLogEntry> Set(ExceptionLogEntry model, object userid)
        {
            ExceptionLogEntry ret = null;

            OPERATIONLOGENUM operation = OPERATIONLOGENUM.INSERT;

            await EntryValidation(model);

            if (Context.Status.Success)
            {

                ExceptionLogResult old
                    = await _repositories.ExceptionLog.Read(new ExceptionLogParam() { pExceptionLogID = model.ExceptionLogID });

                if (old == null)
                {
                    await InsertValidation(model);
                    if (Context.Status.Success)
                    {
                        if (model.ExceptionLogID == 0) { model.ExceptionLogID = FullDevToolKit.Helpers.Utilities.GenerateId(); }
                        await _repositories.ExceptionLog.Create(model);
                    }
                }
                else
                {
                    operation = OPERATIONLOGENUM.UPDATE;

                    await UpdateValidation(model);

                    if (Context.Status.Success)
                    {
                        await _repositories.ExceptionLog.Update(model);
                    }

                }

                if (Context.Status.Success && userid != null)
                {
                    await Context
                         .RegisterDataLogAsync(userid.ToString(), operation, "SYSEXCEPTIONLOG",
                         model.ExceptionLogID.ToString(), old, model);

                    ret = model;
                }

            }

            return ret;
        }



        public async Task<ExceptionLogEntry> Delete(ExceptionLogEntry model, object userid)
        {
            ExceptionLogEntry ret = null;

            ExceptionLogResult old
                = await _repositories.ExceptionLog.Read(new ExceptionLogParam() { pExceptionLogID = model.ExceptionLogID });

            if (old != null)
            {
                await DeleteValidation(model);

                if (Context.Status.Success)
                {
                    await _repositories.ExceptionLog.Delete(model);

                    if (Context.Status.Success && userid != null)
                    {
                        await Context
                                .RegisterDataLogAsync(userid.ToString(), OPERATIONLOGENUM.DELETE, "SYSEXCEPTIONLOG",
                                    model.ExceptionLogID.ToString(), old, model);

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
