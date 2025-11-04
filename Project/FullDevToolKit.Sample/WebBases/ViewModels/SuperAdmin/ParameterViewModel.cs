using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using MyApp.Proxys;

namespace MyApp.ViewModel
{
    public class ParameterViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public ParameterViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView(user);
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;

        public ParameterResult result = new ParameterResult();
        public ParameterParam param = new ParameterParam() { };
        public List<ParameterResult> searchresult = new List<ParameterResult>();
        public List<GroupParameterList> listgroupparameter = new List<GroupParameterList>();    
        public DefaultLocalization texts = null;

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                new ExceptionMessage("ParameterName",""),
                new ExceptionMessage("GroupParameterID",""),                
                new ExceptionMessage("IsActive", ""),
            };

        }

        public async Task LoadGroupParameterList()
        {
            listgroupparameter = new List<GroupParameterList>();

            ServiceStatus = new ExecutionStatus(true);
            listgroupparameter = await _cache.ListGroupParameter();

            if (listgroupparameter == null)
            {
                listgroupparameter = new List<GroupParameterList>();

            }

            listgroupparameter.Insert(0, new GroupParameterList() { GroupParameterID = 0, GroupParameterName = this.texts.Get("SelectItem-Description") });

        }

        public override async Task InitializeModels()
        {
            await ClearSummaryValidation();    
            await LoadGroupParameterList(); 
        }


        public override async Task Set()
        {
            ServiceStatus = new ExecutionStatus(true);

            ParameterEntry entry = new ParameterEntry(result);

            APIResponse<ParameterEntry> ret
                = await _Proxys.Parameter.Set(entry);

            SetResult<ParameterEntry>(ret, ref entry, ref ServiceStatus);

        }

        public override async Task Remove()
        {
            
        }

        public override async Task Get(object id)
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<ParameterResult> ret
                = await _Proxys.Parameter.Get(id.ToString());

            SetResult<ParameterResult>(ret, ref result, ref ServiceStatus);

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
            result = new ParameterResult();
            result.IsActive = true;
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<List<ParameterResult>> ret
               = await _Proxys.Parameter.Search(param);

            SetResult<List<ParameterResult>>(ret, ref searchresult, ref ServiceStatus);

        }

    }
}
