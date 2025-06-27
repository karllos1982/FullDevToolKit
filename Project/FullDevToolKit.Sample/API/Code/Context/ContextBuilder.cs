using Microsoft.Data.SqlClient;
using FullDevToolKit.Core;

namespace MyApp.Context
{
    public class ContextBuilder : IContextBuilder
    {
        public ISettings Settings { get; set; }

        public ContextBuilder(ISettings settings)
        {
            Settings = settings;
        }   

        public void BuilderContext(IContext context)
        {                            
         
            SqlConnection conn = new SqlConnection(Settings.Sources[0].SourceValue);

            ((DapperContext)context).Connection= conn;  

            context.Begin(0, System.Data.IsolationLevel.ReadUncommitted);

        }
    }
}
