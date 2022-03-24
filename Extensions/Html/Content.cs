using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public static class ContentExtensions
    {      
        internal static void BcBeginContent(this System.Web.Mvc.HtmlHelper htmlHelper, IDictionary<string,object> htmlAttributes)
        {
            TagBuilder builder = new TagBuilder("div");
            if (htmlAttributes != null)
            {
                foreach (var attr in htmlAttributes)
                {
                    builder.Attributes.Add(attr.Key, Convert.ToString(attr.Value));
                }
            }
            string result = builder.ToString();
            result = result.Remove(result.IndexOf("</div>"));
            htmlHelper.ViewContext.Writer.Write(result);
        }

        internal static void BcEndContent(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("</div>");
        }

        public static MvcContent BcBeginRow(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            var resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.ContentClass.Row },
                htmlAttributes);

            return new MvcContent(
                () => htmlHelper.BcBeginContent(htmlAttributes: resulthtmlAttributes),
                () => htmlHelper.BcEndContent()
            );
        }

        public static MvcContent BcBeginTabPane(this HtmlHelper htmlHelper, string id, bool active = false, object htmlAttributes = null)
        {
            var resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { id = id, @class = Constants.Style.ContentClass.TabPane + " " + (active?"active":"") },
                htmlAttributes);

            return new MvcContent(
                () => htmlHelper.BcBeginContent(htmlAttributes: resulthtmlAttributes),
                () => htmlHelper.BcEndContent()
            );
        }

        public static MvcContent BcBeginTabPane(this HtmlHelper htmlHelper, TabPane tab, object htmlAttributes = null)
        {
            return htmlHelper.BcBeginTabPane(tab.Key, tab.Selected, htmlAttributes);
        }


        public static MvcContent BcBeginColumn(this HtmlHelper htmlHelper, HtmlColumnSize size, bool paddingContent = true, object htmlAttributes = null)
        {
            var resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = BcHelper.GetColumnSizeClass(size) },
                htmlAttributes);

            if (size == HtmlColumnSize.Auto)
            {
                resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @style = "width:auto" },
                resulthtmlAttributes);
            }

            if (!paddingContent)
            {
                resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.GeneralClass.NoPaddingClass },
                resulthtmlAttributes);
            }

            return new MvcContent(
                () => htmlHelper.BcBeginContent(htmlAttributes: resulthtmlAttributes),
                () => htmlHelper.BcEndContent()
            );
        }



        
    }
}
