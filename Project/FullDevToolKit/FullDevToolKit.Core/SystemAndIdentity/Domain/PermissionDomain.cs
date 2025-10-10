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
    public class PermissionDomain
         : BaseDomain<PermissionParam, PermissionEntry, PermissionList, PermissionResult>, IPermissionDomain

    {

        public PermissionDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);
            this.TableName = _repositories.Permission.TableName;

        }
        
        private ISystemRepositorySet _repositories { get; set; }


        public override async Task<PermissionResult> FillChields(PermissionResult obj)
        {
            return obj;
        }

        public async Task<PermissionResult> Get(PermissionParam param)
        {
            PermissionResult ret = null;

            ret = await _repositories.Permission.Read(param); 
            
            return ret;
        }

        public async Task<List<PermissionList>> List(PermissionParam param)
        {
            List<PermissionList> ret = null;

            ret = await _repositories.Permission.List(param);           

            return ret;
        }

        public async Task<List<PermissionResult>> Search(PermissionParam param)
        {
            List<PermissionResult> ret = null;

            ret = await _repositories.Permission.Search(param);

            return ret;
        }
    
        public override async Task InsertValidation(PermissionEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }

        public override async Task UpdateValidation(PermissionEntry obj)
        {
             Context.Status = new ExecutionStatus(true);

        }

        public override async Task DeleteValidation(PermissionEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }


        public async Task<PermissionEntry> Set(PermissionEntry model, object userid)
        {
            PermissionEntry ret = null;
            
            if (model.PermissionID == 0)
            {
                model.PermissionID = Helpers.Utilities.GenerateId();
            }
            this.PKValue = model.PermissionID.ToString();

            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.Permission.Read(new PermissionParam()
                             { pPermissionID = model.PermissionID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Permission.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Permission.Update(model);
                      }                      
                  );

            return ret;
        }


        public async Task<PermissionEntry> Delete(PermissionEntry model, object userid)
        {
            PermissionEntry ret = null;
            this.PKValue = model.PermissionID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.Permission.Read(new PermissionParam()
                             { pPermissionID = model.PermissionID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Permission.Delete(model);
                      }

                  );

            return ret;
        }



        public async Task<List<PermissionResult>> GetPermissionsByRoleUser(Int64 roleid, Int64 userid)
        {
            List<PermissionResult> ret = null;

            PermissionParam param = new PermissionParam()
            {
                pRoleID = roleid,
                pUserID = userid
            };

            ret = await _repositories.Permission.GetPermissionsByRoleUser(param);
            

            return ret;
        }

    }
}
