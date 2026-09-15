using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    /// <summary>
    /// Panel único (look de card). Sprint 2.6. Aditivo; no reemplaza BcBeginPanelGroup.
    /// Modos: estático / colapsable / acción (+) en cabecera.
    /// Tabs: usar BcBeginUiTabPanel (Sprint 2.4), misma familia CSS .dp-acc.
    /// </summary>
    public static class UiPanelExtensions
    {
        public static MvcContent BcBeginUiPanel(this HtmlHelper htmlHelper,
            string id, string title = null, string iconClass = null,
            bool collapsible = false,
            bool collapse = false,
            bool paddingContent = true,
            object htmlAttributes = null,
            ACollection aCollection = null)
        {
            return new MvcContent(
                () => htmlHelper.BeginUiPanel(id, title, iconClass, collapsible, collapse,
                    paddingContent, htmlAttributes, aCollection),
                () => htmlHelper.EndUiPanel()
            );
        }

        public static MvcContent BcBeginUiPanel(this HtmlHelper htmlHelper,
            string id, string title = null, Icons? icon = null,
            bool collapsible = false,
            bool collapse = false,
            bool paddingContent = true,
            object htmlAttributes = null,
            ACollection aCollection = null)
        {
            return htmlHelper.BcBeginUiPanel(id, title,
                icon != null ? icon.GetID() : null,
                collapsible, collapse, paddingContent, htmlAttributes, aCollection);
        }

        private static void BeginUiPanel(this HtmlHelper htmlHelper,
            string id, string title, string iconClass,
            bool collapsible, bool collapse, bool paddingContent,
            object htmlAttributes, ACollection aCollection)
        {
            string panelClass = Constants.Style.ContentClass.UiPanelClass;
            if (!collapsible)
            {
                panelClass += " " + Constants.Style.ContentClass.UiPanelStaticClass;
            }
            else if (!collapse)
            {
                panelClass += " " + Constants.Style.ContentClass.UiPanelOpenClass;
            }

            IDictionary<string, object> sectionAttrs =
                Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                    new { id = id, @class = panelClass },
                    htmlAttributes);

            TagBuilder section = new TagBuilder("section");
            foreach (var attr in sectionAttrs)
            {
                section.Attributes[attr.Key] = Convert.ToString(attr.Value);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(section.ToString(TagRenderMode.StartTag));
            sb.Append(BuildHead(title, iconClass, collapsible, aCollection));

            string bodyClass = Constants.Style.ContentClass.UiPanelBodyClass;
            if (!paddingContent)
            {
                bodyClass += " " + Constants.Style.GeneralClass.NoPaddingClass;
            }

            sb.AppendFormat("<div class=\"{0}\">", bodyClass);

            htmlHelper.ViewContext.Writer.Write(sb.ToString());
        }

        private static void EndUiPanel(this HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("</div></section>");
        }

        private static string BuildHead(string title, string iconClass, bool collapsible,
            ACollection aCollection)
        {
            if (string.IsNullOrWhiteSpace(title) && (aCollection == null || aCollection.Count == 0))
            {
                return string.Empty;
            }

            TagBuilder head = new TagBuilder("div");
            head.AddCssClass(Constants.Style.ContentClass.UiPanelHeadClass);
            if (collapsible)
            {
                head.Attributes["data-dp-acc"] = "";
                head.Attributes["role"] = "button";
                head.Attributes["tabindex"] = "0";
            }

            StringBuilder inner = new StringBuilder();
            inner.AppendFormat("<span class=\"{0}\">", Constants.Style.ContentClass.UiPanelTitleClass);
            if (!string.IsNullOrWhiteSpace(iconClass))
            {
                inner.AppendFormat("<i class=\"{0}\"></i> ", HttpUtility.HtmlAttributeEncode(iconClass));
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                inner.Append(HttpUtility.HtmlEncode(title));
            }
            inner.Append("</span>");

            if (aCollection != null)
            {
                foreach (AItem item in aCollection)
                {
                    if (item == null)
                    {
                        continue;
                    }

                    inner.Append(BuildHeaderAction(item));
                }
            }

            if (collapsible)
            {
                inner.Append("<i class=\"fa fa-chevron-down\" aria-hidden=\"true\"></i>");
            }

            head.InnerHtml = inner.ToString();
            return head.ToString();
        }

        private static string BuildHeaderAction(AItem item)
        {
            string title = item.Title ?? string.Empty;
            string icon = item.IconClass ?? "fa fa-plus";
            string onClick = item.OnClick ?? string.Empty;
            string href = item.Href ?? "#";

            TagBuilder btn = new TagBuilder("button");
            btn.Attributes["type"] = "button";
            btn.AddCssClass(Constants.Style.ContentClass.UiPanelAddClass);
            btn.Attributes["data-dp-acc-add"] = "";
            if (!string.IsNullOrEmpty(title))
            {
                btn.Attributes["title"] = title;
            }
            if (!string.IsNullOrEmpty(onClick))
            {
                btn.Attributes["onclick"] = onClick;
            }
            else if (!string.IsNullOrEmpty(href) && href != "#")
            {
                btn.Attributes["onclick"] = string.Format("window.location.href='{0}';",
                    HttpUtility.JavaScriptStringEncode(href));
            }

            btn.InnerHtml = string.Format("<i class=\"{0}\" aria-hidden=\"true\"></i>",
                HttpUtility.HtmlAttributeEncode(icon));

            return btn.ToString();
        }
    }
}
