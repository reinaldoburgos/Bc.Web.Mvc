using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Helper
{

    //public static class GeneralValuesEntity
    //{
    //    public static int Estado => 1;
    //}


    //public class GeneralValues
    //{
    //    public int Id { get; set; }
    //    public string Code { get; set; }
    //    public string Value { get; set; }
    //    public string AditionalValues { get; set; }
    //    public string Comment { get; set; }
    //    public int? OrderRows { get; set; }

    //    public static List<GeneralValues> GeneralValuesQuery(int id)
    //    {
    //        Data.DeceContext db = new Data.DeceContext();

    //        var sentencia = $"dbo.spGeneralValueQuery @Id = {id}";
    //        List<GeneralValues> generalValues = db.Database.SqlQuery<GeneralValues>(sentencia).ToList();
    //        return generalValues;
    //    }


    //    public static SelectList GetSelectLists(int id)
    //    {
    //        Data.DeceContext db = new Data.DeceContext();

    //        var sentencia = $"dbo.spGeneralValueQuery @Id = {id}";
    //        IEnumerable<GeneralValues> generalValues = db.Database.SqlQuery<GeneralValues>(sentencia).ToList();

    //        var selectList = new List<SelectListItem>();

    //        foreach (var item in generalValues)
    //        {
    //            selectList.Add(new SelectListItem
    //            {
    //                Value = item.Code.ToString(),
    //                Text = item.Value
    //            });
    //        }
    //        return new SelectList(selectList, "Value", "Text");

    //    }


    //}
}