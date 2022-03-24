using Bc.Web.Mvc.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public static class ContentBoxExtensions
    {
        internal static void BeginContentBox(this System.Web.Mvc.HtmlHelper htmlHelper,
           string title = null, Icons? icon = null, bool paddingContent = false, TabCollection tabs = null, object htmlAttributes = null)
        {
            BeginContentBox(htmlHelper, title, icon!=null?icon.GetID():null, paddingContent, tabs, htmlAttributes: htmlAttributes);
        }

        internal static void BeginContentBox(this System.Web.Mvc.HtmlHelper htmlHelper,
            string title = null, string iconClass = null, bool paddingContent = false, TabCollection tabs = null, object htmlAttributes = null)
        {
            string iconHtml = string.Empty;
            bool useTab = tabs != null && tabs.Count() > 0;

            if (iconClass != null)
            {
                iconHtml = string.Format("<span class=\"{0} {2}\">" +
                            "<i class=\"{1}\"></i>" +
                            "</span> ", Constants.Style.GeneralClass.IconClass, iconClass, (useTab ? "pull-right" : ""));
            }

            string labelHtml = string.Empty;           

            //    "<span class=\"label label-danger\">48 notices</span>";
            StringBuilder tabBuilder = new StringBuilder();
            if (tabs != null)
            {
                tabBuilder.Append("<ul class=\"nav nav-tabs\">");
                foreach (TabPane tab in tabs)
                {
                    tabBuilder.Append(string.Format("<li {2}><a {3} data-toggle=\"tab\" href=\"{0}\">{1}</a></li>",
                        tab.ResultTarget, tab.Name, tab.Selected ? "class=\"active\"" : "",
                        (tab.TargetType == TabTarget.Url ? "targettype=\"url\"" : "targettype=\"content\"")));
                }
                tabBuilder.Append("</ul>");
            }

            string html = string.Empty;
            if(useTab)
            {
                html = string.Format("<div widget-box class=\"{0}\">" +
                        "{1} <div class=\"{2}\">",
                        Constants.Style.WidgetClass.TabContentClass,
                        tabBuilder.ToString(),
                        "tab-content"
                    );
            }
            else
            {
                string titleBlock = string.Empty;
                if(!string.IsNullOrWhiteSpace(title))
                {
                    titleBlock = string.Format("<div class=\"{0}\" style=\"margin-bottom:20px\"><h5>{2}{1}</h5></div>", 
                        Constants.Style.WidgetClass.TitleClass,
                        title.ToUpper(),
                        iconHtml);
                }

                RouteValueDictionary dictionary = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                if(dictionary.ContainsKey("class"))
                {
                    dictionary["class"] = dictionary["class"] + Constants.Style.WidgetClass.BoxClass;
                }
                else
                {
                    dictionary["class"] = Constants.Style.WidgetClass.BoxClass;
                }
         
                StringBuilder builderAttrs = new StringBuilder();
                foreach(var attr in dictionary)
                {                    
                    builderAttrs.AppendFormat(" {0}=\"{1}\"",attr.Key, attr.Value);                    
                }

                html = string.Format("<div widget-box>{0}" +
                        "<div {1}>", 
                        titleBlock,
                        builderAttrs.ToString());
            }

            htmlHelper.ViewContext.Writer.Write(html);


            //htmlHelper.ViewContext.Writer.Write(
            //    string.Format("<div class=\"{0}\">" +
            //                  "<div class=\"{1}\">" +
            //                   "{2}" +
            //                        "<h5 {9}>{3}</h5> {8}" +
            //                        "{4} {10}" +
            //                        "</div>" +
            //                        "<div class=\"{5} {6} {7}\">",
            //                        Constants.Style.WidgetClass.BoxClass,                       //0
            //                        Constants.Style.WidgetClass.TitleClass,                     //1
            //                        iconHtml,                                                   //2
            //                        title,                                                      //3
            //                        labelHtml,                                                  //4
            //                        Constants.Style.WidgetClass.ContentClass,                   //5
            //                        (useTab ? Constants.Style.WidgetClass.TabContentClass : ""),//6
            //                        (!paddingContent ? Constants.Style.GeneralClass.NoPaddingClass : ""),//7
            //                        tabBuilder.ToString(),                                               //8
            //                        (useTab ? "class = pull-right" : ""),               //9
            //                        sbLabels.ToString())                                //10
            //    );
        }

        internal static void EndContentBox(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("</div></div>");
        }

        public static MvcContent BcBeginContentBox(this System.Web.Mvc.HtmlHelper htmlHelper,
            string title, string iconClass, bool paddingContent = false, IEnumerable<Label> labels = null, object htmlAttributes = null)
        {
            return new MvcContent(
                () => htmlHelper.BeginContentBox(title, iconClass, paddingContent, htmlAttributes: htmlAttributes),
                () => htmlHelper.EndContentBox()
            );
        }

        public static MvcContent BcBeginContentBox(this System.Web.Mvc.HtmlHelper htmlHelper,
            string title = null, Icons? icon = null, bool paddingContent = false, IEnumerable<Label> labels = null, object htmlAttributes = null)
        {
            return new MvcContent(
                () => htmlHelper.BeginContentBox(title, icon, paddingContent, htmlAttributes: htmlAttributes),
                () => htmlHelper.EndContentBox()
            );
        }

        public static MvcContent BcBeginTabContentBox(this System.Web.Mvc.HtmlHelper htmlHelper,
            string title, TabCollection tabs, string iconClass = null, bool paddingContent = false)
        {
            return new MvcContent(
                () => htmlHelper.BeginContentBox(title, iconClass, paddingContent, tabs),
                () => htmlHelper.EndContentBox()
            );
        }

    }
}
