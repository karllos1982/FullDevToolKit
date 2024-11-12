using FullDevToolKit.Common;

namespace FullDevToolKit.Core
{
    public interface IContext
    {
        ISettings Settings { get; set; }

        string LocalizationLanguage { get; set; }

        ExecutionStatus ConnStatus { get; set; }

        ExecutionStatus Status { get; set; }

        ExecutionStatus Begin(int connindex, object isolationlavel);

        ExecutionStatus End();

        void RegisterDataLog(string userid, OPERATIONLOGENUM operation,
          string tableaname, string objID, object olddata, object currentdata);

        Task RegisterDataLogAsync(string userid, OPERATIONLOGENUM operation,
             string tableaname, string objID, object olddata, object currentdata);

        Task<List<LocalizationTextItem>> GetLocalizationTextsAsync();

        Task<bool> CheckUniqueValueForInsert(string tablename, string fieldname, string fieldvalue);

        Task<bool> CheckUniqueValueForUpdate(string tablename, string fieldname,
                   string fieldvalue, string pkfieldname, string pkvalue); 

        void Dispose(); 

    }

}