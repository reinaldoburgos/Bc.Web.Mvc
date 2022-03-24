using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public static class PanelGroupExtensions
    {
        internal static void BeginPanelGroup(this System.Web.Mvc.HtmlHelper htmlHelper,
           string id, string title = null, Icons? icon = null,
           bool collapse = false,
           bool paddingContent = true,
           //bool autoGridViewFixedHeight = false,
           //string gridViewFixedHeightName = null,
           //int? marginFixedHeight = null,
           object htmlAttributes = null)
        {
            BeginPanelGroup(htmlHelper,id, title, icon != null ? icon.GetID() : null,
                collapse : collapse,
                paddingContent:paddingContent,
                //autoGridViewFixedHeight : autoGridViewFixedHeight,
                //gridViewFixedHeightName: gridViewFixedHeightName,
                //marginFixedHeight : marginFixedHeight,
                htmlAttributes: htmlAttributes);
        }

        internal static void BeginPanelGroup(this System.Web.Mvc.HtmlHelper htmlHelper,
            string id, string title = null, string iconClass = null,
            bool collapse = false,
            bool paddingContent = true,
            // bool autoGridViewFixedHeight = false,
            //string gridViewFixedHeightName = null,
            //int? marginFixedHeight = null,
            object htmlAttributes = null)
        {

            string iconHtml = string.Empty;

            if (iconClass != null)
            {
                iconHtml = string.Format("<span class=\"{0} {2}\">" +
                            "<i class=\"{1}\"></i>" +
                            "</span> ", Constants.Style.GeneralClass.IconClass, iconClass);
            }

            string labelHtml = string.Empty;
        
            string html = string.Empty;
            
            string titleBlock = string.Empty;
            if (!string.IsNullOrWhiteSpace(title))
            {
                titleBlock = string.Format("<h4 class=\"{0}\">{2}<a data-toggle = \"collapse\" href =\"#{3}-collapse\" >{1}</a></h4 >",
                    Constants.Style.WidgetClass.PanelTitleClass,
                    title.ToUpper(),
                    iconHtml,
                    id);              
            }

            RouteValueDictionary dictionary = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            dictionary.Add("id", id);
            if (dictionary.ContainsKey("class"))
            {
                dictionary["class"] = dictionary["class"] + Constants.Style.WidgetClass.PanelGroupClass;
            }
            else
            {
                dictionary["class"] = Constants.Style.WidgetClass.PanelGroupClass;
            }

            //dictionary.Add("Bc-panelgroup-autoGridViewFixedHeight", autoGridViewFixedHeight.ToString().ToLower());
            //if (gridViewFixedHeightName == null)
            //    gridViewFixedHeightName = "gridViewIndex";

            //dictionary.Add("Bc-panelgroup-gridViewFixedHeightName", gridViewFixedHeightName);
            dictionary.Add("Bc-panelgroup", "");

            //if (!marginFixedHeight.HasValue)
            //    marginFixedHeight = 30;

            //dictionary.Add("Bc-panelgroup-marginFixedHeight", marginFixedHeight);

            StringBuilder builderAttrs = new StringBuilder();
            foreach (var attr in dictionary)
            {
                builderAttrs.AppendFormat(" {0}=\"{1}\"", attr.Key, attr.Value);
            }

            html = string.Format("<div {0}><div class=\"{1}\"><div class=\"{2}\">{3} </div> <div id=\"{4}-collapse\" class=\"panel-collapse collapse {5}\"><div class=\"{6}\">",
                builderAttrs.ToString(),
                Constants.Style.WidgetClass.PanelGroupDefaultClass,
                Constants.Style.WidgetClass.PanelGroupHeadingClass,
                titleBlock,
                id,
                (!collapse?"in":""),
                (paddingContent?"pnpadding":""));            

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

        internal static void EndPanelGroup(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("</div></div></div></div>");
        }

        public static MvcContent BcBeginPanelGroup(this System.Web.Mvc.HtmlHelper htmlHelper,
            string id, string title, string iconClass,
            bool collapse = false,
            bool paddingContent = true,
            //bool autoGridViewFixedHeight = false,
            //string gridViewFixedHeightName = null,
            //int? marginFixedHeight = null, 
            object htmlAttributes = null)
        {
            return new MvcContent(
                () => htmlHelper.BeginPanelGroup(id,title, iconClass: iconClass,
                collapse: collapse,
                paddingContent: paddingContent,
                //autoGridViewFixedHeight : autoGridViewFixedHeight, 
                //gridViewFixedHeightName: gridViewFixedHeightName,
                htmlAttributes: htmlAttributes),
                () => htmlHelper.EndPanelGroup()
            );
        }

        public static MvcContent BcBeginPanelGroup(this System.Web.Mvc.HtmlHelper htmlHelper,
            string id,string title = null, Icons? icon = null, 
            bool collapse = false,
            bool paddingContent = true,
            //bool autoGridViewFixedHeight = false,
            //string gridViewFixedHeightName = null,
            //int? marginFixedHeight = null,
            object htmlAttributes = null)
        {
            return new MvcContent(
                () => htmlHelper.BeginPanelGroup(id,title, icon,
                collapse : collapse,
                paddingContent: paddingContent,
                //autoGridViewFixedHeight: autoGridViewFixedHeight,
                //gridViewFixedHeightName: gridViewFixedHeightName,
                htmlAttributes: htmlAttributes),
                () => htmlHelper.EndPanelGroup()
            );
        }

        //public static MvcContent BcBeginTabContentBox(this System.Web.Mvc.HtmlHelper htmlHelper,
        //    string title, TabCollection tabs, string iconClass = null, bool paddingContent = false)
        //{
        //    return new MvcContent(
        //        () => htmlHelper.BeginContentBox(title, iconClass, paddingContent, tabs),
        //        () => htmlHelper.EndContentBox()
        //    );
        //}
    }
}
