using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using MyApp.Proxys;

namespace MyApp.ViewModel
{
    public class GroupParameterViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public GroupParameterViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView(user);
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;

        public GroupParameterResult result = new GroupParameterResult();
        public GroupParameterParam param = new GroupParameterParam() { };
        public List<GroupParameterResult> searchresult = new List<GroupParameterResult>();

        public DefaultLocalization texts = null;

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                new ExceptionMessage("GroupParameterName",""),
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

            GroupParameterEntry entry = new GroupParameterEntry(result);

            APIResponse<GroupParameterEntry> ret
                = await _Proxys.GroupParameter.Set(entry);

            SetResult<GroupParameterEntry>(ret, ref entry, ref ServiceStatus);

        }

        public override async Task Get(object id)
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<GroupParameterResult> ret
                = await _Proxys.GroupParameter.Get(id.ToString());

            SetResult<GroupParameterResult>(ret, ref result, ref ServiceStatus);

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
            result = new GroupParameterResult();
            result.IsActive = true;
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<List<GroupParameterResult>> ret
               = await _Proxys.GroupParameter.Search(param);

            SetResult<List<GroupParameterResult>>(ret, ref searchresult, ref ServiceStatus);

        }

    }
}
