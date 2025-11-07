using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common; 
using MyApp.Proxys;


namespace MyApp.ViewModel
{
    public class ConfigsViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public ConfigsViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView(user);
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;

        public ConfigsResult result = new ConfigsResult();
        public ConfigsParam param = new ConfigsParam() { };
        public List<ConfigsResult> searchresult = new List<ConfigsResult>();
        public IQueryable<ConfigsResult> gridlist = null;
        
        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {                
                new ExceptionMessage("ConfigName", ""),
                new ExceptionMessage("ConfigValue", ""),
                new ExceptionMessage("IsActive", ""),
            };

        }

        public override async Task InitializeModels()
        {
            await ClearSummaryValidation();


        }


        public override async Task Set()
        {
            ServiceStatus = new ExecutionStatus(true);

            ConfigsEntry entry = new ConfigsEntry(result);

            APIResponse<ConfigsEntry> ret
                = await _Proxys.Configs.Set(entry);

            SetResult<ConfigsEntry>(ret, ref entry, ref ServiceStatus);

        }

        public override async Task Remove()
        {

        }

        public override async Task Get(object id)
        {

            ServiceStatus = new ExecutionStatus(true);

            APIResponse<ConfigsResult> ret
                = await _Proxys.Configs.Get(id.ToString());

            SetResult<ConfigsResult>(ret, ref result, ref ServiceStatus);

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
            result = new ConfigsResult();
            result.IsActive = true;
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<List<ConfigsResult>> ret
               = await _Proxys.Configs.Search(param);

            SetResult<List<ConfigsResult>>(ret, ref searchresult, ref ServiceStatus);
            gridlist = searchresult.AsQueryable();
        }

    }
}
