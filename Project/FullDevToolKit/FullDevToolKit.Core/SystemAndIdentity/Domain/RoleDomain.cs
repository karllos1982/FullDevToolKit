﻿using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;

namespace FullDevToolKit.Sys.Domains
{
    public class RoleDomain : IRoleDomain
    {
     
        public RoleDomain(ISystemRepositorySet repositorySet)
        {
            RepositorySet = repositorySet;
            Context = RepositorySet.Role.Context;
        }

        public IContext Context { get; set; }

        public ISystemRepositorySet RepositorySet { get; set; }

        public async Task<RoleResult> FillChields(RoleResult obj)
        {
            return obj;
        }

        public async Task<RoleResult> Get(RoleParam param)
        {
            RoleResult ret = null;

            ret = await RepositorySet.Role.Read(param); 
            
            return ret;
        }

        public async Task<List<RoleList>> List(RoleParam param)
        {
            List<RoleList> ret = null;

            ret = await RepositorySet.Role.List(param);           

            return ret;
        }

        public async Task<List<RoleResult>> Search(RoleParam param)
        {
            List<RoleResult> ret = null;

            ret = await RepositorySet.Role.Search(param);

            return ret;
        }
        public async Task EntryValidation(RoleEntry obj)
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

        public async Task InsertValidation(RoleEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForInsert(RepositorySet.Role.TableName, "RoleName", obj.RoleName);

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "RoleName",
                   string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Role Name"));
            }  

            Context.Status = ret;
          
        }

        public async Task UpdateValidation(RoleEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await RepositorySet.Role.Context.CheckUniqueValueForUpdate(RepositorySet.Role.TableName, "RoleName",
                obj.RoleName, RepositorySet.User.PKFieldName, obj.RoleID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "RoleName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Role Name"));
            }

            Context.Status = ret;

        }

        public async Task DeleteValidation(RoleEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }

        public async Task<RoleEntry> Set(RoleEntry model, object userid)
        {
            RoleEntry ret = null;
            OPERATIONLOGENUM operation = OPERATIONLOGENUM.INSERT;

            await EntryValidation(model);

            if (Context.Status.Success)
            {

                RoleResult old 
                    = await RepositorySet.Role.Read(new RoleParam() { pRoleID = model.RoleID });

                if (old == null)
                {
                    await InsertValidation(model);

                    if (Context.Status.Success)
                    {
                        model.CreateDate = DateTime.Now;
                        if (model.RoleID == 0) { model.RoleID = FullDevToolKit.Helpers.Utilities.GenerateId(); }
                        await RepositorySet.Role.Create(model);
                    }
                }
                else
                {
                    model.CreateDate = old.CreateDate;
                    operation = OPERATIONLOGENUM.UPDATE;

                    await UpdateValidation(model);

                    if (Context.Status.Success)
                    {
                        await RepositorySet.Role.Update(model);
                    }

                }

                if (Context.Status.Success && userid != null)
                {
                   await  RepositorySet.Role.Context
                        .RegisterDataLogAsync(userid.ToString(), operation, "SYSROLE",
                        model.RoleID.ToString(), old, model);

                    ret = model;
                }

            }     

            return ret;
        }
      
        public async Task<RoleEntry> Delete(RoleEntry model, object userid)
        {
            RoleEntry ret = null;

            RoleResult old 
                = await RepositorySet.Role.Read(new RoleParam() { pRoleID = model.RoleID });

            if (old != null)
            {
                await DeleteValidation(model);

                if (Context.Status.Success)
                {
                   await RepositorySet.Role.Delete(model);
                    if (Context.Status.Success && userid != null)
                    {
                        await RepositorySet.User.Context
                            .RegisterDataLogAsync(userid.ToString(), OPERATIONLOGENUM.DELETE, "SYSROLE",
                            model.RoleID.ToString(), old, model);

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