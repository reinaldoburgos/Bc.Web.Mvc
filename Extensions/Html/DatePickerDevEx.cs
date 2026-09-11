using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Bc.Web.Mvc.Html;
using System.Web.Mvc.Html;

namespace Bc.Web.Mvc.Html
{
    public static class DatePickerDevExExtensions
    {
        public static MvcHtmlString BcDatePickerDevExFor<TModel>(this HtmlHelper<TModel> htmlHelper,
                Expression<Func<TModel, DateTime?>> expression, object htmlAttributes = null,
                bool includeMessageValidation = false,
                bool includeTime = false, DateTime? minDate = null, DateTime? maxDate = null,
                bool readOnly = false)
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(propertyName);
            var id = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(propertyName);

            DateTime? value = null;
            if (htmlHelper.ViewData.Model != null)
            {
                var func = expression.Compile();
                value = func(htmlHelper.ViewData.Model);
            }

            // Contenedor para DevExtreme
            var div = new TagBuilder("div");
            div.Attributes["id"] = id;
            div.Attributes["name"] = fullName;

            div.AddCssClass("datepicker form-control");
            div.AddCssClass(includeTime ? "BcDateTimeInputDevEx" : "BcDateInputDevEx"); // mantiene tu clase para auto-inicialización
            div.AddCssClass("dx-datebox");  // marca semántica

            // Valor inicial en ISO para parseo robusto
            if (value.HasValue)
                div.Attributes["data-value"] = value.Value.ToString("yyyy-MM-ddTHH:mm:ss");

            // Formato visual fijo
            div.Attributes["data-display-format"] = includeTime ? "dd/MM/yyyy HH:mm" : "dd/MM/yyyy";
            div.Attributes["data-include-time"] = includeTime ? "true" : "false";

            if (minDate.HasValue)
                div.Attributes["data-min-date"] = minDate.Value.ToString("yyyy-MM-ddTHH:mm:ss");
            if (maxDate.HasValue)
                div.Attributes["data-max-date"] = maxDate.Value.ToString("yyyy-MM-ddTHH:mm:ss");

            if (readOnly)
                div.Attributes["data-readonly"] = "true";

            // Merge de atributos extra (si los necesitas)
            if (htmlAttributes != null)
            {
                var extra = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var kvp in extra)
                    div.MergeAttribute(kvp.Key.Replace('_', '-'), Convert.ToString(kvp.Value), replaceExisting: true);
            }

            var editor = div.ToString(TagRenderMode.Normal);

            var validationMessage = string.Empty;
            if (includeMessageValidation)
                validationMessage = htmlHelper.ValidationMessageFor(expression).ToString();

            return MvcHtmlString.Create(editor + validationMessage);
        }


        public static MvcHtmlString BcDatePickerDevEx(this HtmlHelper htmlHelper, string name, DateTime? value,
    object htmlAttributes = null, bool includeTime = false, DateTime? minDate = null, DateTime? maxDate = null,
    bool readOnly = false)
        {
            var div = new TagBuilder("div");
            div.Attributes["id"] = name;
            div.Attributes["name"] = name;

            div.AddCssClass("datepicker form-control");
            div.AddCssClass(includeTime ? "BcDateTimeInputDevEx" : "BcDateInputDevEx"); // mantiene tu clase para auto-inicialización
            div.AddCssClass("dx-datebox");

            if (value.HasValue)
                div.Attributes["data-value"] = value.Value.ToString("yyyy-MM-ddTHH:mm:ss");

            div.Attributes["data-display-format"] = includeTime ? "dd/MM/yyyy HH:mm" : "dd/MM/yyyy";
            div.Attributes["data-include-time"] = includeTime ? "true" : "false";

            if (minDate.HasValue)
                div.Attributes["data-min-date"] = minDate.Value.ToString("yyyy-MM-ddTHH:mm:ss");
            if (maxDate.HasValue)
                div.Attributes["data-max-date"] = maxDate.Value.ToString("yyyy-MM-ddTHH:mm:ss");

            if (readOnly)
                div.Attributes["data-readonly"] = "true";

            if (htmlAttributes != null)
            {
                var extra = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var kvp in extra)
                    div.MergeAttribute(kvp.Key.Replace('_', '-'), Convert.ToString(kvp.Value), replaceExisting: true);
            }

            return MvcHtmlString.Create(div.ToString(TagRenderMode.Normal));
        }
    }
}