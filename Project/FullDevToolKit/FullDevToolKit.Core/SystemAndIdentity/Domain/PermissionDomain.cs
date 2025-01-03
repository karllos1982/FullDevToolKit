﻿using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;

namespace FullDevToolKit.Sys.Domains
{
    public class PermissionDomain : IPermissionDomain
    {
       
        public PermissionDomain(IContext context)
        {
            Context = context;
            _repositories = new SystemRepositorySet(context);

        }

        public IContext Context { get; set; }

        private ISystemRepositorySet _repositories { get; set; }


        public async Task<PermissionResult> FillChields(PermissionResult obj)
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

        public async Task EntryValidation(PermissionEntry obj)
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

        public async Task InsertValidation(PermissionEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }

        public async Task UpdateValidation(PermissionEntry obj)
        {
             Context.Status = new ExecutionStatus(true);

        }

        public async Task DeleteValidation(PermissionEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }

        public async Task<PermissionEntry> Set(PermissionEntry model, object userid)
        {
            PermissionEntry ret = null;
            OPERATIONLOGENUM operation = OPERATIONLOGENUM.INSERT;

            await EntryValidation(model);

            if (Context.Status.Success)
            {

                PermissionResult old 
                    = await _repositories.Permission.Read(new PermissionParam() { pPermissionID = model.PermissionID });

                if (old == null)
                {
                    await InsertValidation(model);

                    if (Context.Status.Success)
                    {
                        if (model.PermissionID  == 0) { model.PermissionID = FullDevToolKit.Helpers.Utilities.GenerateId(); }

                        if (model.RoleID != null) { model.TypeGrant = "R"; }
                        if (model.UserID != null) { model.TypeGrant = "U"; }
                        if (model.RoleID != null && model.UserID != null) { model.TypeGrant = "U"; }

                        await _repositories.Permission.Create(model);
                    }
                }
                else
                {                    
                    operation = OPERATIONLOGENUM.UPDATE;

                    await UpdateValidation(model);

                    if (Context.Status.Success)
                    {
                        await _repositories.Permission.Update(model);
                    }

                }

                if (Context.Status.Success && userid != null)
                {
                   await Context
                            .RegisterDataLogAsync(userid.ToString(), operation, "SYSPERMISSION",
                                model.PermissionID.ToString(), old, model);

                    ret = model;
                }

            }     

            return ret;
        }
     

        public async Task<PermissionEntry> Delete(PermissionEntry model, object userid)
        {
            PermissionEntry ret = null;

            PermissionResult old 
                = await _repositories.Permission.Read(new PermissionParam() { pPermissionID = model.PermissionID });

            if (old != null)
            {
                await DeleteValidation(model);

                if (Context.Status.Success)
                {
                    await _repositories.Permission.Delete(model);

                    if (Context.Status.Success && userid != null)
                    {
                        await Context
                                .RegisterDataLogAsync(userid.ToString(), OPERATIONLOGENUM.DELETE, "SYSPERMISSION",
                                model.PermissionID.ToString(), old, model);

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
