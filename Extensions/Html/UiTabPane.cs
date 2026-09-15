using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    /// <summary>
    /// Panel con pestañas + panes (Sprint 2.4). Aditivo; no reemplaza BcBeginTabContentBox / BcBeginTabPane.
    /// Uso:
    ///   @using (Html.BcBeginUiTabPanel(tabs)) {
    ///     using (Html.BcBeginUiTabActions()) { botones opcionales }
    ///     using (Html.BcBeginUiTabPane(...)) { ... }
    ///   }
    /// </summary>
    public static class UiTabPaneExtensions
    {
        private const string TabsBarOpenKey = "Bc.UiTabPanel.TabsBarOpen";

        public static MvcContent BcBeginUiTabPanel(this HtmlHelper htmlHelper, TabCollection tabs,
            object htmlAttributes = null)
        {
            return new MvcContent(
                () => htmlHelper.BeginUiTabPanel(tabs, htmlAttributes),
                () => htmlHelper.EndUiTabPanel()
            );
        }

        public static MvcContent BcBeginUiTabActions(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            return new MvcContent(
                () => htmlHelper.BeginUiTabActions(htmlAttributes),
                () => htmlHelper.EndUiTabActions()
            );
        }

        public static MvcContent BcBeginUiTabPane(this HtmlHelper htmlHelper, string id,
            bool active = false, object htmlAttributes = null)
        {
            return new MvcContent(
                () =>
                {
                    htmlHelper.CloseUiTabBarIfOpen();
                    htmlHelper.BeginUiTabPaneContent(id, active, htmlAttributes);
                },
                () => htmlHelper.BcEndContent()
            );
        }

        public static MvcContent BcBeginUiTabPane(this HtmlHelper htmlHelper, TabPane tab,
            object htmlAttributes = null)
        {
            if (tab == null)
            {
                return htmlHelper.BcBeginUiTabPane(string.Empty, false, htmlAttributes);
            }

            return htmlHelper.BcBeginUiTabPane(tab.Key, tab.Selected, htmlAttributes);
        }

        private static void BeginUiTabPanel(this HtmlHelper htmlHelper, TabCollection tabs, object htmlAttributes)
        {
            IDictionary<string, object> sectionAttrs =
                Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                    new { @class = Constants.Style.ContentClass.UiTabPanelClass },
                    htmlAttributes);

            TagBuilder section = new TagBuilder("section");
            foreach (var attr in sectionAttrs)
            {
                section.Attributes[attr.Key] = Convert.ToString(attr.Value);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(section.ToString(TagRenderMode.StartTag));
            sb.AppendFormat("<div class=\"{0}\">", Constants.Style.ContentClass.UiTabBarClass);
            sb.AppendFormat("<div class=\"{0}\">", Constants.Style.ContentClass.UiTabsClass);
            sb.Append(BuildTabButtons(tabs));
            sb.Append("</div>");

            htmlHelper.ViewContext.Writer.Write(sb.ToString());
            SetTabsBarOpen(true);
        }

        private static void EndUiTabPanel(this HtmlHelper htmlHelper)
        {
            htmlHelper.CloseUiTabBarIfOpen();
            htmlHelper.ViewContext.Writer.Write("</section>");
        }

        private static void BeginUiTabActions(this HtmlHelper htmlHelper, object htmlAttributes)
        {
            IDictionary<string, object> attrs =
                Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                    new { @class = Constants.Style.ContentClass.FormActionClass },
                    htmlAttributes);

            htmlHelper.BcBeginContent(htmlAttributes: attrs);
        }

        private static void EndUiTabActions(this HtmlHelper htmlHelper)
        {
            htmlHelper.BcEndContent();
            htmlHelper.CloseUiTabBarIfOpen();
        }

        private static void BeginUiTabPaneContent(this HtmlHelper htmlHelper, string id, bool active,
            object htmlAttributes)
        {
            string cssClass = Constants.Style.ContentClass.UiTabPaneClass;
            if (active)
            {
                cssClass += " " + Constants.Style.ContentClass.UiTabPaneOnClass;
            }

            IDictionary<string, object> resultHtmlAttributes =
                Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                    new
                    {
                        id = id,
                        @class = cssClass,
                        data_dp_pane = id
                    },
                    htmlAttributes);

            htmlHelper.BcBeginContent(htmlAttributes: resultHtmlAttributes);
        }

        private static void CloseUiTabBarIfOpen(this HtmlHelper htmlHelper)
        {
            if (!IsTabsBarOpen())
            {
                return;
            }

            htmlHelper.ViewContext.Writer.Write("</div>");
            SetTabsBarOpen(false);
        }

        private static string BuildTabButtons(TabCollection tabs)
        {
            if (tabs == null || tabs.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            foreach (TabPane tab in tabs)
            {
                if (tab == null || string.IsNullOrEmpty(tab.Key))
                {
                    continue;
                }

                TagBuilder btn = new TagBuilder("button");
                btn.Attributes["type"] = "button";
                btn.AddCssClass(Constants.Style.ContentClass.UiTabClass);
                if (tab.Selected)
                {
                    btn.AddCssClass(Constants.Style.ContentClass.UiTabPaneOnClass);
                }
                btn.Attributes["data-dp-tab"] = tab.Key;
                if (!string.IsNullOrEmpty(tab.Id))
                {
                    btn.Attributes["id"] = tab.Id;
                }

                string label = HttpUtility.HtmlEncode(tab.Name ?? tab.Key);
                if (!string.IsNullOrWhiteSpace(tab.Icon))
                {
                    string color = string.IsNullOrWhiteSpace(tab.IconColor)
                        ? string.Empty
                        : string.Format(" style=\"color:{0}\"", HttpUtility.HtmlAttributeEncode(tab.IconColor));
                    btn.InnerHtml = string.Format("<i class=\"{0}\"{1}></i> {2}",
                        HttpUtility.HtmlAttributeEncode(tab.Icon), color, label);
                }
                else
                {
                    btn.InnerHtml = label;
                }

                sb.Append(btn.ToString());
            }

            return sb.ToString();
        }

        private static bool IsTabsBarOpen()
        {
            if (HttpContext.Current == null)
            {
                return false;
            }

            return HttpContext.Current.Items[TabsBarOpenKey] as bool? == true;
        }

        private static void SetTabsBarOpen(bool open)
        {
            if (HttpContext.Current == null)
            {
                return;
            }

            HttpContext.Current.Items[TabsBarOpenKey] = open;
        }
    }
}
