using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;
using FullDevToolKit.Core.Common;

namespace FullDevToolKit.Sys.Domains
{
    public class SessionLogDomain
        : BaseDomain<SessionLogParam, SessionLogEntry, SessionLogList, SessionLogResult>, ISessionLogDomain
    {
        
        public SessionLogDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);
            this.TableName = _repositories.SessionLog.TableName;
        }        

        private ISystemRepositorySet _repositories { get; set; }


        public override async Task<SessionLogResult> FillChields(SessionLogResult obj)
        {
            return obj;
        }

        public async Task<SessionLogResult> Get(SessionLogParam param)
        {
            SessionLogResult ret = null;

            ret = await _repositories.SessionLog.ReadObject(param); 
            
            return ret;
        }

        public async Task<List<SessionLogList>> List(SessionLogParam param)
        {
            List<SessionLogList> ret = null;

            ret = await _repositories.SessionLog.ReadList(param);           

            return ret;
        }

        public async Task<PagedList<SessionLogResult>> Search(SessionLogParam param)
        {
            PagedList<SessionLogResult> ret = null;

            ret = await  _repositories.SessionLog.ReadSearch(param);

            return ret;
        }

      
        public override async Task InsertValidation(SessionLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public override async Task UpdateValidation(SessionLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);

        }

        public override async Task DeleteValidation(SessionLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }


        public async Task<SessionLogEntry> Set(SessionLogEntry model, object userid)
        {
            SessionLogEntry ret = null;
            
            if (model.SessionLogID == 0)
            {
                model.SessionLogID = Helpers.Utilities.GenerateId();
            }
            this.PKValue = model.SessionLogID.ToString();

            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.SessionLog.ReadObject(new SessionLogParam()
                             { pSessionLogID = model.SessionLogID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.SessionLog.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.SessionLog.Update(model);
                      }                    
                  );

            return ret;
        }


        public async Task<SessionLogEntry> Remove(SessionLogEntry model, object userid)
        {
            SessionLogEntry ret = null;
            this.PKValue = model.SessionLogID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.SessionLog.ReadObject(new SessionLogParam()
                             { pSessionLogID = model.SessionLogID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.SessionLog.Delete(model);
                      }

                  );

            return ret;
        }

    }
}
