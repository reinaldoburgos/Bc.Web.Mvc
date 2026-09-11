using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public static class RadioGroupExtensions
    {
        public static MvcHtmlString BcRadioButton(this HtmlHelper htmlHelper, string name,
           object value, object htmlAttributes = null, string label = null)
        {
            TagBuilder maintag = new TagBuilder("label");
            maintag.AddCssClass(Bc.Web.Mvc.Html.Constants.Style.ElementClass.RadioButtonInlineClass);

            var attr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                        new { @class = Bc.Web.Mvc.Html.Constants.Style.ElementClass.RadioButtonClass },
                        htmlAttributes);

            maintag.InnerHtml = htmlHelper.RadioButton(name, value, htmlAttributes: attr).ToString();

            if (!string.IsNullOrWhiteSpace(label))
                maintag.InnerHtml += " " + label;

            return MvcHtmlString.Create(maintag.ToString());
        }

        public static MvcHtmlString BcRadioButtonGroup(this HtmlHelper htmlHelper, string name, SelectList selectList,
            object htmlAttributes = null, object selectedValue = null)
        {
            StringBuilder html = new StringBuilder();
            foreach (SelectListItem item in selectList)
            {
                IDictionary<string, object> editorAttr = null;
                if ((selectedValue != null && selectedValue.Equals(item.Value)) || (selectedValue == null && item.Selected))
                {
                    editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                        new { @checked = "" },
                        htmlAttributes);
                }
                else
                {
                    editorAttr = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                }
                html.Append(htmlHelper.BcRadioButton(name, item.Value, editorAttr, item.Text).ToString());
            }

            return MvcHtmlString.Create(html.ToString());
        }

        public static MvcHtmlString BcRadioButtonFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, object value, object htmlAttributes = null, string label = null,
           string name = null, bool disabled = false)
        {
            RouteValueDictionary editorAttr = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            return htmlHelper.BcRadioButtonFor(expression, value, editorAttr, name: name, disabled: disabled);
        }

        public static MvcHtmlString BcRadioButtonFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, object value, IDictionary<string, object> htmlAttributes,
           string label = null, string name = null, bool disabled = false)
        {
            TagBuilder maintag = new TagBuilder("label");
            maintag.AddCssClass(Bc.Web.Mvc.Html.Constants.Style.ElementClass.RadioButtonInlineClass);

            var attr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                        new { @class = Bc.Web.Mvc.Html.Constants.Style.ElementClass.RadioButtonClass },
                        htmlAttributes);

            //maintag.InnerHtml = htmlHelper.RadioButtonFor(expression, value, htmlAttributes: attr).ToString();

            var radioButton = htmlHelper.RadioButtonFor(expression, value, htmlAttributes: attr).ToString();
            var element = System.Xml.Linq.XElement.Parse(radioButton);

            if (!string.IsNullOrEmpty(name))
                element.SetAttributeValue("name", name);

            if (disabled)
                element.SetAttributeValue("disabled", "");

            maintag.InnerHtml = element.ToString();

            if (!string.IsNullOrWhiteSpace(label))
                maintag.InnerHtml += " " + "<span>" + label + "</span>";

            return MvcHtmlString.Create(maintag.ToString());
        }

        //public static MvcHtmlString BcRadioButtonGroupGeneralValuesFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
        //   Expression<Func<TModel, TValue>> expression, short id, object htmlAttributes = null)
        //{
        //    GeneralProxy proxy = new GeneralProxy();

        //    var selectList = proxy.GetGeneralValuesById(id).ToSelectList("Code", "Content");

        //    return htmlHelper.BcRadioButtonGroupFor(expression, selectList, htmlAttributes);
        //}

        public static MvcHtmlString BcRadioButtonGroupFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, SelectList selectList, object htmlAttributes = null,
           string name = null, bool disabled = false)
        {
            Func<TModel, TValue> method = expression.Compile();
            TValue value = method(htmlHelper.ViewData.Model);

            StringBuilder html = new StringBuilder();

            html.Append("<div class='bc-radiogroup'>");

            foreach (SelectListItem item in selectList)
            {
                IDictionary<string, object> editorAttr = null;
                if (value != null && value.Equals(item.Value))
                {
                    editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                        new { @checked = "" },
                        htmlAttributes);
                }
                else
                {
                    editorAttr = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                }
                html.Append(htmlHelper.BcRadioButtonFor(expression, item.Value, editorAttr, item.Text, name: name, disabled: disabled).ToString());
            }

            html.Append("</div>");

            return MvcHtmlString.Create(html.ToString());
        }
    }
}
