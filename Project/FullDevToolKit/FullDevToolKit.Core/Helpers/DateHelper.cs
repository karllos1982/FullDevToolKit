﻿using System;
using FullDevToolKit.Common;

namespace FullDevToolKit.Helpers
{
    public static class DateHelper
    {

        public static bool IsDate(string value)
        {
            bool ret = false;

            try
            {
                Convert.ToDateTime(value);
                ret = true;
            }
            catch
            {

            }

            return ret;
        }

        public static string ToShortDateString(string datetext, string separator)
        {
            string ret = datetext;

            if (IsDate(datetext))
            {
                DateTime dt = Convert.ToDateTime(datetext);
                ret = dt.Year.ToString() + separator + dt.Month.ToString().PadLeft(2, '0') +
                    separator + dt.Day.ToString().PadLeft(2, '0');

            }

            return ret;

        }

        public static string ToShortDateTimeString(string datetext, string separator, string time = "")
        {
            string ret = datetext;
            string timetext = "";

            if (IsDate(datetext))
            {
                DateTime dt = Convert.ToDateTime(datetext);

                if (time != "")
                {
                    timetext = time;
                }
                else
                {
                    timetext = dt.Hour.ToString().PadLeft(2, '0') + ":" +
                               dt.Minute.ToString().PadLeft(2, '0') + ":" +
                               dt.Second.ToString().PadLeft(2, '0');
                }
                ret = dt.Year.ToString() + separator + dt.Month.ToString().PadLeft(2, '0') +
                    separator + dt.Day.ToString().PadLeft(2, '0') + " " + timetext;

            }

            return ret;

        }

        public static string ToDateStringBR(string datetext, string separator)
        {
            string ret = datetext;

            if (IsDate(datetext))
            {
                DateTime dt = Convert.ToDateTime(datetext);
                ret = dt.Day.ToString().PadLeft(2, '0') + separator
                    + dt.Month.ToString().PadLeft(2, '0') +
                    separator + dt.Year.ToString().PadLeft(2, '0');
            }

            return ret;

        }

        public static string ToDateTimeStringBR(string datetext, string separator)
        {
            string ret = datetext;

            if (IsDate(datetext))
            {
                DateTime dt = Convert.ToDateTime(datetext);
                ret = dt.Day.ToString().PadLeft(2, '0') 
                    + separator
                    + dt.Month.ToString().PadLeft(2, '0') 
                    + separator 
                    + dt.Year.ToString().PadLeft(2, '0') + " "
                    + dt.Hour.ToString().PadLeft(2, '0') + ":" 
                    + dt.Minute.ToString().PadLeft(2, '0') + ":" 
                    + dt.Second.ToString().PadLeft(2, '0');
            }

            return ret;


        }

        public static string AddMinute(string hour, int value)
        {
            string ret = "";
            string[] aux = hour.Split(':');

            int H = 0;
            int M = 0;

            if (aux.Length == 2)
            {
                H = int.Parse(aux[0]);
                M = int.Parse(aux[1]);
            }

            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1, H, M, 0);
            DateTime dt2 = dt.AddMinutes(value );

            ret = dt2.Hour.ToString().PadLeft(2, '0') + ":"
                + dt2.Minute.ToString().PadLeft(2, '0');

            return ret;
        }

        public static Int32 DiferenceMinutes(string hour1, string hour2)
        {
            Int32 ret = 0;

            DateTime dt = DateTime.Parse("2018-01-01 " + hour1);
            DateTime dt2 = DateTime.Parse("2018-01-01 " + hour2);
            TimeSpan sp = dt2.Subtract(dt);

            ret = sp.Minutes;

            return ret;
        }

        public static int DayOfWeek(string day_name)
        {
            int ret = 0;
            string[] aux = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            foreach (string s in aux)
            {
                ret = ret + 1;
                if (s == day_name)
                {
                    break;
                }
            }

            return ret;
        }

        public static string DayName(DateTime date, string lang="")
        {
            string ret = "";
           
            switch (date.DayOfWeek)
            {
                case System.DayOfWeek.Sunday:
                    ret = LocalizationText.Get("ShortDayName-1", lang).Text;
                    break;

                case System.DayOfWeek.Monday:
                    ret = LocalizationText.Get("ShortDayName-2", lang).Text;
                    break;

                case System.DayOfWeek.Tuesday:
                    ret = LocalizationText.Get("ShortDayName-3", lang).Text;
                    break;

                case System.DayOfWeek.Wednesday:
                    ret = LocalizationText.Get("ShortDayName-4", lang).Text;
                    break;

                case System.DayOfWeek.Thursday:
                    ret = LocalizationText.Get("ShortDayName-5", lang).Text;
                    break;

                case System.DayOfWeek.Friday:
                    ret = LocalizationText.Get("ShortDayName-6", lang).Text;
                    break;

                case System.DayOfWeek.Saturday:
                    ret = LocalizationText.Get("ShortDayName-7", lang).Text;
                    break;

            }

            return ret;
        }

        public static string MonthName(int month)
        {
            string ret = "";

            List<string> lista = new List<string>();

            lista.Add(LocalizationText.Get("ShortDayName-1","eng").Text);
            lista.Add(LocalizationText.Get("ShortDayName-2", "eng").Text);
            lista.Add(LocalizationText.Get("ShortDayName-2", "eng").Text);
            lista.Add(LocalizationText.Get("ShortDayName-4", "eng").Text);
            lista.Add(LocalizationText.Get("ShortDayName-5", "eng").Text);
            lista.Add(LocalizationText.Get("ShortDayName-6", "eng").Text);
            lista.Add(LocalizationText.Get("ShortDayName-7", "eng").Text);
            lista.Add(LocalizationText.Get("ShortDayName-8", "eng").Text);
            lista.Add(LocalizationText.Get("ShortDayName-9", "eng").Text);
            lista.Add(LocalizationText.Get("ShortDayName-10", "eng").Text);
            lista.Add(LocalizationText.Get("ShortDayName-11", "eng").Text);
            lista.Add(LocalizationText.Get("ShortDayName-12", "eng").Text);

            ret = lista[month-1]; 
            
            return ret;
        }

    }

}
