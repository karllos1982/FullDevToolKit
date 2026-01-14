using FullDevToolKit.Common;
using FullDevToolKit.Core;
using MyApp.Contracts.Domains;
using MyApp.Models;
using MyApp.Contracts.Repositories;
using FullDevToolKit.Helpers;
using MyApp.Data.Repositories;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Core.Common;

namespace MyApp.Domain
{
    public class SimpleEntityDomain
                   : BaseDomain<SimpleEntityParam, SimpleEntityEntry, SimpleEntityList, SimpleEntityResult>, ISimpleEntityDomain
    {
        public SimpleEntityDomain(IContext context)
        {
            Context = context;
            RepositorySet = new MyAppRepositorySet(context);
            this.TableName = RepositorySet.SimpleEntity.TableName;
        }

        private IMyAppRepositorySet RepositorySet { get; set; }

        public override async Task<SimpleEntityResult> FillChields(SimpleEntityResult obj)
        {
            return obj;
        }

        public async Task<SimpleEntityResult> Get(SimpleEntityParam param)
        {
            SimpleEntityResult ret = null;

            ret = await RepositorySet.SimpleEntity.ReadObject(param);

            if (ret != null)
            {
                await FillChields(ret);
            }

            return ret;
        }

        public async Task<List<SimpleEntityList>> List(SimpleEntityParam param)
        {
            List<SimpleEntityList> ret = null;

            ret = await RepositorySet.SimpleEntity.ReadList(param);

            return ret;
        }

        public async Task<PagedList<SimpleEntityResult>> Search(SimpleEntityParam param)
        {
            PagedList<SimpleEntityResult> ret = null;

            ret = await RepositorySet.SimpleEntity.ReadSearch(param);

            return ret;
        }

        public override async Task InsertValidation(SimpleEntityEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
                await Context.CheckUniqueValueForInsert(RepositorySet.SimpleEntity.TableName, "SimpleEntityName", obj.SimpleEntityName);

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "SimpleEntityName",
                  string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Simple Entity Name"));
            }

            Context.Status = ret;
        }

        public override async Task UpdateValidation(SimpleEntityEntry obj)
        {
            ExecutionStatus ret = new ExecutionStatus(true);

            bool check =
              await Context.CheckUniqueValueForUpdate(RepositorySet.SimpleEntity.TableName, "SimpleEntityName",
              obj.SimpleEntityName, RepositorySet.SimpleEntity.PKFieldName, obj.SimpleEntityID.ToString());

            if (!check)
            {
                PrimaryValidation.AddCheckValidationException(ref ret, "SimpleEntityName",
                    string.Format(LocalizationText.Get("Validation-Unique-Value", Context.LocalizationLanguage).Text, "Simple Entity Name"));
            }

            Context.Status = ret;
        }

        public override async Task DeleteValidation(SimpleEntityEntry obj)
        {
            Context.Status = new ExecutionStatus(true);
        }

        public async Task<SimpleEntityEntry> Set(SimpleEntityEntry model, object userid)
        {
            SimpleEntityEntry ret = null;

            if (model.SimpleEntityID == 0)
            {
                model.SimpleEntityID = Utilities.GenerateId();
            }
            this.PKValue = model.SimpleEntityID.ToString();

            ret = await ExecutionForSet(model, userid,
                      async (model) =>
                      {
                          return
                             await RepositorySet.SimpleEntity.ReadObject(new SimpleEntityParam()
                             { pSimpleEntityID = model.SimpleEntityID });
                      }
                      ,
                      async (model) =>
                      {
                          await RepositorySet.SimpleEntity.Create(model);
                      }
                      ,
                      async (model) =>
                      {
                          await RepositorySet.SimpleEntity.Update(model);
                      }
                      ,
                      null
                      ,
                      null
                  );

            return ret;
        }

        public async Task<SimpleEntityEntry> Remove(SimpleEntityEntry model, object userid)
        {
            SimpleEntityEntry ret = null;
            this.PKValue = model.SimpleEntityID.ToString();

            ret = await ExecutionForDelete(model, userid,
                      async (model) =>
                      {
                          SimpleEntityResult simpleEntity
                           = await RepositorySet.SimpleEntity.ReadObject(new SimpleEntityParam()
                           { pSimpleEntityID = model.SimpleEntityID });

                          return simpleEntity;
                      }
                      ,
                      async (model) =>
                      {
                          await RepositorySet.SimpleEntity.Delete(model);
                      }
                      ,
                      null
                  );

            return ret;
        }
    }
}