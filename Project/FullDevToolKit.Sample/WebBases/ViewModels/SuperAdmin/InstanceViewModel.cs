using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Models.Identity;
using MyApp.Proxys;


namespace MyApp.ViewModel
{
    public class InstanceViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public InstanceViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token )
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView(user);
            _Proxys.Init(http, serviceurl,token);
        }

        UserAuthenticated _user;

        public InstanceResult result = new InstanceResult();
        public InstanceParam param = new InstanceParam() {  };
        public List<InstanceResult> searchresult = new List<InstanceResult>();
        public IQueryable<InstanceResult> gridlist = null;
        public DefaultLocalization texts = null;

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                new ExceptionMessage("InstanceTypeName",""),
                new ExceptionMessage("InstanceName", ""),
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

            InstanceEntry entry = new InstanceEntry(result);

            APIResponse<InstanceEntry> ret
                = await _Proxys.Instance.Set(entry);

            SetResult<InstanceEntry>(ret, ref entry, ref ServiceStatus);

        }

        public override async Task Remove()
        {

        }
        public override async Task Get(object id)
        {

            ServiceStatus = new ExecutionStatus(true);

            APIResponse<InstanceResult> ret
                = await _Proxys.Instance.Get(id.ToString());

            SetResult<InstanceResult>(ret, ref result, ref ServiceStatus);

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
            result = new InstanceResult();
            result.IsActive = true; 
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<List<InstanceResult>> ret
               = await _Proxys.Instance.Search(param);

            SetResult<List<InstanceResult>>(ret, ref searchresult, ref ServiceStatus);
            gridlist = searchresult.AsQueryable();
        }

    }
}
