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
    /// Selector una-opción-entre-N (pills). Sprint 2.7. Aditivo; no reemplaza BcRadioButtonGroup.
    /// Look: Notificación "Enviar a" (fila, anchos iguales). Radios nativos con piel .dp-pill.
    /// </summary>
    public static class PillGroupExtensions
    {
        public static MvcHtmlString BcPillGroup(this HtmlHelper htmlHelper, string name,
            IEnumerable<SelectListItem> selectList, object selectedValue = null,
            object htmlAttributes = null, bool disabled = false)
        {
            return RenderPillGroup(htmlHelper, name, selectList, selectedValue, htmlAttributes, disabled);
        }

        public static MvcHtmlString BcPillGroup(this HtmlHelper htmlHelper, string name,
            SelectList selectList, object selectedValue = null,
            object htmlAttributes = null, bool disabled = false)
        {
            return htmlHelper.BcPillGroup(name, (IEnumerable<SelectListItem>)selectList, selectedValue,
                htmlAttributes, disabled);
        }

        public static MvcHtmlString BcPillGroupFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList,
            object htmlAttributes = null, string name = null, bool disabled = false)
        {
            object selectedValue = null;
            if (htmlHelper.ViewData.Model != null)
            {
                selectedValue = expression.Compile()(htmlHelper.ViewData.Model);
            }

            string fieldName = !string.IsNullOrEmpty(name)
                ? name
                : ExpressionHelper.GetExpressionText(expression);

            TagBuilder group = CreateGroup(htmlAttributes);
            StringBuilder inner = new StringBuilder();

            if (selectList != null)
            {
                foreach (SelectListItem item in selectList)
                {
                    if (item == null)
                    {
                        continue;
                    }

                    bool isOn = IsSelected(item, selectedValue);
                    inner.Append(BuildPillFor(htmlHelper, expression, item, isOn, disabled, fieldName));
                }
            }

            group.InnerHtml = inner.ToString();
            return MvcHtmlString.Create(group.ToString());
        }

        public static MvcHtmlString BcPillGroupFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, SelectList selectList,
            object htmlAttributes = null, string name = null, bool disabled = false)
        {
            return htmlHelper.BcPillGroupFor(expression, (IEnumerable<SelectListItem>)selectList,
                htmlAttributes, name, disabled);
        }

        private static MvcHtmlString RenderPillGroup(HtmlHelper htmlHelper, string name,
            IEnumerable<SelectListItem> selectList, object selectedValue, object htmlAttributes,
            bool disabled)
        {
            TagBuilder group = CreateGroup(htmlAttributes);
            StringBuilder inner = new StringBuilder();

            if (selectList != null)
            {
                foreach (SelectListItem item in selectList)
                {
                    if (item == null)
                    {
                        continue;
                    }

                    bool isOn = IsSelected(item, selectedValue);
                    inner.Append(BuildPill(htmlHelper, name, item, isOn, disabled));
                }
            }

            group.InnerHtml = inner.ToString();
            return MvcHtmlString.Create(group.ToString());
        }

        private static TagBuilder CreateGroup(object htmlAttributes)
        {
            TagBuilder group = new TagBuilder("div");
            group.AddCssClass(Constants.Style.ContentClass.UiPillsClass);
            group.Attributes["role"] = "radiogroup";
            ApplyHtmlAttributes(group, htmlAttributes);
            return group;
        }

        private static string BuildPill(HtmlHelper htmlHelper, string name, SelectListItem item,
            bool isOn, bool disabled)
        {
            TagBuilder label = CreatePillLabel(isOn);

            IDictionary<string, object> inputAttrs = CreateInputAttributes(isOn, disabled);
            string radioHtml = htmlHelper.RadioButton(name, item.Value, isOn, inputAttrs).ToString();

            label.InnerHtml = radioHtml + BuildPillText(item);
            return label.ToString();
        }

        private static string BuildPillFor<TModel, TValue>(HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, SelectListItem item, bool isOn,
            bool disabled, string name)
        {
            TagBuilder label = CreatePillLabel(isOn);

            IDictionary<string, object> inputAttrs = CreateInputAttributes(isOn, disabled);
            string radioHtml = htmlHelper.RadioButtonFor(expression, item.Value, inputAttrs).ToString();

            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    var element = System.Xml.Linq.XElement.Parse(radioHtml);
                    element.SetAttributeValue("name", name);
                    if (disabled)
                    {
                        element.SetAttributeValue("disabled", "disabled");
                    }
                    if (isOn)
                    {
                        element.SetAttributeValue("checked", "checked");
                    }
                    radioHtml = element.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
                }
                catch
                {
                    // keep original markup
                }
            }

            label.InnerHtml = radioHtml + BuildPillText(item);
            return label.ToString();
        }

        private static TagBuilder CreatePillLabel(bool isOn)
        {
            TagBuilder label = new TagBuilder("label");
            label.AddCssClass(Constants.Style.ContentClass.UiPillClass);
            if (isOn)
            {
                label.AddCssClass(Constants.Style.ContentClass.UiPillOnClass);
            }
            return label;
        }

        private static IDictionary<string, object> CreateInputAttributes(bool isOn, bool disabled)
        {
            IDictionary<string, object> inputAttrs = new RouteValueDictionary();
            inputAttrs["class"] = Constants.Style.ContentClass.UiPillInputClass;
            if (isOn)
            {
                inputAttrs["checked"] = "checked";
            }
            if (disabled)
            {
                inputAttrs["disabled"] = "disabled";
            }
            return inputAttrs;
        }

        private static string BuildPillText(SelectListItem item)
        {
            string text = HttpUtility.HtmlEncode(item.Text ?? item.Value ?? string.Empty);
            return string.Format("<span class=\"{0}\">{1}</span>",
                Constants.Style.ContentClass.UiPillTextClass, text);
        }

        private static bool IsSelected(SelectListItem item, object selectedValue)
        {
            if (selectedValue == null)
            {
                return item.Selected;
            }

            string selected = Convert.ToString(selectedValue);
            if (string.Equals(selected, item.Value, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return selectedValue.Equals(item.Value);
        }

        private static void ApplyHtmlAttributes(TagBuilder builder, object htmlAttributes)
        {
            if (htmlAttributes == null)
            {
                return;
            }

            RouteValueDictionary attributes;
            IDictionary<string, object> dict = htmlAttributes as IDictionary<string, object>;
            if (dict != null)
            {
                attributes = new RouteValueDictionary(dict);
            }
            else
            {
                attributes = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            }

            foreach (KeyValuePair<string, object> attribute in attributes)
            {
                if (string.Equals(attribute.Key, "class", StringComparison.OrdinalIgnoreCase))
                {
                    builder.AddCssClass(Convert.ToString(attribute.Value));
                }
                else if (attribute.Value != null)
                {
                    builder.MergeAttribute(attribute.Key, Convert.ToString(attribute.Value), true);
                }
            }
        }
    }
}
