using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using MyApp.Proxys;
using FullDevToolKit.Core.Common;

namespace MyApp.ViewModel
{
    public class LocalizationTextViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public LocalizationTextViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView(user);
            _Proxys.Init(http, serviceurl, token);
        }

        UserAuthenticated _user;

        public LocalizationTextResult result = new LocalizationTextResult();
        public LocalizationTextParam param = new LocalizationTextParam();
        public PagedList<LocalizationTextResult> searchresult = new PagedList<LocalizationTextResult>();
        public IQueryable<LocalizationTextResult> gridlist = null;
        public List<LanguageList> listLangs = new List<LanguageList>();

        public LocalizationSelectStringValues _selectvalues = null;

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {
                new ExceptionMessage("Language",""),
                new ExceptionMessage("Code",""),
                new ExceptionMessage("Name",""),
                new ExceptionMessage("Text","")
            };

        }

        public override async Task InitializeModels()
        {
            await ClearSummaryValidation();
            await LoadLangsList();

            _selectvalues = new LocalizationSelectStringValues();   
        }

        public async Task LoadLangsList()
        {
            listLangs = new List<LanguageList>();

            ServiceStatus = new ExecutionStatus(true);
            listLangs = await _cache.ListLanguages();

            if (listLangs == null)
            {
                listLangs = new List<LanguageList>();

            }

            listLangs.Insert(0, new LanguageList() { LanguageID = 0, LanguageName = this.texts.Get("SelectItem-Description") });

        }

        public override async Task Set()
        {
            ServiceStatus = new ExecutionStatus(true);

            LocalizationTextEntry entry = new LocalizationTextEntry(result);

            entry.LanguageID = long.Parse(_selectvalues.LanguageID);

            APIResponse<LocalizationTextEntry> ret
                = await _Proxys.LocalizationText.Set(entry);

            SetResult<LocalizationTextEntry>(ret, ref entry, ref ServiceStatus);

        }

        public override async Task Remove()
        {
            ServiceStatus = new ExecutionStatus(true);

            LocalizationTextEntry entry = new LocalizationTextEntry(result);

            APIResponse<LocalizationTextEntry> ret
                = await _Proxys.LocalizationText.Remove(entry);

            SetResult<LocalizationTextEntry>(ret, ref entry, ref ServiceStatus);
        }

        public override async Task Get(object id)
        {

            ServiceStatus = new ExecutionStatus(true);

            APIResponse<LocalizationTextResult> ret
                = await _Proxys.LocalizationText.Get(id.ToString());
          
            SetResult<LocalizationTextResult>(ret, ref result, ref ServiceStatus);

            if (result != null)
            {
                _selectvalues.LanguageID = result.LanguageID.ToString();

            }

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
            result = new LocalizationTextResult();

            _selectvalues = new LocalizationSelectStringValues();
        }

        public override async Task Search()
        {

            ServiceStatus = new ExecutionStatus(true);

            param.pLanguageID = long.Parse(_selectvalues.pLanguageID);

            APIResponse<PagedList<LocalizationTextResult>> ret
               = await _Proxys.LocalizationText.Search(param);

            SetResult<PagedList<LocalizationTextResult>>(ret, ref searchresult, ref ServiceStatus);
            gridlist = searchresult.RecordList.AsQueryable();
        }

    }

    public class LocalizationSelectStringValues
    {
        public string pLanguageID { get; set; } = "0";

        public string LanguageID { get; set; } = "0";

    }


}
