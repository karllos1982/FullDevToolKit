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

        public static int CalcPageCount(int recordperpage, int recordcount )
        {
            int ret = 0;

            int rest = 0;

            ret = Convert.ToInt32((recordcount / recordperpage));

            rest = recordcount % recordperpage;

            if (rest > 0)
            {
                ret++;
            }

            return ret;
        }
    }

    public class PaginationModel
    {
        public long Seq { get; set; }

    }

    public class PaginationSettingsItem
    {
        public int PageIndex { get; set; }

        public long StartSeq { get; set; }

        public long EndSeq { get; set; }
    }

    public class PaginationSettings
    {
        private List<PaginationSettingsItem> items = new List<PaginationSettingsItem>(); 

        private long _recordcount = 0;

        private int _pagecount = 0;

        public void SetPagination(long recordcount, int pagecount)
        {
            _recordcount = recordcount;
            _pagecount = pagecount;
        }

        public long RecordCount 
        {
            get
            {
                return _recordcount;    
            }
        }

        public int PageCount
        {
            get
            {
                return _pagecount;
            }
        }    
        
        public void AddItem(int index, long start, long end)
        {
            items.Add(new PaginationSettingsItem()
            {
                PageIndex = index,
                StartSeq = start,
                EndSeq = end   
            });
        }

        public PaginationSettingsItem GetItem(int index)
        {
            PaginationSettingsItem ret = null;

            if (items.IndexOf(items[index-1]) != -1)
            {
                ret = items[index-1]; 
            }

            return ret;
        }

    }

    public class PagedList<T>
    {
        public long TotalRecords { get; set; } = 0;

        public int PageCount { get; set; } = 0;

        public List<T> RecordList { get; set; } = null;

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
