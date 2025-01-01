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
            UserAuthenticated user)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView(user);
        }

        UserAuthenticated _user;

        public ParameterResult result = new ParameterResult();
        public ParameterParam param = new ParameterParam() { };
        public List<ParameterResult> searchresult = new List<ParameterResult>();

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

        public override async Task InitializeModels()
        {

            await ClearSummaryValidation();
            await this.InitLocalization(_cache, _user.LocalizationLanguage);
        }


        public override async Task Set()
        {
            ServiceStatus = new ExecutionStatus(true);

            ParameterEntry entry = new ParameterEntry(result);

            APIResponse<ParameterEntry> ret
                = await _Proxys.Parameter.Set(entry);

            SetResult<ParameterEntry>(ret, ref entry, ref ServiceStatus);

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
