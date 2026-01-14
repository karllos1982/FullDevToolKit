using FullDevToolKit.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace FullDevToolKit.Core.Common
{

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

            if (items.IndexOf(items[index - 1]) != -1)
            {
                ret = items[index - 1];
            }

            return ret;
        }

    }

    public class PagedList<T>
    {
        public long TotalRecords { get; set; } = 0;

        public long RecordCount { get; set; } = 0;

        public int PageCount { get; set; } = 0;

        public List<T> RecordList { get; set; } = null;

    }

    public static class PaginationFeatures
    {

        public static int CalcPageCount(int recordperpage, int recordcount)
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

        public static PaginationSettings BuildPaginationSettings(QueryBuilder queryBuilder,
            ref BaseParam param, List<PaginationModel> paglist)
        {
            PaginationSettings paginationSettings
                = queryBuilder.GetPaginationSettings( paglist,
                PaginationFeatures.CalcPageCount(param.RecordsPerPage, paglist.Count), param.RecordsPerPage);

            int index = 1;

            if (param.PageIndex > 0)
            {
                index = param.PageIndex;
            }

            param.Pagination = paginationSettings.GetItem(index);

            return paginationSettings;  

        }

        public static void SetPaginationCounts(ref PagedList<object> obj, int pagecount,
            int totalrecords, List<object> list )
        {
            obj.PageCount = pagecount;
            obj.TotalRecords = totalrecords;
            obj.RecordList = list;
            obj.RecordCount = list.Count;
        }

    }


}
