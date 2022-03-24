using Bc.Data.DbConnection;
using Bc.Web.Mvc.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Bc.Web.Mvc;
using Bc.Web.Proxies;

namespace Bc.Web.Mvc.Html
{
    public static class MultiSelectGridExtensions
    {
        public static MvcHtmlString BcMultiSelectGridGeneralValues(this System.Web.Mvc.HtmlHelper htmlHelper, string name, short id, string label = null,
            IEnumerable<object> selectedValues = null, object htmlAttributes = null)
        {
            GeneralProxy proxy = new GeneralProxy();
            var gvalues = proxy.GetGeneralValuesById(id);
            
            //if(string.IsNullOrEmpty(label)) label = generalTable.Name;
            return htmlHelper.BcMultiSelectGrid(name, label, gvalues.ToMultiSelectList("Code","Content"), selectedValues, htmlAttributes);
        }

        public static MvcHtmlString BcMultiSelectGrid(this System.Web.Mvc.HtmlHelper htmlHelper, string name, string label,
            IEnumerable<SelectListItem> selectList, IEnumerable<object> selectedValues = null, object htmlAttributes = null)
        {
            var resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.ElementClass.Table },
                htmlAttributes);

            TagBuilder builder = new TagBuilder("table");
            if (resulthtmlAttributes != null)
            {
                foreach (var attr in resulthtmlAttributes)
                {
                    builder.Attributes.Add(attr.Key, Convert.ToString(attr.Value));
                }
            }

            string beginTable = builder.ToString();
            beginTable = beginTable.Remove(beginTable.IndexOf("</table>"));

            StringBuilder rows = new StringBuilder();
            rows.AppendFormat("<thead><tr><th>{0}</th><th>{1}</th></tr></thead><tbody>"," ",label);

            foreach (var item in selectList)
            {                
                IDictionary<string, object> editorAttr = null;

                bool isChecked = (selectedValues != null && selectedValues.Contains(item.Value)) || (selectedValues == null && item.Selected);
                
                editorAttr = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                
                rows.AppendFormat("<tr class=\"text-center\" style=\"max-width:80px\"><td>{0}</td><td>{1}</td></tr>", 
                    htmlHelper.BcCheckBox(name, isChecked, item.Value, editorAttr), item.Text);
                
            }
            rows.Append("</tbody></table>");


            return MvcHtmlString.Create(beginTable + rows.ToString());
        }
    }
}
