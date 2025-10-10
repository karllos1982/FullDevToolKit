using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;

namespace FullDevToolKit.Sys.Domains
{
    public class RoleDomain
            : BaseDomain<RoleParam, RoleEntry, RoleList, RoleResult>, IRoleDomain

    {

        public RoleDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);
            this.TableName = _repositories.Role.TableName;
        }

        
        private ISystemRepositorySet _repositories { get; set; }


        public override async Task<RoleResult> FillChields(RoleResult obj)
        {
            return obj;
        }

        public async Task<RoleResult> Get(RoleParam param)
        {
            RoleResult ret = null;

            ret = await _repositories.Role.Read(param); 
            
            return ret;
        }

        public async Task<List<RoleList>> List(RoleParam param)
        {
            List<RoleList> ret = null;

            ret = await _repositories.Role.List(param);           

            return ret;
        }

        public async Task<List<RoleResult>> Search(RoleParam param)
        {
            List<RoleResult> ret = null;

            ret = await _repositories.Role.Search(param);

            return ret;
        }
   
        public override async Task InsertValidation(RoleEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForInsert(_repositories.Role.TableName, "RoleName", obj.RoleName);

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "RoleName",
                   string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Role Name"));
            }  

            Context.Status = ret;
          
        }

        public override async Task UpdateValidation(RoleEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await _repositories.Role.Context.CheckUniqueValueForUpdate(_repositories.Role.TableName, "RoleName",
                obj.RoleName, _repositories.User.PKFieldName, obj.RoleID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "RoleName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Role Name"));
            }

            Context.Status = ret;

        }

        public override async Task DeleteValidation(RoleEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }

        public async Task<RoleEntry> Set(RoleEntry model, object userid)
        {
            RoleEntry ret = null;
            
            if (model.RoleID == 0)
            {
                model.RoleID = Helpers.Utilities.GenerateId();
            }
            this.PKValue = model.RoleID.ToString();

            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.Role.Read(new RoleParam()
                             { pRoleID = model.RoleID });
                      }
                      ,
                      async (model) =>
                      {
                          model.CreateDate = DateTime.Now;  
                          await _repositories.Role.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Role.Update(model);
                      }                     
                  );

            return ret;
        }


        public async Task<RoleEntry> Delete(RoleEntry model, object userid)
        {
            RoleEntry ret = null;
            this.PKValue = model.RoleID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.Role.Read(new RoleParam()
                             { pRoleID = model.RoleID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.Role.Delete(model);
                      }

                  );

            return ret;
        }


    }
}
