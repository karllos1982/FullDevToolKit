using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;

namespace FullDevToolKit.Sys.Domains
{
    public class ObjectPermissionDomain : IObjectPermissionDomain
    {
      
        public ObjectPermissionDomain(ISystemRepositorySet repositorySet)
        {
            RepositorySet = repositorySet;
            Context = RepositorySet.ObjectPermission.Context;
        }

        public IContext Context { get; set; }

        public ISystemRepositorySet RepositorySet { get; set; }

        public async Task<ObjectPermissionResult> FillChields(ObjectPermissionResult obj)
        {
            return obj;
        }

        public async Task<ObjectPermissionResult> Get(ObjectPermissionParam param)
        {
            ObjectPermissionResult ret = null;

            ret = await RepositorySet.ObjectPermission.Read(param); 
            
            return ret;
        }

        public async Task<List<ObjectPermissionList>> List(ObjectPermissionParam param)
        {
            List<ObjectPermissionList> ret = null;

            ret = await RepositorySet.ObjectPermission.List(param);           

            return ret;
        }

        public async Task<List<ObjectPermissionResult>> Search(ObjectPermissionParam param)
        {
            List<ObjectPermissionResult> ret = null;

            ret = await RepositorySet.ObjectPermission.Search(param);

            return ret;
        }

        public async Task EntryValidation(ObjectPermissionEntry obj)
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

        public async Task InsertValidation(ObjectPermissionEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);


            bool check =
                    await Context.CheckUniqueValueForInsert(RepositorySet.ObjectPermission.TableName, "ObjectCode", obj.ObjectCode) ;

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "ObjectCode",
                   string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Object Code"));
            }

            Context.Status = ret;

        }

        public async Task UpdateValidation(ObjectPermissionEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                 await Context.CheckUniqueValueForUpdate(RepositorySet.ObjectPermission.TableName, "ObjectCode",
                    obj.ObjectCode, RepositorySet.User.PKFieldName, obj.ObjectPermissionID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "ObjectCode",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Object Code"));
            }

            Context.Status = ret;

        }

        public async Task DeleteValidation(ObjectPermissionEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }

        public async Task<ObjectPermissionEntry> Set(ObjectPermissionEntry model, object userid)
        {
            ObjectPermissionEntry ret = null;
            OPERATIONLOGENUM operation = OPERATIONLOGENUM.INSERT;

           await EntryValidation(model);

            if (Context.Status.Success)
            {

                ObjectPermissionResult old 
                    = await RepositorySet.ObjectPermission
                        .Read(new ObjectPermissionParam() { pObjectPermissionID = model.ObjectPermissionID });

                if (old == null)
                {
                   await InsertValidation(model);

                    if (Context.Status.Success)
                    {
                        if (model.ObjectPermissionID == 0) { model.ObjectPermissionID = FullDevToolKit.Helpers.Utilities.GenerateId(); }
                        await RepositorySet.ObjectPermission.Create(model);
                    }
                }
                else
                {                    
                    operation = OPERATIONLOGENUM.UPDATE;

                   await UpdateValidation(model);

                    if (Context.Status.Success)
                    {
                         await RepositorySet.ObjectPermission.Update(model);
                    }

                }

                if (Context.Status.Success && userid != null)
                {
                   await Context
                        .RegisterDataLogAsync(userid.ToString(), operation, "SYSOBJECTPERMISSION",
                         model.ObjectPermissionID.ToString(), old, model);

                    ret = model;
                }

            }     

            return ret;
        }
      
        public async Task<ObjectPermissionEntry> Delete(ObjectPermissionEntry model, object userid)
        {
            ObjectPermissionEntry ret = null;

            ObjectPermissionResult old 
                = await RepositorySet.ObjectPermission
                    .Read(new ObjectPermissionParam() { pObjectPermissionID = model.ObjectPermissionID });

            if (old != null)
            {
                await DeleteValidation(model);

                if (Context.Status.Success)
                {
                     await RepositorySet.ObjectPermission.Delete(model);

                    if (Context.Status.Success && userid != null)
                    {
                        await Context
                                .RegisterDataLogAsync(userid.ToString(), OPERATIONLOGENUM.DELETE, "SYSOBJECTPERMISSION",
                                    model.ObjectPermissionID.ToString(), old, model);

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
