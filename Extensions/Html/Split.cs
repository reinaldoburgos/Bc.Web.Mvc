using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    /// <summary>
    /// Layout multipanel (2 o 3 columnas, anchos distintos).
    /// Shell + panes; chrome de lista (colapsar/buscar) es opcional por pane.
    /// No reemplaza BcBeginUiPanel ni el contenido AJAX de cada pantalla.
    /// </summary>
    public static class SplitExtensions
    {
        public static MvcContent BcBeginSplit(this HtmlHelper htmlHelper,
            object htmlAttributes = null,
            int? gap = null)
        {
            string css = Constants.Style.ContentClass.SplitClass;
            IDictionary<string, object> attrs = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = css },
                htmlAttributes);

            if (gap != null && gap.Value >= 0)
            {
                string style = attrs.ContainsKey("style") ? Convert.ToString(attrs["style"]) : string.Empty;
                if (!string.IsNullOrWhiteSpace(style) && !style.TrimEnd().EndsWith(";"))
                {
                    style += ";";
                }
                attrs["style"] = style + " gap: " + gap.Value + "px;";
            }

            return new MvcContent(
                () => htmlHelper.BcBeginContent(htmlAttributes: attrs),
                () => htmlHelper.BcEndContent()
            );
        }

        public static MvcContent BcBeginSplitPane(this HtmlHelper htmlHelper,
            SplitPaneSize size = SplitPaneSize.Fill,
            bool collapsible = false,
            string searchPlaceholder = null,
            string searchName = "findControl",
            string id = null,
            string collapseTitle = "Contraer",
            object htmlAttributes = null)
        {
            return new MvcContent(
                () => htmlHelper.BeginSplitPane(size, collapsible, searchPlaceholder, searchName, id,
                    collapseTitle, htmlAttributes),
                () => htmlHelper.EndSplitPane()
            );
        }

        private static void BeginSplitPane(this HtmlHelper htmlHelper,
            SplitPaneSize size, bool collapsible, string searchPlaceholder, string searchName,
            string id, string collapseTitle, object htmlAttributes)
        {
            string css = Constants.Style.ContentClass.SplitPaneClass
                + " " + SizeClass(size);

            if (collapsible)
            {
                css += " " + Constants.Style.ContentClass.SplitPaneCollapsibleClass;
            }

            if (string.IsNullOrEmpty(id) && collapsible)
            {
                id = "collapsibleDiv";
            }

            object defaults = string.IsNullOrEmpty(id)
                ? (object)new { @class = css }
                : new { id = id, @class = css };

            IDictionary<string, object> attrs = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                defaults,
                htmlAttributes);

            TagBuilder pane = new TagBuilder("div");
            foreach (var attr in attrs)
            {
                pane.Attributes[attr.Key] = Convert.ToString(attr.Value);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(pane.ToString(TagRenderMode.StartTag));

            bool showToolbar = collapsible || !string.IsNullOrEmpty(searchPlaceholder);
            if (showToolbar)
            {
                sb.Append(BuildToolbar(htmlHelper, collapsible, searchPlaceholder, searchName, collapseTitle));
            }

            htmlHelper.ViewContext.Writer.Write(sb.ToString());
        }

        private static void EndSplitPane(this HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("</div>");
        }

        private static string BuildToolbar(HtmlHelper htmlHelper, bool collapsible,
            string searchPlaceholder, string searchName, string collapseTitle)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"{0} search-box\" id=\"bctoolbar\">",
                Constants.Style.ContentClass.SplitToolbarClass);

            if (collapsible)
            {
                string title = HttpUtility.HtmlAttributeEncode(
                    string.IsNullOrEmpty(collapseTitle) ? "Contraer" : collapseTitle);
                sb.AppendFormat(
                    "<div id=\"bcicontoolbar\" class=\"{0} master-collapse-btn\" onclick=\"bcToggleCollapsibleDiv()\" title=\"{1}\">",
                    Constants.Style.ContentClass.SplitCollapseClass,
                    title);
                sb.Append("<i class=\"fa fa-bars\" aria-hidden=\"true\"></i>");
                sb.Append("</div>");
            }

            if (!string.IsNullOrEmpty(searchPlaceholder))
            {
                string name = string.IsNullOrEmpty(searchName) ? "findControl" : searchName;
                sb.AppendFormat("<div class=\"{0} input-wrap\">",
                    Constants.Style.ContentClass.SplitSearchClass);
                sb.Append("<i class=\"fa fa-search\"></i>");
                sb.Append(htmlHelper.BcTextBox(name, htmlAttributes: new
                {
                    id = name,
                    PlaceHolder = searchPlaceholder
                }).ToHtmlString());
                sb.Append("</div>");
            }

            sb.Append("</div>");
            return sb.ToString();
        }

        private static string SizeClass(SplitPaneSize size)
        {
            switch (size)
            {
                case SplitPaneSize.Rail:
                    return Constants.Style.ContentClass.SplitPaneRailClass;
                case SplitPaneSize.Side:
                    return Constants.Style.ContentClass.SplitPaneSideClass;
                case SplitPaneSize.Fr1:
                    return Constants.Style.ContentClass.SplitPaneFr1Class;
                case SplitPaneSize.Fr2:
                    return Constants.Style.ContentClass.SplitPaneFr2Class;
                case SplitPaneSize.Fr3:
                    return Constants.Style.ContentClass.SplitPaneFr3Class;
                case SplitPaneSize.Fr4:
                    return Constants.Style.ContentClass.SplitPaneFr4Class;
                case SplitPaneSize.Fr5:
                    return Constants.Style.ContentClass.SplitPaneFr5Class;
                default:
                    return Constants.Style.ContentClass.SplitPaneFillClass;
            }
        }
    }
}
