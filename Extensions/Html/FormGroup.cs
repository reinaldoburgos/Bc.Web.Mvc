using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public static class FormGroupExtensions
    {
        private static MvcHtmlString FormGroupEditor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string editor, object labelHtmlAttributes = null, string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4,
            bool labelTextUp = Constants.DefaultLabelPosition, object formGroupHtmlAttributes = null)
        {
            //int labelColSize = GetLabelColSize(labelSize); ;
            int labelColSize = labelTextUp ? 0 : GetLabelColSize(labelSize);
            int inputColSize = 12 - labelColSize;

            var labelAttr =
                Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(new { @class = "col-sm-" + labelColSize.ToString() },
                labelHtmlAttributes);

            MvcHtmlString label = htmlHelper.BcLabelFor(expression, htmlAttributes: labelAttr, labelText: labelText);

            TagBuilder mainBuilder = new TagBuilder("div");
            mainBuilder.AddCssClass(Bc.Web.Mvc.Html.Constants.Style.ContentClass.FormGroupClass);

            if (formGroupHtmlAttributes != null)
            {
                // Convertir htmlAttributes a un diccionario
                var attributes = new RouteValueDictionary(formGroupHtmlAttributes);

                // Añadir estos atributos al TagBuilder
                foreach (KeyValuePair<string, object> attribute in attributes)
                {
                    mainBuilder.MergeAttribute(attribute.Key, attribute.Value.ToString(), true);
                }
            }


            TagBuilder innerControl = new TagBuilder("div");
            innerControl.AddCssClass(Constants.Style.ContentClass.InputControlClass);
            innerControl.AddCssClass("col-sm-" + inputColSize.ToString());

            if (labelTextUp)
            {
                innerControl.InnerHtml = label.ToString() + editor;
                mainBuilder.InnerHtml = innerControl.ToString();
            }
            else
            {
                innerControl.InnerHtml = editor;
                mainBuilder.InnerHtml = label.ToString() + innerControl.ToString();
            }
            return MvcHtmlString.Create(mainBuilder.ToString());
        }

        public static MvcContent BcBeginFormGroupEditor(this HtmlHelper htmlHelper, string labelText, object labelHtmlAttributes = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool labelTextUp = Constants.DefaultLabelPosition)
        {
            return new MvcContent(
                () => htmlHelper.BeginFormGroupEditor(labelText, labelHtmlAttributes, labelSize, labelTextUp),
                () => htmlHelper.EndFormGroupEditor()
            );
        }

        private static void EndFormGroupEditor(this HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write(MvcHtmlString.Create("</div></div>"));
        }

        internal static int GetLabelColSize(HtmlColumnSize labelSize)
        {
            int labelColSize = 4;

            switch (labelSize)
            {
                case HtmlColumnSize.Size_1:
                    labelColSize = 1;
                    break;
                case HtmlColumnSize.Size_2:
                    labelColSize = 2;
                    break;
                case HtmlColumnSize.Size_3:
                    labelColSize = 3;
                    break;
                case HtmlColumnSize.Size_4:
                    labelColSize = 4;
                    break;
                case HtmlColumnSize.Size_5:
                    labelColSize = 5;
                    break;
                case HtmlColumnSize.Size_6:
                    labelColSize = 6;
                    break;
                case HtmlColumnSize.Size_7:
                    labelColSize = 7;
                    break;
                case HtmlColumnSize.Size_8:
                    labelColSize = 8;
                    break;
                case HtmlColumnSize.Size_9:
                    labelColSize = 9;
                    break;
                case HtmlColumnSize.Size_10:
                    labelColSize = 10;
                    break;
                case HtmlColumnSize.Size_11:
                    labelColSize = 11;
                    break;
                case HtmlColumnSize.Size_12:
                    labelColSize = 12;
                    break;
            }

            return labelColSize;
        }

        private static void BeginFormGroupEditor(this HtmlHelper htmlHelper, string labelText, object labelHtmlAttributes = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool labelTextUp = Constants.DefaultLabelPosition)
        {
            //int labelColSize = GetLabelColSize(labelSize);
            int labelColSize = labelTextUp ? 0 : GetLabelColSize(labelSize);
            int inputColSize = 12 - labelColSize;

            var labelAttr =
                Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(new { @class = "col-sm-" + labelColSize.ToString() },
                labelHtmlAttributes);

            MvcHtmlString label = htmlHelper.BcLabel(htmlAttributes: labelAttr, labelText: labelText);

            StringBuilder mainBuilder = new StringBuilder(string.Format("<div class=\"{0}\">", Bc.Web.Mvc.Html.Constants.Style.ContentClass.FormGroupClass));

            StringBuilder subBuilder = new StringBuilder($"<div class=\"{Bc.Web.Mvc.Html.Constants.Style.ContentClass.InputControlClass} {"col-sm-" + inputColSize.ToString()}\"> ");

            if (labelTextUp)
            {
                subBuilder.Append(label.ToString());
                mainBuilder.Append(subBuilder.ToString());
            }
            else
            {
                mainBuilder.Append(label.ToString());
            }

            mainBuilder.AppendFormat("<div class=\"{0} {1}\">",
                Constants.Style.ContentClass.InputControlClass,
                "col-sm-" + inputColSize.ToString());

            htmlHelper.ViewContext.Writer.Write(MvcHtmlString.Create(mainBuilder.ToString()));
        }

        private static MvcHtmlString FormGroupEditor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, DateTime>> expression,
            string editor, object labelHtmlAttributes = null, string labelText = null)
        {
            return htmlHelper.FormGroupEditor(expression, editor, labelHtmlAttributes, labelText);
        }

        internal static MvcHtmlString FormGroupEditor(this HtmlHelper htmlHelper,
            string editor, object labelHtmlAttributes = null, string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool labelTextUp = false)
        {
            int labelColSize = labelTextUp ? 0 : GetLabelColSize(labelSize); ;
            int inputColSize = 12 - labelColSize;

            var labelAttr =
                Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(new { @class = "col-sm-" + labelColSize.ToString() },
                labelHtmlAttributes);

            MvcHtmlString label = htmlHelper.BcLabel(htmlAttributes: labelAttr, labelText: labelText);

            TagBuilder mainBuilder = new TagBuilder("div");
            mainBuilder.AddCssClass(Bc.Web.Mvc.Html.Constants.Style.ContentClass.FormGroupClass);

            string left = label.ToString();

            if (string.IsNullOrEmpty(labelText))
            {
                TagBuilder labelDiv = new TagBuilder("div");
                labelDiv.AddCssClass("col-sm-" + labelColSize.ToString());
                labelDiv.InnerHtml = label.ToString();

                left = labelDiv.ToString();
            }
            TagBuilder innerControl = new TagBuilder("div");
            innerControl.AddCssClass(Constants.Style.ContentClass.InputControlClass);
            innerControl.AddCssClass("col-sm-" + inputColSize.ToString());

            if (labelTextUp)
            {
                innerControl.InnerHtml = left.ToString() + editor;
                mainBuilder.InnerHtml = innerControl.ToString();
            }
            else
            {
                innerControl.InnerHtml = editor;
                mainBuilder.InnerHtml = left.ToString() + innerControl.ToString();
            }
            return MvcHtmlString.Create(mainBuilder.ToString());
        }

        public static MvcHtmlString BcFormGroupText(this HtmlHelper htmlHelper, string name, string value = "", object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string editorValueFormat = null, string labelText = null, bool readOnly = false, HtmlColumnSize labelSize = HtmlColumnSize.Size_4,
            bool labelTextUp = Constants.DefaultLabelPosition)
        {
            MvcHtmlString editor = htmlHelper.BcTextBox(name, value, htmlAttributes: editorHtmlAttributes, format: editorValueFormat, readOnly: readOnly);

            return htmlHelper.FormGroupEditor(editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        }



        public static MvcHtmlString BcFormGroupTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string editorValueFormat = null, string labelText = null, bool readOnly = false, HtmlColumnSize labelSize = HtmlColumnSize.Size_4,
            bool labelTextUp = Constants.DefaultLabelPosition, object formGroupHtmlAttributes = null, bool toUpperCase = false)
        {
            MvcHtmlString editor = htmlHelper.BcTextBoxFor(expression, htmlAttributes: editorHtmlAttributes, format: editorValueFormat, readOnly: readOnly, toUpperCase: toUpperCase);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp, formGroupHtmlAttributes: formGroupHtmlAttributes);
        }

        public static MvcHtmlString BcFormGroupNumericTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
           byte precision = 0, NumericType numericType = NumericType.Number, bool nullable = true, string currencySymbol = null,
           TextAlign textAlign = TextAlign.Right, string labelText = null,
           HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool readOnly = false, bool labelTextUp = Constants.DefaultLabelPosition)
        {
            MvcHtmlString editor = htmlHelper.BcNumericTextBoxFor(expression, htmlAttributes: editorHtmlAttributes,
                precision: precision, numericType: numericType, nullable: nullable, currencySymbol: currencySymbol, textAlign: textAlign, readOnly: readOnly);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        }


        public static MvcHtmlString BcFormGroupTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, bool readOnly = false, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool labelTextUp = Constants.DefaultLabelPosition)
        {
            MvcHtmlString editor = htmlHelper.BcTextAreaFor(expression, htmlAttributes: editorHtmlAttributes, readOnly: readOnly);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        }

        public static MvcHtmlString BcFormGroupTextArea(this HtmlHelper htmlHelper, string name, string value = "", object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, bool readOnly = false, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool labelTextUp = Constants.DefaultLabelPosition)
        {
            MvcHtmlString editor = htmlHelper.BcTextArea(name, value, htmlAttributes: editorHtmlAttributes, readOnly: readOnly);

            return htmlHelper.FormGroupEditor(editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        }


        public static MvcHtmlString BcFormGroupListBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4)
        {
            MvcHtmlString editor = htmlHelper.BcListBoxFor(expression, selectList, htmlAttributes: editorHtmlAttributes);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        }

        //public static MvcHtmlString BcFormGroupListBoxGeneralValuesFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
        //   Expression<Func<TModel, TValue>> expression, short id,
        //   object labelHtmlAttributes = null, object editorHtmlAttributes = null,
        //   string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, Func<Bc.Web.Mvc.Helper.GeneralValues, object> sortexpression = null)
        ////string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, Func<Bc.Web.Mvc.Helper.GeneralValues, object> sortexpression = null)
        //{
        //    MvcHtmlString editor = htmlHelper.BcListBoxGeneralValuesFor(expression, id, htmlAttributes: editorHtmlAttributes, sortexpression: sortexpression);

        //    return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        //}


        public static MvcHtmlString BcFormGroupDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4,
            bool labelTextUp = Constants.DefaultLabelPosition, bool useAjax = false)
        {
            MvcHtmlString editor = htmlHelper.BcDropDownListFor(expression, selectList, htmlAttributes: editorHtmlAttributes, useAjax: useAjax);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        }

        public static MvcHtmlString BcFormGroupDropDownGroupListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<GroupedSelectListItem> selectList,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool labelTextUp = Constants.DefaultLabelPosition)
        {
            MvcHtmlString editor = htmlHelper.BcDropDownGroupListFor(expression, selectList, htmlAttributes: editorHtmlAttributes);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        }

        //public static MvcHtmlString BcFormGroupDropDownListIdentificationTypeFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
        //    Expression<Func<TModel, TValue>> expression,
        //    object labelHtmlAttributes = null, object editorHtmlAttributes = null,
        //    string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool usePlaceHolder = false)
        //{
        //    MvcHtmlString editor = htmlHelper.BcDropDownListIdentificationTypeFor(expression, htmlAttributes: editorHtmlAttributes, usePlaceHolder: usePlaceHolder);

        //    return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        //}

        //public static MvcHtmlString BcFormGroupDropDownGeneralValuesFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
        //    Expression<Func<TModel, TValue>> expression, short id,
        //    object labelHtmlAttributes = null, object editorHtmlAttributes = null,
        //    string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, Func<Bc.Web.Mvc.Helper.GeneralValues, object> sortexpression = null)
        //{
        //    MvcHtmlString editor = htmlHelper.BcDropDownListGeneralValuesFor(expression, id, htmlAttributes: editorHtmlAttributes, sortexpression: sortexpression);

        //    return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        //}

        //todol: importante
        //public static MvcHtmlString BcFormGroupDropDownGeneralValues(this HtmlHelper htmlHelper, string name, short id,
        //    object labelHtmlAttributes = null, object editorHtmlAttributes = null,
        //    string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, Func<Bc.Web.Mvc.Helper.GeneralValues, object> sortexpression = null)
        //{
        //    MvcHtmlString editor = htmlHelper.BcDropDownListGeneralValues(name, id, htmlAttributes: editorHtmlAttributes, sortexpression: sortexpression);

        //    return htmlHelper.FormGroupEditor(editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        //}


        // public static MvcHtmlString BcFormGroupDatePickerFor<TModel>(this HtmlHelper<TModel> htmlHelper,
        //Expression<Func<TModel, DateTime?>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
        // string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool includeTime = false, bool labelTextUp = false)

        //public static MvcHtmlString BcFormGroupDatePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
        //   Expression<Func<TModel, TValue>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
        //    string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool includeTime = false, bool labelTextUp = false)
        //{
        //    MvcHtmlString editor = htmlHelper.BcDatePickerFor(expression, editorHtmlAttributes, includeTime: includeTime);

        //    return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        //}

        public static MvcHtmlString BcFormGroupDatePickerFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, DateTime?>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            bool includeMessageValidation = false,
            string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool includeTime = false, bool labelTextUp = Constants.DefaultLabelPosition,
            bool readOnly = false)
        {
            MvcHtmlString editor = htmlHelper.BcDatePickerFor(expression, editorHtmlAttributes, includeMessageValidation: includeMessageValidation, includeTime: includeTime, readOnly: readOnly);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        }

        public static MvcHtmlString BcFormGroupDatePicker(this HtmlHelper htmlHelper, string name, DateTime value, string labelText,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool includeTime = false, bool labelTextUp = Constants.DefaultLabelPosition, bool readOnly = false)
        {
            MvcHtmlString editor = htmlHelper.BcDatePicker(name, value, includeTime: includeTime, readOnly: readOnly);

            return htmlHelper.FormGroupEditor(editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        }

        public static MvcHtmlString BcFormGroupDatePicker(this HtmlHelper htmlHelper, string name, DateTime? value, string labelText,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool includeTime = false, bool readOnly = false)
        {
            MvcHtmlString editor = htmlHelper.BcDatePicker(name, value, includeTime: includeTime, readOnly: readOnly);

            return htmlHelper.FormGroupEditor(editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        }

        public static MvcHtmlString BcFormGroupDateRangePicker(this HtmlHelper htmlHelper, string name, string labelText,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool labelTextUp = Constants.DefaultLabelPosition)
        {
            MvcHtmlString editor = htmlHelper.BcDateRangePicker(name, editorHtmlAttributes);

            return htmlHelper.FormGroupEditor(editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        }



        #region DatePicker DexExtreme

        public static MvcHtmlString BcFormGroupDatePickerDevExFor<TModel>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, DateTime?>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
           bool includeMessageValidation = false,
           string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool includeTime = false, bool labelTextUp = Constants.DefaultLabelPosition,
           bool readOnly = false)
        {
            MvcHtmlString editor = htmlHelper.BcDatePickerDevExFor(expression, editorHtmlAttributes, includeMessageValidation: includeMessageValidation, includeTime: includeTime, readOnly: readOnly);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        }

        public static MvcHtmlString BcFormGroupDatePickerDevEx(this HtmlHelper htmlHelper, string name, DateTime value, string labelText,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool includeTime = false, bool labelTextUp = Constants.DefaultLabelPosition, bool readOnly = false)
        {
            MvcHtmlString editor = htmlHelper.BcDatePickerDevEx(name, value, includeTime: includeTime, readOnly: readOnly);

            return htmlHelper.FormGroupEditor(editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        }

        public static MvcHtmlString BcFormGroupDatePickerDevEx(this HtmlHelper htmlHelper, string name, DateTime? value, string labelText,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool includeTime = false, bool readOnly = false)
        {
            MvcHtmlString editor = htmlHelper.BcDatePickerDevEx(name, value, includeTime: includeTime, readOnly: readOnly);

            return htmlHelper.FormGroupEditor(editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        }


        #endregion DatePicker DexExtreme


        public static MvcHtmlString BcFormGroupDropDownList(this HtmlHelper htmlHelper, IEnumerable<SelectListItem> selectList,
            string labelText, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string name = "", HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool labelTextUp = Constants.DefaultLabelPosition,
            bool useAjax = false)
        {
            MvcHtmlString editor = htmlHelper.BcDropDownList(selectList, htmlAttributes: editorHtmlAttributes, name: name, useAjax: useAjax);

            return htmlHelper.FormGroupEditor(editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        }

        public static MvcHtmlString BcFormGroupDropDownGroupList(this HtmlHelper htmlHelper, IEnumerable<GroupedSelectListItem> selectList,
            string labelText, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string name = "", HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool labelTextUp = Constants.DefaultLabelPosition)
        {
            MvcHtmlString editor = htmlHelper.BcDropDownGroupList(selectList, htmlAttributes: editorHtmlAttributes, name: name);

            return htmlHelper.FormGroupEditor(editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        }


        public static MvcHtmlString BcFormGroupPasswordFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null, string labelText = null,
            HtmlColumnSize labelSize = HtmlColumnSize.Size_4)
        {
            MvcHtmlString editor = htmlHelper.BcPasswordFor(expression, htmlAttributes: editorHtmlAttributes);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        }

        public static MvcHtmlString BcFormGroupPassword(this HtmlHelper htmlHelper, string name, string value = "", object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4)
        {
            MvcHtmlString editor = htmlHelper.BcPassword(name, value, htmlAttributes: editorHtmlAttributes);

            return htmlHelper.FormGroupEditor(editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        }

        public static MvcHtmlString BcFormGroupCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked = false, object value = null, object labelHtmlAttributes = null, object editorHtmlAttributes = null, string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool readOnly = false)
        {
            MvcHtmlString editor = htmlHelper.BcCheckBox(name, isChecked, value, htmlAttributes: editorHtmlAttributes, readOnly: readOnly);

            return htmlHelper.FormGroupEditor(editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        }

        public static MvcHtmlString BcFormGroupCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null, string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool readOnly = false, bool labelTextUp = false,
            object formGroupHtmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcCheckBoxFor(expression, htmlAttributes: editorHtmlAttributes, readOnly: readOnly);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp, formGroupHtmlAttributes: formGroupHtmlAttributes);
        }


        //TODO: Solo comentario, Control Toogle
        public static MvcHtmlString BcFormGroupCheckBoxToggleFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression,
            Icons dataOnIcon = Icons.Check, Icons dataOffIcon = Icons.Cancel,
            string dataOn = "Si", ElementThemeType dataOnthemeType = ElementThemeType.Primary,
            string dataOff = "No", ElementThemeType dataOffthemeType = ElementThemeType.Default,
            int? width = null,
            string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, object labelHtmlAttributes = null, bool labelTextUp = false,
            object editorHtmlAttributes = null, bool readOnly = false)
        {
            MvcHtmlString editor = htmlHelper.BcCheckBoxToggleFor(expression, dataOnIcon: dataOnIcon, dataOffIcon: dataOffIcon,
                dataOn: dataOn, dataOnthemeType: dataOnthemeType,
                dataOff: dataOff, dataOffthemeType: dataOffthemeType,
                width: width,
                labelText: labelText, labelSize: labelSize, labelHtmlAttributes: labelHtmlAttributes, labelTextUp: labelTextUp,
                htmlAttributes: editorHtmlAttributes, readOnly: readOnly);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        }



        public static MvcHtmlString BcFormGroupRadioButtonFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object value, object labelHtmlAttributes = null, object editorHtmlAttributes = null, string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4)
        {
            MvcHtmlString editor = htmlHelper.BcRadioButtonFor(expression, value, htmlAttributes: editorHtmlAttributes);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        }

        public static MvcHtmlString BcFormGroupRadioButtonGroupFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, SelectList selectList, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, string name = null, bool labelTextUp = Constants.DefaultLabelPosition)
        {
            MvcHtmlString editor = htmlHelper.BcRadioButtonGroupFor(expression, selectList, htmlAttributes: editorHtmlAttributes, name: name);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp: labelTextUp);
        }

        //public static MvcHtmlString BcFormGroupRadioButtonGroupGeneralValuesFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
        //    Expression<Func<TModel, TValue>> expression, short id, object labelHtmlAttributes = null, object editorHtmlAttributes = null, string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4)
        //{
        //    MvcHtmlString editor = htmlHelper.BcRadioButtonGroupGeneralValuesFor(expression, id, htmlAttributes: editorHtmlAttributes);

        //    return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        //}


        public static MvcHtmlString BcFormGroupDisplayFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object labelHtmlAttributes = null, object displayHtmlAttributes = null, string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, bool labelTextUp = Constants.DefaultLabelPosition)
        {
            MvcHtmlString editor = htmlHelper.BcDisplayFor(expression, htmlAttributes: displayHtmlAttributes);

            return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize, labelTextUp);
        }

        //public static MvcHtmlString BcFormGroupDisplayFor<TModel>(this HtmlHelper<TModel> htmlHelper,
        //    Expression<Func<TModel, Boolean>> expression, object labelHtmlAttributes = null, object displayHtmlAttributes = null, string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4)
        //{
        //    MvcHtmlString editor = htmlHelper.BcDisableCheckBoxFor(expression, htmlAttributes: displayHtmlAttributes);

        //    return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        //}

        //public static MvcHtmlString BcFormGroupDisplayFor<TModel>(this HtmlHelper<TModel> htmlHelper,
        //    Expression<Func<TModel, DateTime?>> expression, object labelHtmlAttributes = null, object displayHtmlAttributes = null, string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4)
        //{
        //    Func<TModel, DateTime?> method = expression.Compile();
        //    DateTime? value = method(htmlHelper.ViewData.Model);

        //    var expression2 = (MemberExpression)expression.Body;
        //    string name = expression2.Member.Name;


        //    string textValue = "";
        //    if (value.HasValue)
        //    {
        //        textValue = value.Value.Date == value.Value ? value.Value.ToDefaultDateFormat() : value.Value.ToDefaultDateTimeFormat();
        //    }

        //    MvcHtmlString editor = htmlHelper.BcTextBox(name, textValue, htmlAttributes: displayHtmlAttributes, readOnly: true);

        //    return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        //}

        //public static MvcHtmlString BcFormGroupDisplayFor<TModel>(this HtmlHelper<TModel> htmlHelper,
        //    Expression<Func<TModel, DateTime>> expression, object labelHtmlAttributes = null, object displayHtmlAttributes = null, string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4)
        //{
        //    Func<TModel, DateTime> method = expression.Compile();
        //    DateTime? value = method(htmlHelper.ViewData.Model);

        //    var expression2 = (MemberExpression)expression.Body;
        //    string name = expression2.Member.Name;

        //    string textValue = "";

        //    textValue = value.Value.Date == value.Value ? value.Value.ToDefaultDateFormat() : value.Value.ToDefaultDateTimeFormat();

        //    MvcHtmlString editor = htmlHelper.BcTextBox(name, textValue, htmlAttributes: displayHtmlAttributes, readOnly: true);

        //    return htmlHelper.FormGroupEditor(expression, editor.ToString(), labelHtmlAttributes: labelHtmlAttributes, labelText: labelText, labelSize: labelSize);
        //}
    }
}
