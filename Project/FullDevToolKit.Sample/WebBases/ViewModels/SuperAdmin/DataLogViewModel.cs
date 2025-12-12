using FullDevToolKit.Common;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Models.Common;
using MyApp.Proxys;
using System.Collections.Generic;
using FullDevToolKit.Core.Common;

namespace MyApp.ViewModel
{
    public class DataLogViewModel : BaseViewModel
    {

        private SystemProxy _Proxys;
        private DataCacheProxy _cache;

        public DataLogViewModel(SystemProxy service, DataCacheProxy cache,
            UserAuthenticated user, HttpClient http, string serviceurl, string token)
        {
            _user = user;
            _Proxys = service;
            _cache = cache;
            this.InitializeView();
            _Proxys.Init(http, serviceurl, token);
            param = new DataLogParam();
        }

        UserAuthenticated _user;


        public DataLogEntry entry = new DataLogEntry();
        public DataLogResult result = new DataLogResult();
        public DataLogParam param = new DataLogParam(); 
        public PagedList<DataLogResult> searchresult = new PagedList<DataLogResult>();
        public IQueryable<DataLogResult> gridlist = null;
        public List<TipoOperacaoValueModel> listTipoOperacao = new List<TipoOperacaoValueModel>();
        public List<TabelasValueModel> listTabelas = new List<TabelasValueModel>();
        
        public IQueryable<DataLogItem> logold_content;
        public IQueryable<DataLogItem> logcurrent_content;
        public List<DataLogTimelineModel> timeline = null;
        public bool ShowTimeline = false;
        

        public DateTime? dataInicio { get; set; }
        public DateTime? dataFim { get; set; }

        public string idDataLog { get; set; }

        public override async Task ClearSummaryValidation()
        {
            SummaryValidation = new List<ExceptionMessage>()
            {

            };
          
        }

        public override async Task InitializeModels()
        {
            await ClearSummaryValidation();
            await this.LoadTipoOperacaoList(_cache);
            await this.LoadTabelaList(_cache);
        }

        public async Task LoadTipoOperacaoList(DataCacheProxy cache)
        {

            ServiceStatus = new ExecutionStatus(true);

            listTipoOperacao 
                = new List<TipoOperacaoValueModel>()
                    {
                        new TipoOperacaoValueModel(){ Value="I", Text=this.texts.Get("InsertOperation-Text") },
                        new TipoOperacaoValueModel(){ Value="U", Text=this.texts.Get("UpdateOperation-Text")},
                        new TipoOperacaoValueModel(){ Value="D", Text=this.texts.Get("DeleteOperation-Text")}
                    };

            if (listTipoOperacao != null)
            {
                listTipoOperacao.Insert(0, new TipoOperacaoValueModel() { Value = "0", Text = this.texts.Get("AllItem-Description") });
            }
            else
            {
                listTipoOperacao = new List<TipoOperacaoValueModel>();
            }

        }

        public async Task LoadTabelaList(DataCacheProxy cache)
        {

            ServiceStatus = new ExecutionStatus(true);
            listTabelas = await cache.ListTabelas();

            if (listTabelas != null)
            {
                listTabelas.Insert(0, new TabelasValueModel() { Value = "0", Text = this.texts.Get("AllItem-Description") });
            }
            else
            {
                listTabelas = new List<TabelasValueModel>();
            }

        }

        public override async Task Set()
        {


        }

        public override async Task Remove()
        {

        }

        public override async Task Get(object id)
        {

            ServiceStatus = new ExecutionStatus(true);

            APIResponse<DataLogResult> ret
                = await _Proxys.DataLog.Get(id.ToString());

            SetResult<DataLogResult>(ret, ref result, ref ServiceStatus); 
          
        }

        public async Task GetTimeline()
        {

            ServiceStatus = new ExecutionStatus(true);

            APIResponse<List<DataLogTimelineModel>?> ret = 
                await _Proxys.DataLog.GetTimeLine(result.ID.ToString());

            SetResult<List<DataLogTimelineModel>>(ret, ref timeline, ref ServiceStatus);

        }

        public override void BackToSearch()
        {
            this.BaseBack();
            this.ShowTimeline = false;
        }

        public override void InitEdit()
        {
            this.BaseInitEdit();

            this.ModoLabel = texts.Get("DetailsLabel");

            if (result != null)
            {
                GetDataLogContent(result);

            }

        }

        public void GetDataLogContent(DataLogResult log)
        {
            logold_content = null;
            logcurrent_content = null;

            List<DataLogItem> oldlist = null;
            List<DataLogItem> currentlist = null;

            if (log.LogOldData != null)
            {
                if (log.LogOldData != "")
                {
                    oldlist 
                        = _Proxys.DataLog.GetDataLogItems(log.LogOldData);                    
                }
            }

            if (log.LogCurrentData != null)
            {
                if (log.LogCurrentData != "")
                {
                    currentlist 
                        = _Proxys.DataLog.GetDataLogItems(log.LogCurrentData);                    
                }
            }

            if (log.Operation == "U")
            {
                _Proxys.DataLog
                    .GetDataLogDiff(ref oldlist, ref currentlist);
            }

            if (oldlist != null)
            {
                logold_content = oldlist.AsQueryable(); 
            }

            if (currentlist != null)
            {
                logcurrent_content = currentlist.AsQueryable();
            }

        }

        public override void InitNew()
        {
            this.BaseInitNew();
        }

        public override async Task Search()
        {

            ServiceStatus = new ExecutionStatus(true);

            if (dataInicio != null && dataFim != null)
            {
                ((DataLogParam)param).pDate_Start = dataInicio.Value;
                ((DataLogParam)param).pData_End = dataFim.Value;
                ((DataLogParam)param).SearchByDate = true;
            }
            else
            {
                dataInicio = null;
                dataFim = null;
            }

            long aux = 0;
            long.TryParse(idDataLog, out aux);
            ((DataLogParam)param).pID = aux;

            APIResponse<PagedList<DataLogResult>> ret
               = await _Proxys.DataLog.Search(((DataLogParam)param));

            SetResult<PagedList<DataLogResult>>(ret, ref searchresult, ref ServiceStatus);

            gridlist = searchresult.RecordList.AsQueryable();

        }

    }

}
