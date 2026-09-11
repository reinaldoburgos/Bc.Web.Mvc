using System;
using System.Collections.Generic;
using System.Configuration;
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


        public static string URL_API
        {
            get
            {
                return "http://localhost:18459/";
                //return ConfigurationManager.AppSettings["URL_DEFAULT"];
            }
        }

        public class Url
        {
            public static string SignalR
            {
                get
                {
                    return URL_API + "signalr";
                }

            }
            public static string SignalRHub
            {
                get
                {
                    return URL_API + "signalr/hubs";
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
