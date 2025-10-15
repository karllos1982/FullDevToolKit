using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;

namespace FullDevToolKit.Sys.Domains
{
    public class ExceptionLogDomain
        : BaseDomain<ExceptionLogParam, ExceptionLogEntry, ExceptionLogList, ExceptionLogResult>, IExceptionLogDomain
    {
        public ExceptionLogDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);
            this.TableName = _repositories.ExceptionLog.TableName;
        }
        
        private ISystemRepositorySet _repositories { get; set; }

        public override async Task<ExceptionLogResult> FillChields(ExceptionLogResult obj)
        {
            return obj;
        }

        public async Task<ExceptionLogResult> Get(ExceptionLogParam param)
        {
            ExceptionLogResult ret = null;

            ret = await _repositories.ExceptionLog.ReadObject(param);

            return ret;
        }

        public async Task<List<ExceptionLogList>> List(ExceptionLogParam param)
        {
            List<ExceptionLogList> ret = null;

            ret = await _repositories.ExceptionLog.ReadList(param);

            return ret;
        }

        public async Task<List<ExceptionLogResult>> Search(ExceptionLogParam param)
        {
            List<ExceptionLogResult> ret = null;

            ret = await _repositories.ExceptionLog.ReadSearch(param);

            return ret;
        }

       
        public override async Task InsertValidation(ExceptionLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public override async Task UpdateValidation(ExceptionLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);

        }

        public override async Task DeleteValidation(ExceptionLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task<ExceptionLogEntry> Set(ExceptionLogEntry model, object userid)
        {
            ExceptionLogEntry ret = null;
            
            if (model.ExceptionLogID == 0)
            {
                model.ExceptionLogID = Helpers.Utilities.GenerateId();
            }
            this.PKValue = model.ExceptionLogID.ToString();

            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.ExceptionLog.ReadObject(new ExceptionLogParam() 
                             { pExceptionLogID = model.ExceptionLogID });
                      }
                      ,
                      async (model) =>
                      {                          
                          await _repositories.ExceptionLog.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.ExceptionLog.Update(model);
                      }                      
                  );

            return ret;
        }

        public async Task<ExceptionLogEntry> Remove(ExceptionLogEntry model, object userid)
        {
            ExceptionLogEntry ret = null;
            this.PKValue = model.ExceptionLogID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.ExceptionLog.ReadObject(new ExceptionLogParam() 
                                { pExceptionLogID = model.ExceptionLogID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.ExceptionLog.Delete(model);
                      }

                  );

            return ret;
        }

      
    }
}
