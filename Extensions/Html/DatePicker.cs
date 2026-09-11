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
    public static class DatePickerExtensions
    {


        //public static MvcHtmlString BcDatePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
        //       Expression<Func<TModel, TValue>> expression, object htmlAttributes = null,
        //       bool includeTime = false, DateTime? minDate = null, DateTime? maxDate = null, bool readOnly = false)
        //{
        //    RouteValueDictionary dictionary = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

        //    return htmlHelper.BcDatePickerTextBoxFor(expression, htmlAttributes: dictionary,
        //           includeMessageValidation: false, includeTime: includeTime, minDate: minDate, maxDate: maxDate, readOnly: readOnly);

        //}


        public static MvcHtmlString BcDatePickerFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, DateTime?>> expression, object htmlAttributes = null,
            bool includeMessageValidation = false,
            bool includeTime = false, DateTime? minDate = null, DateTime? maxDate = null,
            bool readOnly = false)
        {
            Func<TModel, DateTime?> method = expression.Compile();
            DateTime? value = method(htmlHelper.ViewData.Model);

            var editorAttr = GetDateAttr(htmlAttributes, value, includeTime: includeTime, minDate: minDate, maxDate: maxDate, readOnly: readOnly);

            //return htmlHelper.TextBoxFor(expression, editorAttr);


            string editor = htmlHelper.TextBoxFor(expression, editorAttr).ToString();
            editor = Helper.Utils.ReplaceNameAttributes(htmlHelper, expression, editor, editorAttr);


            string validationMessage = string.Empty;

            if (includeMessageValidation)
            {
                validationMessage = htmlHelper.BcValidationMessageFor(expression).ToString();
                validationMessage = Helper.Utils.ReplaceNameAttributes(htmlHelper, expression, validationMessage, editorAttr);
            }

            //return MvcHtmlString.Create(editor.ToString());
            return MvcHtmlString.Create(editor.ToString() + validationMessage.ToString());
        }




        // RBU
        //public static MvcHtmlString BcDatePickerTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
        //  Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes,
        //   bool includeMessageValidation = true, bool includeTime = false,
        //   DateTime? minDate = null, DateTime? maxDate = null, bool readOnly = false)
        //{
        //    var editorAttr = GetResultHtmlAttributes(htmlAttributes, includeTime: includeTime, minDate: minDate, maxDate: maxDate, readOnly: readOnly);

        //    return htmlHelper.BcTextBoxFor(expression, editorAttr, includeMessageValidation: includeMessageValidation, readOnly: readOnly);
        //}


        //private static IDictionary<string, object> GetResultHtmlAttributes(IDictionary<string, object> htmlAttributes = null,
        //    bool includeTime = false, DateTime? minDate = null, DateTime? maxDate = null, bool readOnly = false
        //   )
        //{
        //    //var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
        //    //    new { @class = Constants.Style.ElementClass.TextBoxClass + " " + Constants.Style.InputSizeClass.Default },
        //    //    htmlAttributes);

        //    string inputFormat = Bc.Web.Mvc.Html.Constants.DateFormat.InputDateFormat;
        //    string serverFormat = Bc.Web.Mvc.Html.Constants.DateFormat.ServerInputDateFormat;

        //    if (includeTime)
        //    {
        //        inputFormat = Bc.Web.Mvc.Html.Constants.DateFormat.InputDateTimeFormat;
        //        serverFormat = Bc.Web.Mvc.Html.Constants.DateFormat.ServerInputDateTimeFormat;
        //    }


        //    var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
        //              new
        //              {
        //                  // Bc_data_date = textValue,
        //                  use_time = includeTime.ToString().ToLower(),
        //                  //data_date = textValue,
        //                  //  value = textValue,
        //                  // value = "2021-01-01",
        //                  //date_date_format = inputFormat,
        //                  type = "date",
        //                  @class = Bc.Web.Mvc.Html.Constants.Style.ElementClass.DatePickerClass,

        //              },
        //              htmlAttributes);


        //    if (readOnly)
        //        editorAttr.Add(new KeyValuePair<string, object>("readonly", ""));

        //    //  editorAttr.Add(new KeyValuePair<string, object>("type", "datetime"));

        //    if (!editorAttr.ContainsKey("autocomplete"))
        //        editorAttr.Add("autocomplete", "off");

        //    if (minDate.HasValue)
        //    {
        //        editorAttr.Add("min-Date", string.Format("{0:" + serverFormat + "}", minDate));
        //    }

        //    if (maxDate.HasValue)
        //    {
        //        editorAttr.Add("max-Date", string.Format("{0:" + serverFormat + "}", maxDate));
        //    }

        //    return editorAttr;
        //}

        private static IDictionary<string, object> GetDateAttr(object htmlAttributes,
            DateTime? value, bool includeTime = false, DateTime? minDate = null, DateTime? maxDate = null, bool readOnly = false)
        {

            string inputFormat = Bc.Web.Mvc.Html.Constants.DateFormat.InputDateFormat;
            string serverFormat = Bc.Web.Mvc.Html.Constants.DateFormat.ServerInputDateFormat;
            string classFormat = Bc.Web.Mvc.Html.Constants.Style.ElementClass.DatePickerClass;

            if (includeTime)
            {
                inputFormat = Bc.Web.Mvc.Html.Constants.DateFormat.InputDateTimeFormat;
                serverFormat = Bc.Web.Mvc.Html.Constants.DateFormat.ServerInputDateTimeFormat;
                classFormat = Bc.Web.Mvc.Html.Constants.Style.ElementClass.DateTimePickerClass;
            }

            string textValue = string.Format("{0:" + serverFormat + "}", value);

            var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                        new
                        {
                            //rp3_data_date = textValue,
                            bc_data_date = textValue,
                            //use_time = false,
                            use_time = includeTime.ToString().ToLower(),
                            data_date = textValue,
                            value = textValue,
                            date_date_format = inputFormat,
                            @class = classFormat
                        },
                        htmlAttributes);

            if (minDate.HasValue)
            {
                editorAttr.Add("min-Date", string.Format("{0:" + serverFormat + "}", minDate));
            }

            if (maxDate.HasValue)
            {
                editorAttr.Add("max-Date", string.Format("{0:" + serverFormat + "}", maxDate));
            }

            if (readOnly)
                editorAttr.Add("readonly", "");
            //editorAttr.Add(new KeyValuePair<string, object>("readonly", ""));

            return editorAttr;
        }



        private static readonly Func<string, TagBuilder> InputDiv = (a) =>
        {
            var div = new TagBuilder("div");
            div.AddCssClass(a);
            return div;
        };

        private static readonly Func<TagBuilder> FormGroup = () =>
        {
            var div = new TagBuilder("div");
            div.AddCssClass("form-group");
            return div;
        };
        public static MvcHtmlString DatePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string labelClass = "col-md-2")
        {
            var label = htmlHelper.LabelFor(expression, new { @class = "control-label col-md-2" }).ToHtmlString();

            var inputDiv = InputDiv(labelClass);

            var input = htmlHelper.TextBoxFor(expression, "{0:dd/MM/yyyy}",
                new
                {
                    maxLength = "10",
                    @class = "datepicker form-control"
                }).ToHtmlString();

            inputDiv.InnerHtml = input;

            var formGroupDiv = FormGroup();
            formGroupDiv.InnerHtml = label + inputDiv;

            return MvcHtmlString.Create(formGroupDiv.ToString());
        }


        //private static IDictionary<string, object> GetDateAttr(object htmlAttributes,
        //    DateTime? value, bool includeTime = false, DateTime? minDate = null, DateTime? maxDate = null)
        //{
        //    string inputFormat = Bc.Web.Mvc.Html.Constants.DateFormat.InputDateFormat;
        //    string serverFormat = Bc.Web.Mvc.Html.Constants.DateFormat.ServerInputDateFormat;

        //    if (includeTime)
        //    {
        //        inputFormat = Bc.Web.Mvc.Html.Constants.DateFormat.InputDateTimeFormat;
        //        serverFormat = Bc.Web.Mvc.Html.Constants.DateFormat.ServerInputDateTimeFormat;
        //    }

        //    string textValue = string.Format("{0:" + serverFormat + "}", value);

        //    var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
        //                new
        //                {
        //                    Bc_data_date = textValue,
        //                    use_time = includeTime.ToString().ToLower(),
        //                    //data_date = textValue,
        //                    value = textValue,
        //                    date_date_format = inputFormat,
        //                    @class = Bc.Web.Mvc.Html.Constants.Style.ElementClass.DatePickerClass
        //                },
        //                htmlAttributes);

        //    if (minDate.HasValue)
        //    {
        //        editorAttr.Add("min-Date", string.Format("{0:" + serverFormat + "}", minDate));
        //    }

        //    if (maxDate.HasValue)
        //    {
        //        editorAttr.Add("max-Date", string.Format("{0:" + serverFormat + "}", maxDate));
        //    }

        //    return editorAttr;
        //}

        public static MvcHtmlString BcDatePicker(this HtmlHelper htmlHelper, string name, DateTime value,
           object htmlAttributes = null, bool includeTime = false, DateTime? minDate = null, DateTime? maxDate = null,
           bool readOnly = false)
        {
            var editorAttr = GetDateAttr(htmlAttributes, value, includeTime, minDate, maxDate, readOnly: readOnly);
            string textValue = Convert.ToString(editorAttr["value"]);

            return htmlHelper.TextBox(name, textValue, editorAttr);
        }

        public static MvcHtmlString BcDatePicker(this HtmlHelper htmlHelper, string name, DateTime? value,
           object htmlAttributes = null, bool includeTime = false, DateTime? minDate = null, DateTime? maxDate = null, bool readOnly = false)
        {
            var editorAttr = GetDateAttr(htmlAttributes, value, includeTime, minDate, maxDate: maxDate, readOnly: readOnly);
            string textValue = Convert.ToString(editorAttr["value"]);

            return htmlHelper.TextBox(name, textValue, editorAttr);
        }


        public static MvcHtmlString BcDateRangePicker(this HtmlHelper htmlHelper, string name, object htmlAttributes = null)
        {
            //var editorAttr = GetDateAttr(htmlAttributes, value, includeTime, minDate, maxDate: maxDate, readOnly: readOnly);
            //string textValue = Convert.ToString(editorAttr["value"]);

            string classFormat = Bc.Web.Mvc.Html.Constants.Style.ElementClass.DateRangePickerClass;

            var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                      new
                      {
                          @class = classFormat
                      },
                      htmlAttributes);
            editorAttr.Add("bcType", "DateRange");

            return htmlHelper.TextBox(name, null, htmlAttributes: editorAttr);
        }
    }
}