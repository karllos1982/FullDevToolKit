﻿using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Domains;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;

namespace FullDevToolKit.Sys.Domains
{
    public class SessionLogDomain : ISessionLogDomain
    {
        
        public SessionLogDomain(ISystemRepositorySet repositorySet)
        {
            RepositorySet = repositorySet;
            Context = RepositorySet.SessionLog.Context;

        }

        public IContext Context { get; set; }

        public ISystemRepositorySet RepositorySet { get; set; }

        public async Task<SessionLogResult> FillChields(SessionLogResult obj)
        {
            return obj;
        }

        public async Task<SessionLogResult> Get(SessionLogParam param)
        {
            SessionLogResult ret = null;

            ret = await RepositorySet.SessionLog.Read(param); 
            
            return ret;
        }

        public async Task<List<SessionLogList>> List(SessionLogParam param)
        {
            List<SessionLogList> ret = null;

            ret = await RepositorySet.SessionLog.List(param);           

            return ret;
        }

        public async Task<List<SessionLogResult>> Search(SessionLogParam param)
        {
            List<SessionLogResult> ret = null;

            ret = await  RepositorySet.SessionLog.Search(param);

            return ret;
        }

        public async Task EntryValidation(SessionLogEntry obj)
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

        public async Task InsertValidation(SessionLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task UpdateValidation(SessionLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);

        }

        public async Task DeleteValidation(SessionLogEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task<SessionLogEntry> Set(SessionLogEntry model, object userid)
        {
            SessionLogEntry ret = null;
            OPERATIONLOGENUM operation = OPERATIONLOGENUM.INSERT;

             await EntryValidation(model);

            if (Context.Status.Success)
            {

                SessionLogResult old 
                    = await RepositorySet.SessionLog.Read(new SessionLogParam() 
                            { pSessionLogID = model.SessionLogID });

                if (old == null)
                {
                    await InsertValidation(model);

                    if (Context.Status.Success)
                    {
                        if (model.SessionLogID == 0) { model.SessionLogID = FullDevToolKit.Helpers.Utilities.GenerateId(); }
                        await RepositorySet.SessionLog.Create(model);
                    }
                }
                else
                {                    
                    operation = OPERATIONLOGENUM.UPDATE;

                   await UpdateValidation(model);

                    if (Context.Status.Success)
                    {
                       await RepositorySet.SessionLog.Update(model);
                    }

                }

                if (Context.Status.Success && userid != null)
                {
                   await Context
                            .RegisterDataLogAsync(userid.ToString(), operation, "SYSSESSIONLOG",
                                model.SessionLogID.ToString(), old, model);

                    ret= model;
                }

            }     

            return ret;
        }
      
        public async Task<SessionLogEntry> Delete(SessionLogEntry model, object userid)
        {
            SessionLogEntry ret = null;

            SessionLogResult old 
                = await RepositorySet.SessionLog.Read(new SessionLogParam() 
                    { pSessionLogID = model.SessionLogID });

            if (old != null)
            {
                await DeleteValidation(model);

                if (Context.Status.Success)
                {
                    await RepositorySet.SessionLog.Delete(model);

                    if (Context.Status.Success && userid != null)
                    {
                        await Context
                                .RegisterDataLogAsync(userid.ToString(), OPERATIONLOGENUM.DELETE, "SYSSESSIONLOG",
                                    model.SessionLogID.ToString(), old, model);

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