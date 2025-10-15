using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using MyApp.Proxys;

namespace MyApp.ViewModel
{
    public class RoleViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public RoleViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView(user);
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;

        public RoleResult result = new RoleResult();
        public RoleParam param = new RoleParam() { };
        public List<RoleResult> searchresult = new List<RoleResult>();

        public DefaultLocalization texts = null;

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                new ExceptionMessage("RoleName",""),                
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

            RoleEntry entry = new RoleEntry(result);

            APIResponse<RoleEntry> ret
                = await _Proxys.Role.Set(entry);

            SetResult<RoleEntry>(ret, ref entry, ref ServiceStatus);

        }

        public override async Task Get(object id)
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<RoleResult> ret
                = await _Proxys.Role.Get(id.ToString());

            SetResult<RoleResult>(ret, ref result, ref ServiceStatus);           

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
            result = new RoleResult();
            result.IsActive = true;
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<List<RoleResult>> ret
               = await _Proxys.Role.Search(param);

            SetResult<List<RoleResult>>(ret, ref searchresult, ref ServiceStatus);           

        }

    }
}
