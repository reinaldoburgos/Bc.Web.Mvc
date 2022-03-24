using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class TableExtensions
    {
        internal static void BcBeginTable(this System.Web.Mvc.HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes, 
            bool columnAutowidth =  false)
        {
            TagBuilder builder = new TagBuilder("table");
            if (htmlAttributes != null)
            {
                foreach (var attr in htmlAttributes)
                {
                    builder.Attributes.Add(attr.Key, Convert.ToString(attr.Value));
                }
            }
            string result = builder.ToString();
            result = result.Remove(result.IndexOf("</table>"));
            htmlHelper.ViewContext.Writer.Write(result);
        }

        internal static void BcEndTable(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("</table>");
        }

        public static MvcContent BcBeginTable(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            var resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.ElementClass.Table },
                htmlAttributes);

            return new MvcContent(
                () => htmlHelper.BcBeginTable(htmlAttributes: resulthtmlAttributes),
                () => htmlHelper.BcEndTable()
            );
        }        

        public static MvcContent BcBeginDataTable(this HtmlHelper htmlHelper, object htmlAttributes = null, 
            bool columnAutoWidth = true, bool allowSearch = true, bool allowSort = true, int sortColumn = 1, bool sortDesc = false, bool scrollX = false, string scrollY = null,
            bool allowPagination = true, int pageSize = 20, int leftColumns = 0, int rightColumns = 0, bool fixedHeader = false, bool applyLanguage = true, bool autoInit = true,
            bool autoWindowHeight = false)
        {
            var resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.ElementClass.Table + " " + Constants.Style.GeneralClass.DataTableClass },
                htmlAttributes);


            resulthtmlAttributes.Add("autoWidth", columnAutoWidth.ToString().ToLower());

            if(scrollX)
            {
                resulthtmlAttributes.Add("scrollX", "100%");
                //resulthtmlAttributes.Add("sScrollXInner", "110%");  
            }

            resulthtmlAttributes.Add("autoInit", autoInit.ToString().ToLower());

            if(!string.IsNullOrEmpty(scrollY))
                resulthtmlAttributes.Add("scrollY", scrollY);

            resulthtmlAttributes.Add("bPaginate", allowPagination.ToString().ToLower());
            
            resulthtmlAttributes.Add("iDisplayLength", pageSize);
              
            resulthtmlAttributes.Add("bFilter", allowSearch.ToString().ToLower());                                  
            resulthtmlAttributes.Add("bSort", allowSort.ToString().ToLower());
            
            //deprecated
            resulthtmlAttributes.Add("functionsPageTop", false);

            resulthtmlAttributes.Add("sortColumn", sortColumn);
            resulthtmlAttributes.Add("autoWindowHeight", autoWindowHeight.ToString().ToLower());

            resulthtmlAttributes.Add("applyLanguage", applyLanguage);
            
            if (sortDesc)
                resulthtmlAttributes.Add("sortDesc", "desc");
            else
                resulthtmlAttributes.Add("sortDesc", "asc");

            if (fixedHeader)
                resulthtmlAttributes.Add("fixedHeader", fixedHeader);

            if (leftColumns > 0)
                resulthtmlAttributes.Add("leftColumns", leftColumns);

            if (rightColumns > 0)
                resulthtmlAttributes.Add("rightColumns", rightColumns);

            //if (scrollX || leftColumns > 0 || rightColumns > 0)
            //    resulthtmlAttributes.Add("scrollCollapse", true);

            return new MvcContent(
                () => htmlHelper.BcBeginTable(htmlAttributes: resulthtmlAttributes),
                () => htmlHelper.BcEndTable()
            );
        }
    }
}
