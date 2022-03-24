using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public static class LabelExtensions
    {
        public static MvcHtmlString BcLabelFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
                                                                Expression<Func<TModel, TValue>> expression,
                                                                object htmlAttributes = null,
                                                                string labelText = null)
        {
            //var attributes = new RouteValueDictionary(htmlAttributes);

            //if (htmlAttributes == null || !attributes.ContainsKey("class"))
            //{
            //    attributes.Add("class", Constants.Style.ElementClass.LabelClass
            //        + (labelTextUp ? "" : " " + Constants.Style.ColumnSizeClass.Size_3));
            //}

            //return htmlHelper.LabelFor(expression, labelText: labelText, htmlAttributes: attributes);

          //  var attributes = new RouteValueDictionary(htmlAttributes);
          //  attributes.Add("class", Constants.Style.ElementClass.LabelClass);

           var labelAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(new { @class = "control-label" }, htmlAttributes);

            return htmlHelper.LabelFor(expression, labelText: labelText, htmlAttributes: labelAttr);

        }

        public static MvcHtmlString BcLabel(this HtmlHelper htmlHelper,
                                            object htmlAttributes = null,
                                            string labelText = null,
                                            bool labelTextUp = false)
        {
            //var attributes = new RouteValueDictionary(htmlAttributes);

            //if (htmlAttributes == null || !attributes.ContainsKey("class"))
            //{
            //    attributes.Add("class", Constants.Style.ElementClass.LabelClass 
            //        + (labelTextUp ? "" : " " + Constants.Style.ColumnSizeClass.Size_3));
            //}

            //return htmlHelper.Label("", labelText: labelText, htmlAttributes: attributes);

            var labelAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(new { @class = "control-label" }, htmlAttributes);

            return htmlHelper.Label("", labelText: labelText, htmlAttributes: labelAttr);
        }
    }
}