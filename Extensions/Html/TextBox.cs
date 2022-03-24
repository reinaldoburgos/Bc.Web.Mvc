using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public enum NumericType
    {
        [ID("number")]
        Number,
        [ID("currency")]
        Currency,
        [ID("integer")]
        Integer
    }

    public enum TextAlign
    {
        Left,
        Center,
        Right
    }

    public static class TextBoxExtensions
    {
        public static MvcHtmlString BcTextBox(this HtmlHelper htmlHelper, string name, string value = "", object htmlAttributes = null, string format = null,
            bool readOnly = false, TextAlign textAlign = TextAlign.Left)
        {
            RouteValueDictionary dictionary = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            return htmlHelper.BcTextBox(name, value, htmlAttributes: dictionary, format: format, readOnly: readOnly, textAlign: textAlign);
        }

        public static MvcHtmlString BcTextBox(this HtmlHelper htmlHelper, string name, string value, IDictionary<string, object> htmlAttributes,
            string format = null, bool readOnly = false, TextAlign textAlign = TextAlign.Left)
        {
            var editorAttr = GetResultHtmlAttributes(htmlAttributes, readOnly, textAlign: textAlign);

            string editor = htmlHelper.TextBox(name, value, htmlAttributes: editorAttr, format: format).ToString();

            return MvcHtmlString.Create(editor.ToString());
        }



        public static MvcHtmlString BcNumericTextBox(this HtmlHelper htmlHelper, string name, object value,
            IDictionary<string, object> htmlAttributes,
            byte? precision = null, NumericType numericType = NumericType.Number, bool nullable = true,
            string currencySymbol = null, TextAlign textAlign = TextAlign.Right, bool readOnly = false)
        {
            var editorAttr = GetResultHtmlAttributes(htmlAttributes, isNumeric: true, nullable: nullable, numericType: numericType,
                precision: precision, currencySymbol: currencySymbol, textAlign: textAlign, readOnly: readOnly);

            string editor = htmlHelper.TextBox(name, value, htmlAttributes: editorAttr).ToString();

            return MvcHtmlString.Create(editor.ToString());
        }

        public static MvcHtmlString BcNumericTextBox(this HtmlHelper htmlHelper, string name, object value = null, object htmlAttributes = null,
            byte? precision = null, NumericType numericType = NumericType.Number, bool nullable = true, string currencySymbol = null,
            TextAlign textAlign = TextAlign.Right, bool readOnly = false)
        {
            RouteValueDictionary dictionary = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            return htmlHelper.BcNumericTextBox(name, value, htmlAttributes: dictionary, nullable: nullable,
                numericType: numericType, precision: precision, currencySymbol: currencySymbol, textAlign: textAlign, readOnly: readOnly);
        }

        public static MvcHtmlString BcTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, object htmlAttributes = null, string format = null, bool readOnly = false,
            bool includeMessageValidation = true, TextAlign textAlign = TextAlign.Left)
        {
            RouteValueDictionary dictionary = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            return htmlHelper.BcTextBoxFor(expression, htmlAttributes: dictionary, format: format, readOnly: readOnly,
                includeMessageValidation: includeMessageValidation, textAlign: textAlign);
        }

        public static MvcHtmlString BcTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes, string format = null, bool readOnly = false,
            bool includeMessageValidation = true, TextAlign textAlign = TextAlign.Left)
        {
            var editorAttr = GetResultHtmlAttributes(htmlAttributes, readOnly);

            string editor = htmlHelper.TextBoxFor(expression, htmlAttributes: editorAttr, format: format).ToString();

            string validationMessage = string.Empty;
            if (includeMessageValidation)
                validationMessage = htmlHelper.BcValidationMessageFor(expression).ToString();

            return MvcHtmlString.Create(editor.ToString() + validationMessage.ToString());
        }

        public static MvcHtmlString BcTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, object htmlAttributes = null, bool readOnly = false,
            bool includeMessageValidation = true, TextAlign textAlign = TextAlign.Left)
        {
            RouteValueDictionary dictionary = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            return htmlHelper.BcAreaBoxFor(expression, htmlAttributes: dictionary, readOnly: readOnly,
                includeMessageValidation: includeMessageValidation, textAlign: textAlign);
        }

        public static MvcHtmlString BcTextArea(this HtmlHelper htmlHelper,
            string name, string value,
            object htmlAttributes = null, bool readOnly = false,
            TextAlign textAlign = TextAlign.Left)
        {
            RouteValueDictionary dictionary = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            return htmlHelper.BcAreaBox(name, value, htmlAttributes: dictionary, readOnly: readOnly, textAlign: textAlign);
        }

        public static MvcHtmlString BcAreaBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes, bool readOnly = false,
            bool includeMessageValidation = true, TextAlign textAlign = TextAlign.Left)
        {
            var editorAttr = GetResultHtmlAttributes(htmlAttributes, readOnly);

            string editor = htmlHelper.TextAreaFor(expression, htmlAttributes: editorAttr).ToString();

            string validationMessage = string.Empty;
            if (includeMessageValidation)
                validationMessage = htmlHelper.BcValidationMessageFor(expression).ToString();

            return MvcHtmlString.Create(editor.ToString() + validationMessage.ToString());
        }

        public static MvcHtmlString BcAreaBox(this HtmlHelper htmlHelper, string name,
            string value, IDictionary<string, object> htmlAttributes, bool readOnly = false,
            TextAlign textAlign = TextAlign.Left)
        {
            var editorAttr = GetResultHtmlAttributes(htmlAttributes, readOnly);

            string editor = htmlHelper.TextArea(name, value, htmlAttributes: editorAttr).ToString();
            return MvcHtmlString.Create(editor.ToString());
        }

        public static MvcHtmlString BcNumericTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, object htmlAttributes = null,
            bool includeMessageValidation = true,
            byte? precision = null, NumericType numericType = NumericType.Number, bool nullable = true, string currencySymbol = null,
            TextAlign textAlign = TextAlign.Right, bool readOnly = false)
        {
            RouteValueDictionary dictionary = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            return htmlHelper.BcNumericTextBoxFor(expression, htmlAttributes: dictionary,
                includeMessageValidation: includeMessageValidation, precision: precision, nullable: nullable, numericType: numericType, currencySymbol: currencySymbol, textAlign: textAlign, readOnly: readOnly);
        }

        public static MvcHtmlString BcNumericTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes,
            bool includeMessageValidation = true, byte? precision = null,
            NumericType numericType = NumericType.Number, bool nullable = true, string currencySymbol = null,
            TextAlign textAlign = TextAlign.Right, bool readOnly = false)
        {
            var editorAttr = GetResultHtmlAttributes(htmlAttributes, isNumeric: true, nullable: nullable,
                numericType: numericType, precision: precision, currencySymbol: currencySymbol, textAlign: textAlign);

            return htmlHelper.BcTextBoxFor(expression, editorAttr, includeMessageValidation: includeMessageValidation, readOnly: readOnly);
        }

        private static IDictionary<string, object> GetResultHtmlAttributes(IDictionary<string, object> htmlAttributes = null,
            bool readOnly = false, bool isNumeric = false, bool nullable = true, TextAlign textAlign = TextAlign.Left,
            NumericType numericType = NumericType.Number, byte? precision = null, string currencySymbol = null)
        {
            var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.ElementClass.TextBoxClass + " " + Constants.Style.InputSizeClass.Default },
                htmlAttributes);

            if (readOnly)
                editorAttr.Add(new KeyValuePair<string, object>("readonly", ""));

            string alignClass = "";
            switch (textAlign)
            {
                case TextAlign.Left:
                    alignClass = "text-left";
                    break;
                case TextAlign.Center:
                    alignClass = "text-center";
                    break;
                case TextAlign.Right:
                    alignClass = "text-right";
                    break;
            }

            if (editorAttr.ContainsKey("class"))
                editorAttr["class"] += " " + alignClass;
            else
                editorAttr.Add("class", alignClass);

            if (isNumeric)
            {
                if (editorAttr.ContainsKey("class"))
                    editorAttr["class"] += " BcNumericInput";
                else
                    editorAttr.Add("class", "BcNumericInput");

                editorAttr.Add("BcNumericInput-Nullable", nullable.ToString().ToLower());

                if (currencySymbol == null)
                    currencySymbol = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;

                if (!precision.HasValue)
                {
                    precision = 2;
                }
                editorAttr.Add("BcNumericInput-DecimalPrecision", precision);

                editorAttr.Add("BcNumericInput-NumericType", numericType.GetID());
                editorAttr.Add("BcNumericInput-CurrencySymbol", currencySymbol);
            }

            if (!editorAttr.ContainsKey("autocomplete"))
                editorAttr.Add("autocomplete", "off");

            return editorAttr;
        }
    }
}
