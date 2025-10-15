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
    public class ObjectPermissionDomain
              : BaseDomain<ObjectPermissionParam, ObjectPermissionEntry, ObjectPermissionList, ObjectPermissionResult>, IObjectPermissionDomain

    {

        public ObjectPermissionDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);
            this.TableName = _repositories.ObjectPermission.TableName;
        }

        
        private ISystemRepositorySet _repositories { get; set; }

        public override async Task<ObjectPermissionResult> FillChields(ObjectPermissionResult obj)
        {
            return obj;
        }

        public async Task<ObjectPermissionResult> Get(ObjectPermissionParam param)
        {
            ObjectPermissionResult ret = null;

            ret = await _repositories.ObjectPermission.ReadObject(param); 
            
            return ret;
        }

        public async Task<List<ObjectPermissionList>> List(ObjectPermissionParam param)
        {
            List<ObjectPermissionList> ret = null;

            ret = await _repositories.ObjectPermission.ReadList(param);           

            return ret;
        }

        public async Task<List<ObjectPermissionResult>> Search(ObjectPermissionParam param)
        {
            List<ObjectPermissionResult> ret = null;

            ret = await _repositories.ObjectPermission.ReadSearch(param);

            return ret;
        }

 
        public override async Task InsertValidation(ObjectPermissionEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);


            bool check =
                    await Context.CheckUniqueValueForInsert(_repositories.ObjectPermission.TableName, "ObjectCode", obj.ObjectCode) ;

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "ObjectCode",
                   string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Object Code"));
            }

            Context.Status = ret;

        }

        public override async Task UpdateValidation(ObjectPermissionEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                 await Context.CheckUniqueValueForUpdate(_repositories.ObjectPermission.TableName, "ObjectCode",
                    obj.ObjectCode, _repositories.User.PKFieldName, obj.ObjectPermissionID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "ObjectCode",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Object Code"));
            }

            Context.Status = ret;

        }

        public override async Task DeleteValidation(ObjectPermissionEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }


        public async Task<ObjectPermissionEntry> Set(ObjectPermissionEntry model, object userid)
        {
            ObjectPermissionEntry ret = null;
            
            if (model.ObjectPermissionID == 0)
            {
                model.ObjectPermissionID = Helpers.Utilities.GenerateId();
            }
            this.PKValue = model.ObjectPermissionID.ToString();

            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.ObjectPermission.ReadObject(new ObjectPermissionParam()
                             { pObjectPermissionID = model.ObjectPermissionID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.ObjectPermission.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.ObjectPermission.Update(model);
                      }                     
                  );

            return ret;
        }


        public async Task<ObjectPermissionEntry> Remove(ObjectPermissionEntry model, object userid)
        {
            ObjectPermissionEntry ret = null;
            this.PKValue = model.ObjectPermissionID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          return
                             await _repositories.ObjectPermission.ReadObject(new ObjectPermissionParam()
                             { pObjectPermissionID = model.ObjectPermissionID });
                      }
                      ,
                      async (model) =>
                      {
                          await _repositories.ObjectPermission.Delete(model);
                      }

                  );

            return ret;
        }



    }
}
