using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bc.Web.Mvc
{
    public static class Constants
    {
        public static class Formats
        {
            public static string DateTimeFormat
            {
                get
                {
                    return "dd/MM/yyyy HH:mm";
                }
            }            

            public static string DateFormat
            {
                get
                {
                    return "dd/MM/yyyy";
                }
            }

            public static string TimeFormat
            {
                get
                {
                    return "HH:mm";
                }
            }
        }
    }

    public static class FormatExtensions
    {        
        public static string ToDefaultDateTimeFormat(this DateTime dateTime)
        {            
            return dateTime.ToString(Constants.Formats.DateTimeFormat);            
        }

        public static string ToDefaultDateFormat(this DateTime dateTime)
        {            
            return dateTime.ToString(Constants.Formats.DateFormat);         
        }

        public static string ToDefaultTimeFormat(this DateTime dateTime)
        {
            return dateTime.ToString(Constants.Formats.TimeFormat);
        }

        public static string ToDefaultDayMonthFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM");
        }

        public static string ToDefaultDateTimeFormat(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
                return dateTime.Value.ToDefaultDateTimeFormat();
            else
                return "";
        }

        public static string ToDefaultDateFormat(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
                return dateTime.Value.ToDefaultDateTimeFormat();
            else
                return "";
        }

        public static string ToDefaultTimeFormat(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
                return dateTime.Value.ToDefaultTimeFormat();
            else
                return "";
        }
    }
}
