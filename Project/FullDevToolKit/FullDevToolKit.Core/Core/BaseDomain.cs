using FullDevToolKit.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Data.Repositories;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Core
{ 
    public abstract class BaseDomain<TParam, TEntry, TList, TResult>
    {
       
        public IContext Context { get; set; }

        public string TableName { get;set; }

        public string PKValue {  get;set; } 
        

        public abstract Task<TResult> FillChields(TResult obj); 
       
            

        public async Task EntryValidation(TEntry obj,Func<TEntry, Task<ExecutionStatus>> method)
        {
            ExecutionStatus ret = null;

            ret = PrimaryValidation.Execute(obj, new List<string>(), Context.LocalizationLanguage);

            if (!ret.Success)
            {
                ret.SetFailStatus("Error",
                   LocalizationText.Get("Validation-Error", Context.LocalizationLanguage).Text);
            }
            else
            {
                if (method != null)
                {
                    ret = await method(obj);
                }
            }

            Context.Status = ret;

        }


        public abstract Task InsertValidation(TEntry obj);

        public abstract Task UpdateValidation(TEntry obj);

        public abstract Task DeleteValidation(TEntry obj);

      
        public async Task<TEntry> ExecutionForSet(TEntry model, object userid,
            Func<TEntry, Task<TResult>> readmethod,
            Func<TEntry, Task> createmethod,
            Func<TEntry, Task> updatemethod,
            Func<TEntry, Task<ExecutionStatus>> validationmethod=null,
            Func<TEntry, Task> childsetmethod = null)
        {
            TEntry ret = default;

            OPERATIONLOGENUM operation = OPERATIONLOGENUM.INSERT;

            await EntryValidation(model,validationmethod);

            if (Context.Status.Success)
            {

                TResult old = await readmethod(model);
                    
                if (old == null)
                {
                    await InsertValidation(model);
                    if (Context.Status.Success)
                    {                        
                        await createmethod(model);
                    }
                }
                else
                {
                    operation = OPERATIONLOGENUM.UPDATE;

                    await UpdateValidation(model);

                    if (Context.Status.Success)
                    {
                        await updatemethod(model);                        
                    }

                }

                if (childsetmethod != null)
                {
                    await childsetmethod(model);
                }
                
                if (Context.Status.Success && userid != null)
                {
                    await Context
                         .RegisterDataLogAsync(userid.ToString(), operation, this.TableName,
                          this.PKValue, old, model);

                    ret = model;
                }

            }

            return ret;
        }



        public async Task<TEntry> ExecutionForDelete(TEntry model, object userid,
             Func<TEntry, Task<TResult>> readmethod,
             Func<TEntry, Task> deletemethod=null,
             Func<TResult, Task> childsetmethod = null)
        {
            TEntry ret = default;

            TResult old = await readmethod(model);             

            if (old != null)
            {
                await DeleteValidation(model);

                if (Context.Status.Success)
                {

                    if (Context.Status.Success &&  childsetmethod != null)
                    {
                        await childsetmethod (old);
                    }

                    if (Context.Status.Success)
                    {
                        await deletemethod(model);

                    }

                    if (Context.Status.Success && userid != null)
                    {
                        await Context
                                .RegisterDataLogAsync(userid.ToString(), OPERATIONLOGENUM.DELETE, this.TableName ,
                                    this.PKValue, old, model);

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
