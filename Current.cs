using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Bc.Runtime
{
    public class Current
    {
        public static int UserId
        {
            get
            {
                if (HttpContext.Current.Session != null)
                    return Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                else
                    return 0;
            }
        }        

        public static int RoleId
        {
            get
            {
                if (HttpContext.Current.Session != null)
                    return Convert.ToInt32(HttpContext.Current.Session["RoleId"]);
                else
                    return 0;
            }
        }

        public static DateTime GetCurrentDateTime()
        {
            //todo: importante
            //if (Bc.Configuration.BcConfigurationSection.Current.IsDataProvider && Bc.Configuration.BcConfigurationSection.Current.UseDbDateTime)
            //{
            //    Bc.Data.DbConnection.DbContextManagerService db = new Data.DbConnection.DbContextManagerService();
            //    return db.GeneralServices.GetCurrentDateTime();
            //}
            //else
            //{
                TimeZoneInfo.GetSystemTimeZones();
                TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), nzTimeZone);
            //}
           
        }
    }
}
