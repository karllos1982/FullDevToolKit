using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using MyApp.Proxys;

namespace MyApp.ViewModel
{
    public class SessionLogViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public SessionLogViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView();
        }

        UserAuthenticated _user;


        public SessionLogEntry entry = new SessionLogEntry();
        public SessionLogResult result = new SessionLogResult();
        public SessionLogParam param = new SessionLogParam();
        public List<SessionLogResult> searchresult = new List<SessionLogResult>();

        public DefaultLocalization texts = null;

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {

            };

           
        }

        public override async Task InitializeModels()
        {
            param.pDate_Start = DateTime.Now.AddDays(-7);
            param.pData_End = DateTime.Now;

            await ClearSummaryValidation();
            await this.InitLocalization(_cache, _user.LocalizationLanguage);

        }

        public override async Task Set()
        {


        }

        public override async Task Get(object id)
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<SessionLogResult> ret
                = await _Proxys.SessionLog.Get(id.ToString());

            SetResult<SessionLogResult>(ret, ref result, ref ServiceStatus);
         
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
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<List<SessionLogResult>> ret
               = await _Proxys.SessionLog.Search(param);

            SetResult<List<SessionLogResult>>(ret, ref searchresult, ref ServiceStatus);
          
        }

    }
}
