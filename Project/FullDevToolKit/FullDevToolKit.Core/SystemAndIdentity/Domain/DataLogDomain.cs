using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;

namespace FullDevToolKit.Sys.Domains
{
    public class DataLogDomain 
        : BaseDomain<DataLogParam,DataLogEntry,DataLogList,DataLogResult>, IDataLogDomain
    {
        public DataLogDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);
            this.TableName = _repositories.DataLog.TableName; 
            
        }
        
        private ISystemRepositorySet _repositories { get; set; }

        public override async Task<DataLogResult> FillChields(DataLogResult obj)
        {
            return obj;
        }

        public async Task<DataLogResult> Get(DataLogParam param)
        {
            DataLogResult ret = null;

            ret = await _repositories.DataLog.ReadObject(param); 
            
            return ret;
        }

        public async Task<List<DataLogList>> List(DataLogParam param)
        {
            List<DataLogList> ret = null;

            ret = await _repositories.DataLog.ReadList(param);           

            return ret;
        }

        public async Task<List<DataLogResult>> Search(DataLogParam param)
        {
            List<DataLogResult> ret = null;

            ret = await _repositories.DataLog.ReadSearch(param);

            return ret;
        }
  

        public override async Task InsertValidation(DataLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public override async Task UpdateValidation(DataLogEntry obj)
        {
             Context.Status = new ExecutionStatus(true);

        }

        public override async Task DeleteValidation(DataLogEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }

        public async Task<DataLogEntry> Set(DataLogEntry model, object userid)
        {
            DataLogEntry ret = null;

            if (model.DataLogID == 0)
            {
                model.DataLogID = Helpers.Utilities.GenerateId();
            }
            this.PKValue = model.DataLogID.ToString();

            
            ret = await ExecutionForSet( model, userid,
                      async (model) =>
                      {
                         return 
                            await _repositories.DataLog.ReadObject(new DataLogParam() { pDataLogID = model.DataLogID });
                      }
                      ,
                      async (model) =>
                      {                          
                          await _repositories.DataLog.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.DataLog.Update(model);
                      }                     
                  );

            return ret;
        }

      
        public async Task<DataLogEntry> Remove(DataLogEntry model, object userid)
        {
            DataLogEntry ret = null;
            this.PKValue = model.DataLogID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.DataLog.ReadObject(new DataLogParam() { pDataLogID = model.DataLogID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.DataLog.Delete(model);
                      }
                    
                  );

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
