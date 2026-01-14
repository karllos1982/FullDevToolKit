using FullDevToolKit.Sys.Models.Common;
using System.Linq;
using System.Reflection;

namespace FullDevToolKit.Core.Common
{

    public abstract class BaseParam
    {
        public int RecordsPerPage { get; set; } = 20;

        public int PageIndex { get; set; } = 0;

        public PaginationSettingsItem? Pagination { get; set; }
    
    }


    public abstract class BaseModel
    {
        public long Seq { get; set; }

        public DateTime TSCreate { get; set; }

        public DateTime TSLastUpdate { get; set; }        

        public static void ConvertTo(BaseModel inobj, BaseModel outobj)
        {

            if (inobj != null)
            {
                PropertyInfo[] props = inobj.GetType().GetProperties();

                foreach (PropertyInfo pi in props)
                {
                    if (!pi.ToString().Contains("System.Collections.Generic.List"))
                    {
                        object val = pi.GetValue(inobj);

                        PropertyInfo p_out = outobj.GetType().GetProperty(pi.Name);
                        if (p_out != null)
                        {
                            p_out.SetValue(outobj, val);
                        }
                    }
                    
                }

            }
        }

        
    }
}
