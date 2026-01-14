using FullDevToolKit.Common;
using MyApp.Models;
using MyApp.Proxys;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Helpers;
using FullDevToolKit.Core.Common;
using FullDevToolKit.Sys.Models.Common;

namespace MyApp.ViewModel
{
    public class SimpleEntityViewModel : BaseViewModel
    {
        private MyAppProxy _proxys;
        private DataCacheProxy _cache;

        public SimpleEntityViewModel(MyAppProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _proxys = service;
            _cache = cache;
            this.InitializeView(user);
            _proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;

        public SimpleEntityResult result = new SimpleEntityResult();
        public SimpleEntityParam param = new SimpleEntityParam() { };
        public PagedList<SimpleEntityResult> searchresult = new PagedList<SimpleEntityResult>();
        public IQueryable<SimpleEntityResult> gridlist = null;

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                new ExceptionMessage("SimpleEntityName",""),
                new ExceptionMessage("SimpleEntityDescription","")
            };
        }

        public override async Task InitializeModels()
        {
            await ClearSummaryValidation();
        }

        public override async Task Set()
        {
            ServiceStatus = new ExecutionStatus(true);

            SimpleEntityEntry entry = new SimpleEntityEntry(result);

            APIResponse<SimpleEntityEntry> ret
              = await _proxys.SimpleEntity.Set(entry);

            SetResult<SimpleEntityEntry>(ret, ref entry, ref ServiceStatus);
        }

        public override async Task Remove()
        {
            ServiceStatus = new ExecutionStatus(true);

            SimpleEntityEntry entry = new SimpleEntityEntry(result);

            APIResponse<SimpleEntityEntry> ret
                = await _proxys.SimpleEntity.Remove(entry);

            SetResult<SimpleEntityEntry>(ret, ref entry, ref ServiceStatus);
        }

        public override async Task Get(object id)
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<SimpleEntityResult> ret
                = await _proxys.SimpleEntity.Get(id.ToString());

            SetResult<SimpleEntityResult>(ret, ref result, ref ServiceStatus);
        }

        public override void BackToSearch()
        {
            this.BaseBack();
        }

        public override void InitEdit()
        {
            this.BaseInitEdit();
        }

        public override void InitNew()
        {
            this.BaseInitNew();
            result = new SimpleEntityResult();
            result.SimpleEntityID = Utilities.GenerateId();
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<PagedList<SimpleEntityResult>> ret
               = await _proxys.SimpleEntity.Search(param);

            SetResult<PagedList<SimpleEntityResult>>(ret, ref searchresult, ref ServiceStatus);
            gridlist = searchresult.RecordList.AsQueryable();
        }
    }
}