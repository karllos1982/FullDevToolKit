using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;

namespace FullDevToolKit.Sys.Domains
{
    public class DataLogDomain : IDataLogDomain
    {
        public DataLogDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);          
            
        }
        public IContext Context { get; set; }

        private ISystemRepositorySet _repositories { get; set; }

        public async Task<DataLogResult> FillChields(DataLogResult obj)
        {
            return obj;
        }

        public async Task<DataLogResult> Get(DataLogParam param)
        {
            DataLogResult ret = null;

            ret = await _repositories.DataLog.Read(param); 
            
            return ret;
        }

        public async Task<List<DataLogList>> List(DataLogParam param)
        {
            List<DataLogList> ret = null;

            ret = await _repositories.DataLog.List(param);           

            return ret;
        }

        public async Task<List<DataLogResult>> Search(DataLogParam param)
        {
            List<DataLogResult> ret = null;

            ret = await _repositories.DataLog.Search(param);

            return ret;
        }

        public async Task EntryValidation(DataLogEntry obj)
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

        public async Task InsertValidation(DataLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task UpdateValidation(DataLogEntry obj)
        {
             Context.Status = new ExecutionStatus(true);

        }

        public async Task DeleteValidation(DataLogEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }

        public async Task<DataLogEntry> Set(DataLogEntry model, object userid)
        {
            DataLogEntry ret = null;

            OPERATIONLOGENUM operation = OPERATIONLOGENUM.INSERT;

            await EntryValidation(model);

            if (Context.Status.Success)
            {

                DataLogResult old 
                    = await _repositories.DataLog.Read(new DataLogParam() { pDataLogID = model.DataLogID });

                if (old == null)
                {
                    await InsertValidation(model); 
                    if (Context.Status.Success)
                    {           
                        if (model.DataLogID ==0 ) { model.DataLogID = FullDevToolKit.Helpers.Utilities.GenerateId(); }
                        await _repositories.DataLog.Create(model);
                    }
                }
                else
                {                    
                    operation = OPERATIONLOGENUM.UPDATE;

                     await UpdateValidation(model);

                    if (Context.Status.Success)
                    {
                         await _repositories.DataLog.Update(model);
                    }

                }

                if (Context.Status.Success && userid != null)
                {
                   await Context
                        .RegisterDataLogAsync(userid.ToString(), operation, "SYSDATALOG",
                        model.DataLogID.ToString(), old, model);

                    ret = model;
                }

            }
            
            return ret;
        }

      

        public async Task<DataLogEntry> Delete(DataLogEntry model, object userid)
        {
            DataLogEntry ret = null;

            DataLogResult old 
                = await _repositories.DataLog.Read(new DataLogParam() { pDataLogID = model.DataLogID });

            if (old != null)
            {
               await DeleteValidation(model);

                if (Context.Status.Success)
                {
                   await _repositories.DataLog.Delete(model);

                    if (Context.Status.Success && userid != null)
                    {
                        await Context
                                .RegisterDataLogAsync(userid.ToString(), OPERATIONLOGENUM.DELETE, "SYSDATALOG",
                                    model.DataLogID.ToString(), old, model);

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


      

        public async Task<List<DataLogTimelineModel>> GetTimeLine(long recordID)
        {
            List<DataLogTimelineModel> ret = null;

            ret = await _repositories.DataLog.GetDataLogTimeline(recordID);    

            return ret;
        }

        public async Task<List<TabelasValueModel>> GetTableList()
        {
            List<TabelasValueModel> ret = null;

            ret =await  _repositories.DataLog.GetTableList();


            return ret;
        }

    }
}
