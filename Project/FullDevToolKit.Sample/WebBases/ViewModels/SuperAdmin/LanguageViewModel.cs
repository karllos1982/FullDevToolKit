using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common; 
using MyApp.Proxys;


namespace MyApp.ViewModel
{
    public class LanguageViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public LanguageViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView(user);
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;

        public LanguageResult result = new LanguageResult();
        public LanguageParam param = new LanguageParam() { };
        public List<LanguageResult> searchresult = new List<LanguageResult>();
        public IQueryable<LanguageResult> gridlist = null;
        
        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {                
                new ExceptionMessage("LanguageName", ""),
                new ExceptionMessage("Description", "")                
            };

        }

        public override async Task InitializeModels()
        {
            await ClearSummaryValidation();


        }


        public override async Task Set()
        {
            ServiceStatus = new ExecutionStatus(true);

            LanguageEntry entry = new LanguageEntry(result);

            APIResponse<LanguageEntry> ret
                = await _Proxys.Language.Set(entry);

            SetResult<LanguageEntry>(ret, ref entry, ref ServiceStatus);

        }

        public override async Task Remove()
        {

        }

        public override async Task Get(object id)
        {

            ServiceStatus = new ExecutionStatus(true);

            APIResponse<LanguageResult> ret
                = await _Proxys.Language.Get(id.ToString());

            SetResult<LanguageResult>(ret, ref result, ref ServiceStatus);

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
            result = new LanguageResult();            
        }

        public override async Task Search()
        {
            ServiceStatus = new ExecutionStatus(true);

            APIResponse<List<LanguageResult>> ret
               = await _Proxys.Language.Search(param);

            SetResult<List<LanguageResult>>(ret, ref searchresult, ref ServiceStatus);
            gridlist = searchresult.AsQueryable();
        }

    }
}
