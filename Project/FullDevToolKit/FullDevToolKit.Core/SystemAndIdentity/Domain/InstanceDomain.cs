﻿using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;


namespace FullDevToolKit.Sys.Domains
{
    public class InstanceDomain : IInstanceDomain
    {
       
        public InstanceDomain(ISystemRepositorySet repositorySet)
        {            
            RepositorySet = repositorySet;
            Context = RepositorySet.Instance.Context;
        }

        public IContext Context { get; set; }

        public ISystemRepositorySet RepositorySet { get; set; }

        public async Task<InstanceResult> FillChields(InstanceResult obj)
        {
            return obj;
        }

        public async Task<InstanceResult> Get(InstanceParam param)
        {
            InstanceResult ret = null;

            ret = await RepositorySet.Instance.Read(param);

            return ret;
        }

        public async Task<List<InstanceList>> List(InstanceParam param)
        {
            List<InstanceList> ret = null;

            ret = await RepositorySet.Instance.List(param);

            return ret;
        }

        public async Task<List<InstanceResult>> Search(InstanceParam param)
        {
            List<InstanceResult> ret = null;

            ret = await RepositorySet.Instance.Search(param);

            return ret;
        }

        public async Task EntryValidation(InstanceEntry obj)
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

        public async Task InsertValidation(InstanceEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);


            bool check =
              await Context.CheckUniqueValueForInsert(RepositorySet.Instance.TableName, "InstanceName", obj.InstanceName);

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "InstanceName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Instance Name"));
              
            }

            Context.Status = ret;

        }

        public async Task UpdateValidation(InstanceEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
              await Context.CheckUniqueValueForUpdate(RepositorySet.Instance.TableName, "InstanceName",
                    obj.InstanceName, RepositorySet.User.PKFieldName,obj.InstanceID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "InstanceName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Instance Name"));

            }
       
            Context.Status = ret;

        }

        public async Task DeleteValidation(InstanceEntry obj)
        {
             Context.Status = new ExecutionStatus(true);
        }

        public async Task<InstanceEntry> Set(InstanceEntry model, object userid)
        {
            InstanceEntry ret = null;
            OPERATIONLOGENUM operation = OPERATIONLOGENUM.INSERT;

            await EntryValidation(model);

            if (Context.Status.Success)
            {

                InstanceResult old
                    = await RepositorySet.Instance.Read(new InstanceParam() { pInstanceID = model.InstanceID });

                if (old == null)
                {
                    await InsertValidation(model);

                    if (Context.Status.Success)
                    {
                        model.CreateDate = DateTime.Now;
                        if (model.InstanceID == 0) { model.InstanceID = FullDevToolKit.Helpers.Utilities.GenerateId(); }
                        await RepositorySet.Instance.Create(model);
                    }
                }
                else
                {
                    model.CreateDate = old.CreateDate;
                    operation = OPERATIONLOGENUM.UPDATE;

                    await UpdateValidation(model);

                    if (Context.Status.Success)
                    {
                        await RepositorySet.Instance.Update(model);
                    }

                }

                if (Context.Status.Success && userid != null)
                {
                    await Context
                        .RegisterDataLogAsync(userid.ToString(), operation, "SYSINSTANCE",
                        model.InstanceID.ToString(), old, model);

                    ret = model;
                }

            }

            return ret;
        }

        public async Task<InstanceEntry> Delete(InstanceEntry model, object userid)
        {
            InstanceEntry ret = null;

            InstanceResult old
                = await RepositorySet.Instance.Read(new InstanceParam() { pInstanceID = model.InstanceID });

            if (old != null)
            {
                await DeleteValidation(model);

                if (Context.Status.Success)
                {
                    await RepositorySet.Instance.Delete(model);

                    if (Context.Status.Success && userid != null)
                    {
                        await Context
                            .RegisterDataLogAsync(userid.ToString(),  OPERATIONLOGENUM.DELETE, "SYSINSTANCE",
                            model.InstanceID.ToString(), old, model);

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