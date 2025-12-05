using Newtonsoft.Json;
using FullDevToolKit.Core;
using FullDevToolKit.Sys.Models.Common;

namespace FullDevToolKit.Common
{

    public class LocalizationTextItem
    {
        public string LanguageID { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string Code { get; set; }

    }

    public class LocalizationText
    {

        private static List<LocalizationTextItem> _items = new List<LocalizationTextItem>();

        private static void LoadLocalizationENG(ref List<LocalizationTextItem> items)
        {
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "1", Name = "Execution-Error", Text = "Execution error:" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "2", Name = "Validation-Error", Text = "Data validation error." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "3", Name = "Record-NotFound", Text = "The requested record was not found." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "4", Name = "Login-Invalid-Password", Text = "Invalid password. You still have {0} attempts before the account is deactivated." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "5", Name = "Login-Attempts", Text = "You have already used access attempts and the account has been disabled. Request activation." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "6", Name = "Login-Inactive-Account", Text = "The account associated with the User is not active. Request account activation." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "7", Name = "Login-Locked-Account", Text = "The account associated with the User is locked out. Contact your system administrator." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "8", Name = "Login-User-NotFound", Text = "User not found." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "9", Name = "User-Exists", Text = "There is already a user with the email:" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "10", Name = "Email-Exists", Text = "The email you entered already exists." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "11", Name = "Profile-NotBe-Null", Text = "The Profile cannot be null." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "12", Name = "User-Error-Exclude-Childs", Text = "There was an error deleting child items (Roles)." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "13", Name = "User-Invalid-Password-Code", Text = "The password exchange authorization code is invalid." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "14", Name = "Account-Active", Text = "The account associated with the User is already active." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "15", Name = "User-Invalid-Activation-Code", Text = "The activation authorization code is invalid." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "16", Name = "User-No-Image", Text = "Send the image file." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "17", Name = "User-Role-Exists", Text = "This Role is already associated with the user." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "18", Name = "User-Role-No-Exists", Text = "This Role does not belong to the user." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "19", Name = "Http-Unauthorized", Text = "Unauthorized access" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "20", Name = "Http-NotFound", Text = "The resource could not be found" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "21", Name = "Http-Forbidden", Text = "User profile without access permission." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "22", Name = "Http-500Error", Text = "An error occurred in the processing of the request." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "23", Name = "Http-ServiceUnavailable", Text = "The requested service is unavailable." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "24", Name = "API-Unexpected-Exception", Text = "Unexpected error not identified:: GetExceptionMessages@f2]" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "25", Name = "ShortDayName-1", Text = "Sun" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "26", Name = "ShortDayName-2", Text = "Mon" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "27", Name = "ShortDayName-3", Text = "Tue" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "28", Name = "ShortDayName-4", Text = "Wed" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "29", Name = "ShortDayName-5", Text = "Thu" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "30", Name = "ShortDayName-6", Text = "Fri" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "31", Name = "ShortDayName-7", Text = "Sat" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "32", Name = "MonthName-1", Text = "JANUARY" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "33", Name = "MonthName-2", Text = "FEBRUARY" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "34", Name = "MonthName-3", Text = "MARCH" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "35", Name = "MonthName-4", Text = "APRIL" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "36", Name = "MonthName-5", Text = "MAY" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "37", Name = "MonthName-6", Text = "JUNE" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "38", Name = "MonthName-7", Text = "JULY" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "39", Name = "MonthName-8", Text = "AUGUST" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "40", Name = "MonthName-9", Text = "SEPTEMBER" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "41", Name = "MonthName-10", Text = "OCTOBER" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "42", Name = "MonthName-11", Text = "NOVEMBER" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "43", Name = "MonthName-12", Text = "DECEMBER" });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "44", Name = "Validation-NotNull", Text = "cannot be null." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "45", Name = "Validation-Max-Characters", Text = "The {0} field cannot have more than {1} characters." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "46", Name = "Validation-Invalid-Field", Text = "The {0} field is invalid." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "47", Name = "Validation-Invalid-UserName", Text = "The {0} field is invalid. Do not use special characters or spaces." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "48", Name = "Validation-Unique-Value", Text = "The {0} field is invalid. Value must be unique." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "49", Name = "User-Instance-Exists", Text = "This Instance is already associated with the user." });
            items.Add(new LocalizationTextItem() { LanguageID = "1", Code = "50", Name = "User-Instance-No-Exists", Text = "This Instance does not belong to the user." });
        }

        public static LocalizationTextItem GetItemOld(string codeorname, string language)
        {
            LocalizationTextItem ret = null;
            string content = null;

            if (_items == null)
            {
                _items = new List<LocalizationTextItem>();

                try
                {
                    string jsonfile = "localization.json";

                    string filename = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    filename = filename.Replace("\\GW.Framework.dll", "");
                    filename = Path.Combine(filename, jsonfile);
                    if (File.Exists(filename))
                    {
                        content = File.ReadAllText(filename);

                        if (content != null)
                        {
                            _items = JsonConvert.DeserializeObject<List<LocalizationTextItem>>(content);
                        }
                    }

                }
                catch (Exception ex)
                {

                }

            }

            if (_items != null)
            {
                if (_items.Count == 0)
                {
                    _items = new List<LocalizationTextItem>();
                    LoadLocalizationENG(ref _items);
                }

                ret = _items.Where(l => l.Code == codeorname && l.LanguageID == language).FirstOrDefault();

                if (ret == null)
                {
                    ret = _items.Where(l => l.Name == codeorname && l.LanguageID == language).FirstOrDefault();
                }
            }

            if (ret == null)
            {
                ret = new LocalizationTextItem() { Code = "0", Name = "RootError", Text = "Error" };
            }

            return ret;
        }

        public async static Task LoadDataSync(IContext context, bool enforceupdate = false)
        {
            if (enforceupdate)
            {
                _items = await context.GetLocalizationTextsAsync();
            }
            else
            {
                if (_items == null)
                {
                    _items = await context.GetLocalizationTextsAsync();
                }
            }

        }

        public static void LoadData(IContext context, bool enforceupdate = false)
        {

            if (enforceupdate)
            {
                _items = context.GetLocalizationTextsAsync().Result;
            }
            else
            {
                if (_items == null)
                {
                    _items = context.GetLocalizationTextsAsync().Result;
                }
            }

        }

        public static LocalizationTextItem Get(string codeorname, string language)
        {
            LocalizationTextItem ret = null;


            if (_items != null)
            {
                if (_items.Count == 0)
                {
                    _items = new List<LocalizationTextItem>();
                    LoadLocalizationENG(ref _items);
                }

                ret = _items.Where(l => l.Code == codeorname && l.LanguageID == language)
                        .FirstOrDefault();

                if (ret == null)
                {
                    ret = _items.Where(l => l.Name == codeorname && l.LanguageID == language)
                        .FirstOrDefault();
                }

                if (ret == null)
                {
                    ret = _items.Where(l => l.Code == codeorname)
                        .FirstOrDefault();
                }

                if (ret == null)
                {
                    ret = _items.Where(l => l.Name == codeorname)
                        .FirstOrDefault();
                }

            }

            if (ret == null)
            {
                ret = new LocalizationTextItem() { Code = "0", Name = "RootError", Text = "Error" };
            }

            return ret;
        }

    }


    public class BaseLocalization 
    {

        private Dictionary<string, string> _texts;

        public void Set(List<LocalizationTextResult> texts, string lang)
        {
            _texts = new Dictionary<string, string>();

            foreach (LocalizationTextResult r in texts)
            {
                if (r.LanguageID.ToString() == lang)
                {

                    if (!_texts.ContainsKey(r.Name))
                    {
                        _texts.Add(r.Name, r.Text);
                    }
                }
            }
        }

        public string Get(string name)
        {
            string ret = name;

            if (_texts != null)
            {
                if (_texts.ContainsKey(name))
                {
                    ret = _texts[name];
                }
            }

            return ret;
        }


    }

}
