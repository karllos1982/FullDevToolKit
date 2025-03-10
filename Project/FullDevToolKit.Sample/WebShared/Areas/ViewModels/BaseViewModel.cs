﻿using FullDevToolKit.Common;
using FullDevToolKit.Helpers;
using FullDevToolKit.Sys.Models.Common;
using FullDevToolKit.Sys.Models.Identity;
using MyApp.Proxys;


namespace MyApp.ViewModel
{

    public class TabItem
    {
        public TabItem(string index, string statelink, string statetab)
        {
            this.Index = index;
            this.StateLink = statelink;
            this.StateTab = statetab;
        }

        public string Index { get; set; }

        public string StateLink { get; set; }

        public string StateTab { get; set; }
    }

    public class TabItemCollection
    {

        public TabItemCollection()
        {
            Items = new List<TabItem>();
        }

        public List<TabItem> Items { get; set; }

        public void Add(string index, string statelink, string statetab)
        {
            Items.Add(new TabItem(index, statelink, statetab));
        }

        public void InactiveAll()
        {
            foreach (TabItem t in Items)
            {
                t.StateTab = "none";
                t.StateLink = "";
            }
        }

        public void AtiveTab(string index)
        {
            InactiveAll();

            TabItem t = Items.Where(t => t.Index == index).FirstOrDefault();
            t.StateTab = "block";
            t.StateLink = "active";
        }

        public string GetStatTabValue(string index)
        {
            return Items.Where(t => t.Index == index).FirstOrDefault().StateTab;
        }

        public string GetStatLinkValue(string index)
        {
            return Items.Where(t => t.Index == index).FirstOrDefault().StateLink;
        }
    }

    public abstract class SummaryManager
    {
        public List<ExceptionMessage> SummaryValidation = null;

        public string GetSummaryState(string text)
        {
            string ret = "none";

            if (text != null)
            {
                if (text != "")
                {
                    ret = "display";
                }
            }

            return ret;
        }

        public string GetSummaryMessage(string fieldkey)
        {
            string ret = "";

            foreach (ExceptionMessage ex in SummaryValidation)
            {
                if (ex.Key == fieldkey)
                {
                    ret = ex.Description;
                    break;

                }
            }

            return ret;
        }

        public void ShowSummaryValidation(List<ExceptionMessage> validations)
        {

            ClearSummaryValidation();

            if (validations != null)
            {
                foreach (ExceptionMessage ex in validations)
                {
                    ExceptionMessage s = SummaryValidation.Where(i => i.Key == ex.Key).FirstOrDefault();
                    if (s != null)
                    {
                        s.Description = ex.Description;
                    }
                }
            }
        }

        public abstract void ClearSummaryValidation();
        
    }


    public abstract class BaseViewModel
    {

        public void InitializeView( )
        {
            SearchingState = "block";
            EditingState = "none";
            ModoLabel = "";                        
            
        }

        public void InitializeView(UserAuthenticated user)                
        {
            SearchingState = "block";
            EditingState = "none";
            ModoLabel = "";                   

        }

        public ExecutionStatus ServiceStatus = null;

        public string SearchingState = "block";

        public string EditingState = "none";

        public string ModoLabel = "";

        public BaseLocalization texts = new BaseLocalization();

        public bool Inserting { get; set; }

        public PermissionsState Permissions { get; set; }

        public List<ExceptionMessage> SummaryValidation = null;

        public string GetSummaryState(string text)
        {
            string ret = "none";

            if (text != null)
            {
                if (text != "")
                {
                    ret = "display"; 
                }
            }

            return ret;
        }

        public string GetSummaryMessage(string fieldkey)
        {
            string ret = "";

            foreach (ExceptionMessage ex in SummaryValidation)
            {
                if (ex.Key == fieldkey)
                {
                    ret = ex.Description;
                    break; 
                    
                }
            }

            return ret;
        }

        public void ShowSummaryValidation(List<ExceptionMessage> validations)
        {

              ClearSummaryValidation();

            if (validations != null)
            {
                foreach (ExceptionMessage ex in validations)
                {
                    ExceptionMessage s = SummaryValidation.Where(i => i.Key == ex.Key).FirstOrDefault();
                    if (s != null)
                    {
                        s.Description = ex.Description;
                    }
                }
            }
        }

        protected void BaseBack()
        {
            this.SearchingState = "block";
            this.EditingState = "none";
            this.Inserting = false; 
        }

        protected void BaseInitNew()
        {
            ClearSummaryValidation();
            this.EditingState = "block";
            this.SearchingState = "none";
            ModoLabel = "Inserindo";
            this.Inserting = true;
            ClearSummaryValidation();
        }

        protected void BaseInitEdit()
        {
            this.EditingState = "block";
            this.SearchingState = "none";
            ModoLabel = "Editando";
            this.Inserting = false;
            ClearSummaryValidation();
        }

        // metodos abstratos

        public abstract Task InitializeModels();

        public abstract Task ClearSummaryValidation();

        public abstract void BackToSearch();

        public abstract void InitNew();

        public abstract void InitEdit();

        public abstract Task Search();

        public abstract Task Get(object id);

        public abstract Task Set();

        public void SetResult<T>(APIResponse<T> response,
           ref T data,  ref ExecutionStatus status)
        {
            
            if (response.IsSuccess)
            {
                data = response.Data;
            }
            else
            {
                ServiceStatus.Exceptions = response.Exceptions;
                ServiceStatus.Success = false;
                this.ShowSummaryValidation(response.Exceptions.Messages);
            }            
        }

		public void SetResult<T>(APIResponse<T> response,
		ref T data, ref ExecutionStatus status, SummaryManager summary )
		{

			if (response.IsSuccess)
			{
				data = response.Data;
			}
			else
			{
				ServiceStatus.Exceptions = response.Exceptions;
				ServiceStatus.Success = false;
				summary.ShowSummaryValidation(response.Exceptions.Messages);
			}
		}

		public async Task InitLocalization(DataCacheProxy cache, string lang)
        {
            this.texts = new DefaultLocalization();
            this.texts.Set(await cache.ListLocalizationTexts(), lang);
        }

        public PermissionsState CheckPermissions(UserAuthenticated user,
            string objectcode, bool allownone)
        {
            PermissionsState ret = new PermissionsState(false, false, false);

            List<UserPermissions> permissions = user.Permissions;          

            ret =
               Utilities.GetPermissionsState(permissions, objectcode, allownone);

            return ret;
        }

        public static PermissionsState SetPermissions(List<UserPermissions> list,
             string objectcode, bool allownone)
        {
            PermissionsState ret = new PermissionsState(false, false, false);

            ret =
               Utilities.GetPermissionsState(list, objectcode, allownone);

            return ret; 

        }



        public static string FormatDate(DateTime? value)
        {
            string ret = "";

            if (value != null)
            {
                ret = FullDevToolKit.Helpers.DateHelper.ToDateStringBR(value.ToString(), "/");
            }

            return ret;
        }

        public static DateTime ToDate(DateTime? value, string time)
        {
            DateTime ret = DateTime.Now;

            if (value != null)
            {
                string aux = FullDevToolKit.Helpers.DateHelper.ToShortDateString(value.ToString(), "-");
                
                ret = DateTime.Parse(aux + " " + time); 
            }

            return ret;
        }

        public static string FormatBoolean(bool value)
        {
            string ret = "NÃO";

            if (value)
            {
                ret = "SIM";
            }

            return ret;
        }

        public static string FormatCellPhoneNumber(string value)
        {
            string ret = value;
                        
            ret = Convert.ToInt64(value).ToString("(##)#####-####");

            return ret;
        }

        public static string FormatPhoneNumber(string value)
        {
            string ret = value;

            ret = Convert.ToInt64(value).ToString("(##)####-####");

            return ret;
        }

        // convert 0000 para: 00:00
        public static string FormatTime(string value)
        {
            string ret = value;

            if (value != null)
            {
                if (ret.IndexOf(":") == -1)
                {
                    ret = ret.Substring(0, 2) + ":" + ret.Substring(2, 2);
                }
            }
            
            return ret;
        }

    }
}
