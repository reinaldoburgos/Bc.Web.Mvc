using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bc.Web.Mvc.Html
{
    public static class DisplayExtensions
    {
        public static MvcHtmlString BcDisplayFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, object htmlAttributes = null, string format = null)
        {
            //var editorAttr = 
            //    Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(new { @class = "control-label" }, htmlAttributes);

            string editor = htmlHelper.BcTextBoxFor(expression, htmlAttributes: htmlAttributes, format: format, readOnly: true).ToString();            

            return MvcHtmlString.Create(editor.ToString());
        }
    }
}
