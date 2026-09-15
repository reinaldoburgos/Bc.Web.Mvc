using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    /// <summary>
    /// Switch Sí/No sin bootstrap-toggle. Sprint 2 deuda / P4.
    /// Checkbox nativo (POST bool). Opcional: etiquetas on/off (Cualitativa, Matriculados…).
    /// Aditivo: no reemplaza BcCheckBoxToggleFor.
    /// </summary>
    public static class SwitchExtensions
    {
        public static MvcHtmlString BcSwitchFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression,
            Icons? dataOnIcon = Icons.Check,
            Icons? dataOffIcon = Icons.Cancel,
            string dataOn = "Si",
            ElementThemeType dataOnthemeType = ElementThemeType.Primary,
            string dataOff = "No",
            ElementThemeType dataOffthemeType = ElementThemeType.Default,
            int? width = null,
            bool showLabels = true,
            object htmlAttributes = null,
            bool readOnly = false)
        {
            bool isOn = false;
            if (htmlHelper.ViewData.Model != null)
            {
                isOn = expression.Compile()(htmlHelper.ViewData.Model);
            }

            IDictionary<string, object> inputAttrs = new RouteValueDictionary(
                System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            inputAttrs["class"] = MergeClass(inputAttrs, Constants.Style.ContentClass.UiSwitchInputClass);
            inputAttrs["bcType"] = "CheckBox";
            if (readOnly)
            {
                inputAttrs["disabled"] = "disabled";
            }

            MvcHtmlString checkBoxHtml = htmlHelper.CheckBoxFor(expression, inputAttrs);

            TagBuilder wrap = new TagBuilder("label");
            wrap.AddCssClass(Constants.Style.ContentClass.UiSwitchClass);
            if (isOn)
            {
                wrap.AddCssClass(Constants.Style.ContentClass.UiSwitchOnClass);
            }
            if (showLabels)
            {
                wrap.AddCssClass(Constants.Style.ContentClass.UiSwitchLabeledClass);
            }
            if (readOnly)
            {
                wrap.AddCssClass(Constants.Style.GeneralClass.DisabledClass);
            }

            string onTheme = dataOnthemeType.GetID();
            if (!string.IsNullOrEmpty(onTheme))
            {
                wrap.AddCssClass("is-on-" + onTheme);
            }
            string offTheme = dataOffthemeType.GetID();
            if (!string.IsNullOrEmpty(offTheme) && offTheme != "default")
            {
                wrap.AddCssClass("is-off-" + offTheme);
            }

            if (width.HasValue && width.Value > 0)
            {
                wrap.Attributes["style"] = string.Format("--dp-switch-width:{0}px;", width.Value);
            }

            wrap.Attributes["data-dp-switch"] = "";
            wrap.Attributes["role"] = "switch";
            wrap.Attributes["aria-checked"] = isOn ? "true" : "false";
            if (readOnly)
            {
                wrap.Attributes["aria-disabled"] = "true";
            }

            StringBuilder ui = new StringBuilder();
            ui.AppendFormat("<span class=\"{0}\" aria-hidden=\"true\">",
                Constants.Style.ContentClass.UiSwitchTrackClass);

            if (showLabels)
            {
                ui.AppendFormat("<span class=\"{0}\">{1}</span>",
                    Constants.Style.ContentClass.UiSwitchOnLabelClass,
                    BuildCaption(dataOnIcon, dataOn));
                ui.AppendFormat("<span class=\"{0}\">{1}</span>",
                    Constants.Style.ContentClass.UiSwitchOffLabelClass,
                    BuildCaption(dataOffIcon, dataOff));
            }

            ui.AppendFormat("<span class=\"{0}\"></span>",
                Constants.Style.ContentClass.UiSwitchKnobClass);
            ui.Append("</span>");

            wrap.InnerHtml = checkBoxHtml.ToString() + ui.ToString();
            return MvcHtmlString.Create(wrap.ToString());
        }

        public static MvcHtmlString BcSwitch(this HtmlHelper htmlHelper, string name,
            bool isChecked = false,
            Icons? dataOnIcon = Icons.Check,
            Icons? dataOffIcon = Icons.Cancel,
            string dataOn = "Si",
            ElementThemeType dataOnthemeType = ElementThemeType.Primary,
            string dataOff = "No",
            ElementThemeType dataOffthemeType = ElementThemeType.Default,
            int? width = null,
            bool showLabels = true,
            object htmlAttributes = null,
            bool readOnly = false)
        {
            IDictionary<string, object> inputAttrs = new RouteValueDictionary(
                System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            inputAttrs["class"] = MergeClass(inputAttrs, Constants.Style.ContentClass.UiSwitchInputClass);
            inputAttrs["type"] = "checkbox";
            inputAttrs["name"] = name;
            inputAttrs["id"] = name;
            inputAttrs["value"] = "true";
            inputAttrs["bcType"] = "CheckBox";
            if (isChecked)
            {
                inputAttrs["checked"] = "checked";
            }
            if (readOnly)
            {
                inputAttrs["disabled"] = "disabled";
            }

            TagBuilder input = new TagBuilder("input");
            foreach (var attr in inputAttrs)
            {
                input.Attributes[attr.Key] = Convert.ToString(attr.Value);
            }

            TagBuilder hidden = new TagBuilder("input");
            hidden.Attributes["type"] = "hidden";
            hidden.Attributes["name"] = name;
            hidden.Attributes["value"] = "false";

            TagBuilder wrap = new TagBuilder("label");
            wrap.AddCssClass(Constants.Style.ContentClass.UiSwitchClass);
            if (isChecked)
            {
                wrap.AddCssClass(Constants.Style.ContentClass.UiSwitchOnClass);
            }
            if (showLabels)
            {
                wrap.AddCssClass(Constants.Style.ContentClass.UiSwitchLabeledClass);
            }
            if (readOnly)
            {
                wrap.AddCssClass(Constants.Style.GeneralClass.DisabledClass);
            }

            string onTheme = dataOnthemeType.GetID();
            if (!string.IsNullOrEmpty(onTheme))
            {
                wrap.AddCssClass("is-on-" + onTheme);
            }
            string offTheme = dataOffthemeType.GetID();
            if (!string.IsNullOrEmpty(offTheme) && offTheme != "default")
            {
                wrap.AddCssClass("is-off-" + offTheme);
            }

            if (width.HasValue && width.Value > 0)
            {
                wrap.Attributes["style"] = string.Format("--dp-switch-width:{0}px;", width.Value);
            }

            wrap.Attributes["data-dp-switch"] = "";
            wrap.Attributes["role"] = "switch";
            wrap.Attributes["aria-checked"] = isChecked ? "true" : "false";

            StringBuilder ui = new StringBuilder();
            ui.AppendFormat("<span class=\"{0}\" aria-hidden=\"true\">",
                Constants.Style.ContentClass.UiSwitchTrackClass);
            if (showLabels)
            {
                ui.AppendFormat("<span class=\"{0}\">{1}</span>",
                    Constants.Style.ContentClass.UiSwitchOnLabelClass,
                    BuildCaption(dataOnIcon, dataOn));
                ui.AppendFormat("<span class=\"{0}\">{1}</span>",
                    Constants.Style.ContentClass.UiSwitchOffLabelClass,
                    BuildCaption(dataOffIcon, dataOff));
            }
            ui.AppendFormat("<span class=\"{0}\"></span>",
                Constants.Style.ContentClass.UiSwitchKnobClass);
            ui.Append("</span>");

            wrap.InnerHtml = input.ToString(TagRenderMode.SelfClosing)
                + hidden.ToString(TagRenderMode.SelfClosing)
                + ui.ToString();

            return MvcHtmlString.Create(wrap.ToString());
        }

        private static string BuildCaption(Icons? icon, string text)
        {
            StringBuilder sb = new StringBuilder();
            if (icon.HasValue)
            {
                string iconClass = icon.Value.GetID();
                if (!string.IsNullOrEmpty(iconClass))
                {
                    sb.AppendFormat("<i class=\"{0}\" aria-hidden=\"true\"></i> ",
                        HttpUtility.HtmlAttributeEncode(iconClass));
                }
            }
            if (!string.IsNullOrEmpty(text))
            {
                sb.Append(HttpUtility.HtmlEncode(text));
            }
            return sb.ToString();
        }

        private static string MergeClass(IDictionary<string, object> attrs, string addClass)
        {
            object existing;
            if (attrs.TryGetValue("class", out existing) && existing != null)
            {
                return Convert.ToString(existing) + " " + addClass;
            }
            return addClass;
        }
    }
}
