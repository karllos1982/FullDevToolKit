using FullDevToolKit.Common;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Contracts.Repositories;
using FullDevToolKit.Sys.Models.Identity;
using FullDevToolKit.Sys.Data.QueryBuilders;
using FullDevToolKit.Core.Common;

namespace FullDevToolKit.Sys.Data.Repositories
{
    public class SessionLogRepository : ISessionLogRepository        
    {
       
        public SessionLogRepository(IContext context)
        {
            Context = context;
            TableName = "sysSessionLog";
            PKFieldName = "SessionLogID";

        }
         
        private SessionLogQueryBuilder query = new SessionLogQueryBuilder();

        public string TableName { get; set; }

        public string PKFieldName { get; set; }

        public IContext Context { get; set; }

        public async Task Create(SessionLogEntry model)
        {
            
            string sql = query.QueryForCreate(TableName, model,model);
            await Context.ExecuteAsync(sql, model);
            
        }

        public async Task<SessionLogResult> ReadObject(SessionLogParam param)
        {
            SessionLogResult ret = null;
            
            string sql = query.QueryForGet(null);

            ret = await Context
                .ExecuteQueryFirstAsync<SessionLogResult>(sql, param); 
                 
            return ret;
        }

        public async Task Update(SessionLogEntry model)
        {
            
            string sql = query.QueryForUpdate(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
            
        }

        public async Task Delete(SessionLogEntry model)
        {
            
            string sql = query.QueryForDelete(TableName, model, model);
            await Context.ExecuteAsync(sql, model);
 
        }

        public async Task<List<SessionLogList>> ReadList(SessionLogParam param)
        {
            List<SessionLogList> ret = null;

            ret = await Context
                .ExecuteQueryToListAsync<SessionLogList>(query.QueryForList(param),param); 

            return ret;
        }
             
        // retorna uma listagem com controle de paginação 
        public async Task<PagedList<SessionLogResult>> ReadSearch(SessionLogParam param)
        {
            PagedList<SessionLogResult> ret  = new PagedList<SessionLogResult>();   
            List<SessionLogResult> recordlist = null;
            List<PaginationModel> paglist = null;
            int index = 1; 

            // pegar a list de Seq da busca solicitada pelo cliente
            paglist = await Context
            .ExecuteQueryToListAsync<PaginationModel>(query.QueryForPaginationSettings(param), param);

            if (paglist.Count > 0)
            {
                // montar o objeto com as configurações da paginação: lista com os seq start e seq end para cada pagina
                PaginationSettings paginationSettings
                    = query.GetPaginationSettings(paglist, 
                    BaseParam.CalcPageCount(param.RecordsPerPage, paglist.Count), param.RecordsPerPage);
                
                // por padrão, se é uma nova pesquisa (PageIndex), sempre retornar a primeira pagina
                // se passar um indice, retorna a pagina correspondente
                if(param.PageIndex > 0)
                {
                    index = param.PageIndex; 
                }
                
                // fazer a busca de retorno correspondente à pagina solicitada
                param.Pagination = paginationSettings.GetItem(index);
                recordlist = await Context
                .ExecuteQueryToListAsync<SessionLogResult>(query.QueryForSearch(param), param);

                // configurando o Paged do retorno

                ret.PageCount = paginationSettings.PageCount; 
                ret.TotalRecords = paglist.Count;
                ret.RecordList = recordlist; 
                
            }
          
            return ret;
        }

        public async Task<ExecutionStatus> SetDateLogout(SessionLogParam obj)
        {           
            string sql = query.QueryForSetDateLogout();
            await Context.ExecuteAsync(sql, obj);
          
            return Context.Status;
        }

    }

}
