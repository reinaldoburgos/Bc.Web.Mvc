using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    /// <summary>
    /// Anchos del campo en la grilla de 12 columnas (equivalente a HtmlColumnSize, sin col-sm-N).
    /// </summary>
    public static class FieldSpan
    {
        public const int Sixth = 2;
        public const int Third = 4;
        public const int Half = 6;
        public const int Full = 12;
    }

    public static class FieldGroupExtensions
    {
        private static int ClampSpan(int span)
        {
            if (span < 1) return FieldSpan.Full;
            if (span > 12) return FieldSpan.Full;
            return span;
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

        private static TagBuilder BuildFieldWrapper(int span, bool readOnly, bool labelTextUp, object htmlAttributes)
        {
            TagBuilder wrap = new TagBuilder("div");
            wrap.AddCssClass(Constants.Style.ContentClass.FieldClass);
            wrap.AddCssClass(Constants.Style.ContentClass.FieldSpanPrefix + ClampSpan(span).ToString());
            if (readOnly)
            {
                wrap.AddCssClass(Constants.Style.ContentClass.FieldReadOnlyClass);
            }
            if (!labelTextUp)
            {
                wrap.AddCssClass(Constants.Style.ContentClass.FieldInlineClass);
            }
            ApplyHtmlAttributes(wrap, htmlAttributes);
            return wrap;
        }

        private static MvcHtmlString FieldGroupEditor(MvcHtmlString label, string editor, bool readOnly, bool labelTextUp, int span, object htmlAttributes)
        {
            TagBuilder wrap = BuildFieldWrapper(span, readOnly, labelTextUp, htmlAttributes);

            TagBuilder control = new TagBuilder("div");
            control.AddCssClass(Constants.Style.ContentClass.FieldControlClass);
            control.InnerHtml = editor ?? string.Empty;

            wrap.InnerHtml = (label != null ? label.ToString() : string.Empty) + control.ToString();
            return MvcHtmlString.Create(wrap.ToString());
        }

        private static MvcHtmlString FieldGroupEditor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string editor,
            object labelHtmlAttributes = null,
            string labelText = null,
            bool readOnly = false,
            bool labelTextUp = Constants.DefaultLabelPosition,
            int span = FieldSpan.Full,
            object htmlAttributes = null)
        {
            MvcHtmlString label = htmlHelper.BcLabelFor(expression, htmlAttributes: labelHtmlAttributes, labelText: labelText);
            return FieldGroupEditor(label, editor, readOnly, labelTextUp, span, htmlAttributes);
        }

        private static MvcHtmlString FieldGroupEditor(this HtmlHelper htmlHelper,
            string editor,
            object labelHtmlAttributes = null,
            string labelText = null,
            bool readOnly = false,
            bool labelTextUp = Constants.DefaultLabelPosition,
            int span = FieldSpan.Full,
            object htmlAttributes = null)
        {
            MvcHtmlString label = htmlHelper.BcLabel(htmlAttributes: labelHtmlAttributes, labelText: labelText);
            return FieldGroupEditor(label, editor, readOnly, labelTextUp, span, htmlAttributes);
        }

        public static MvcContent BcBeginFieldGrid(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            IDictionary<string, object> resultHtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.ContentClass.FieldGridClass },
                htmlAttributes);

            return new MvcContent(
                () => htmlHelper.BcBeginContent(htmlAttributes: resultHtmlAttributes),
                () => htmlHelper.BcEndContent()
            );
        }

        public static MvcContent BcBeginFieldGroup(this HtmlHelper htmlHelper, string labelText,
            int span = FieldSpan.Full,
            bool labelTextUp = Constants.DefaultLabelPosition,
            bool readOnly = false,
            object labelHtmlAttributes = null,
            object htmlAttributes = null)
        {
            return new MvcContent(
                () => htmlHelper.BeginFieldGroup(labelText, span, labelTextUp, readOnly, labelHtmlAttributes, htmlAttributes),
                () => htmlHelper.EndFieldGroup()
            );
        }

        private static void BeginFieldGroup(this HtmlHelper htmlHelper, string labelText, int span, bool labelTextUp, bool readOnly, object labelHtmlAttributes, object htmlAttributes)
        {
            TagBuilder wrap = BuildFieldWrapper(span, readOnly, labelTextUp, htmlAttributes);
            string open = wrap.ToString();
            int closeAt = open.IndexOf("</div>", StringComparison.Ordinal);
            if (closeAt >= 0)
            {
                open = open.Remove(closeAt);
            }

            MvcHtmlString label = htmlHelper.BcLabel(htmlAttributes: labelHtmlAttributes, labelText: labelText);
            htmlHelper.ViewContext.Writer.Write(open);
            htmlHelper.ViewContext.Writer.Write(label);
            htmlHelper.ViewContext.Writer.Write("<div class=\"" + Constants.Style.ContentClass.FieldControlClass + "\">");
        }

        private static void EndFieldGroup(this HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("</div></div>");
        }

        public static MvcHtmlString BcFieldGroupText(this HtmlHelper htmlHelper, string name, string value = "",
            object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string editorValueFormat = null, string labelText = null, bool readOnly = false,
            bool labelTextUp = Constants.DefaultLabelPosition, int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcTextBox(name, value, htmlAttributes: editorHtmlAttributes, format: editorValueFormat, readOnly: readOnly);
            return htmlHelper.FieldGroupEditor(editor.ToString(), labelHtmlAttributes, labelText, readOnly, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string editorValueFormat = null, string labelText = null, bool readOnly = false,
            bool labelTextUp = Constants.DefaultLabelPosition, object htmlAttributes = null, bool toUpperCase = false,
            int span = FieldSpan.Full)
        {
            MvcHtmlString editor = htmlHelper.BcTextBoxFor(expression, htmlAttributes: editorHtmlAttributes, format: editorValueFormat, readOnly: readOnly, toUpperCase: toUpperCase);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText, readOnly, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupNumericTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            byte precision = 0, NumericType numericType = NumericType.Number, bool nullable = true, string currencySymbol = null,
            TextAlign textAlign = TextAlign.Right, string labelText = null, bool readOnly = false,
            bool labelTextUp = Constants.DefaultLabelPosition, int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcNumericTextBoxFor(expression, htmlAttributes: editorHtmlAttributes,
                precision: precision, numericType: numericType, nullable: nullable, currencySymbol: currencySymbol, textAlign: textAlign, readOnly: readOnly);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText, readOnly, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, bool readOnly = false, bool labelTextUp = Constants.DefaultLabelPosition,
            int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcTextAreaFor(expression, htmlAttributes: editorHtmlAttributes, readOnly: readOnly);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText, readOnly, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupTextArea(this HtmlHelper htmlHelper, string name, string value = "",
            object labelHtmlAttributes = null, object editorHtmlAttributes = null, string labelText = null, bool readOnly = false,
            bool labelTextUp = Constants.DefaultLabelPosition, int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcTextArea(name, value, htmlAttributes: editorHtmlAttributes, readOnly: readOnly);
            return htmlHelper.FieldGroupEditor(editor.ToString(), labelHtmlAttributes, labelText, readOnly, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupListBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null, string labelText = null,
            bool labelTextUp = Constants.DefaultLabelPosition, int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcListBoxFor(expression, selectList, htmlAttributes: editorHtmlAttributes);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText, false, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null, string labelText = null,
            bool labelTextUp = Constants.DefaultLabelPosition, bool useAjax = false,
            int span = FieldSpan.Full, object htmlAttributes = null, bool readOnly = false)
        {
            MvcHtmlString editor = htmlHelper.BcDropDownListFor(expression, selectList, htmlAttributes: editorHtmlAttributes, useAjax: useAjax);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText, readOnly, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupDropDownGroupListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<GroupedSelectListItem> selectList,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null, string labelText = null,
            bool labelTextUp = Constants.DefaultLabelPosition, int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcDropDownGroupListFor(expression, selectList, htmlAttributes: editorHtmlAttributes);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText, false, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupDropDownList(this HtmlHelper htmlHelper, IEnumerable<SelectListItem> selectList,
            string labelText, object labelHtmlAttributes = null, object editorHtmlAttributes = null, string name = "",
            bool labelTextUp = Constants.DefaultLabelPosition, bool useAjax = false,
            int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcDropDownList(selectList, htmlAttributes: editorHtmlAttributes, name: name, useAjax: useAjax);
            return htmlHelper.FieldGroupEditor(editor.ToString(), labelHtmlAttributes, labelText, false, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupDropDownGroupList(this HtmlHelper htmlHelper, IEnumerable<GroupedSelectListItem> selectList,
            string labelText, object labelHtmlAttributes = null, object editorHtmlAttributes = null, string name = "",
            bool labelTextUp = Constants.DefaultLabelPosition, int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcDropDownGroupList(selectList, htmlAttributes: editorHtmlAttributes, name: name);
            return htmlHelper.FieldGroupEditor(editor.ToString(), labelHtmlAttributes, labelText, false, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupPasswordFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, bool labelTextUp = Constants.DefaultLabelPosition,
            int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcPasswordFor(expression, htmlAttributes: editorHtmlAttributes);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText, false, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupPassword(this HtmlHelper htmlHelper, string name, string value = "",
            object labelHtmlAttributes = null, object editorHtmlAttributes = null, string labelText = null,
            bool labelTextUp = Constants.DefaultLabelPosition, int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcPassword(name, value, htmlAttributes: editorHtmlAttributes);
            return htmlHelper.FieldGroupEditor(editor.ToString(), labelHtmlAttributes, labelText, false, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked = false, object value = null,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null, string labelText = null, bool readOnly = false,
            bool labelTextUp = Constants.DefaultLabelPosition, int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcCheckBox(name, isChecked, value, htmlAttributes: editorHtmlAttributes, readOnly: readOnly);
            return htmlHelper.FieldGroupEditor(editor.ToString(), labelHtmlAttributes, labelText, readOnly, labelTextUp, span, WithCheckSide(htmlAttributes, labelTextUp));
        }

        public static MvcHtmlString BcFieldGroupCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, bool readOnly = false, bool labelTextUp = Constants.DefaultLabelPosition,
            object htmlAttributes = null, int span = FieldSpan.Full)
        {
            MvcHtmlString editor = htmlHelper.BcCheckBoxFor(expression, htmlAttributes: editorHtmlAttributes, readOnly: readOnly);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText, readOnly, labelTextUp, span, WithCheckSide(htmlAttributes, labelTextUp));
        }

        public static MvcHtmlString BcFieldGroupCheckBoxToggleFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression,
            Icons dataOnIcon = Icons.Check, Icons dataOffIcon = Icons.Cancel,
            string dataOn = "Si", ElementThemeType dataOnthemeType = ElementThemeType.Primary,
            string dataOff = "No", ElementThemeType dataOffthemeType = ElementThemeType.Default,
            int? width = null, string labelText = null, object labelHtmlAttributes = null,
            bool labelTextUp = Constants.DefaultLabelPosition, object editorHtmlAttributes = null, bool readOnly = false,
            int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcCheckBoxToggleFor(expression, dataOnIcon: dataOnIcon, dataOffIcon: dataOffIcon,
                dataOn: dataOn, dataOnthemeType: dataOnthemeType,
                dataOff: dataOff, dataOffthemeType: dataOffthemeType,
                width: null,
                labelText: labelText, labelHtmlAttributes: labelHtmlAttributes, labelTextUp: labelTextUp,
                htmlAttributes: editorHtmlAttributes, readOnly: readOnly);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText, readOnly, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupSwitchFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression,
            Icons? dataOnIcon = Icons.Check, Icons? dataOffIcon = Icons.Cancel,
            string dataOn = "Si", ElementThemeType dataOnthemeType = ElementThemeType.Primary,
            string dataOff = "No", ElementThemeType dataOffthemeType = ElementThemeType.Default,
            int? width = null, bool showLabels = true,
            string labelText = null, object labelHtmlAttributes = null,
            bool labelTextUp = Constants.DefaultLabelPosition, object editorHtmlAttributes = null, bool readOnly = false,
            int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcSwitchFor(expression,
                dataOnIcon: dataOnIcon, dataOffIcon: dataOffIcon,
                dataOn: dataOn, dataOnthemeType: dataOnthemeType,
                dataOff: dataOff, dataOffthemeType: dataOffthemeType,
                width: width, showLabels: showLabels,
                htmlAttributes: editorHtmlAttributes, readOnly: readOnly);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText,
                readOnly, labelTextUp, span, htmlAttributes);
        }

        private static object WithCheckSide(object htmlAttributes, bool labelTextUp)
        {
            if (labelTextUp)
            {
                return htmlAttributes;
            }

            return Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.ContentClass.FieldCheckSideClass },
                htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupRadioButtonFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object value, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, bool labelTextUp = Constants.DefaultLabelPosition,
            int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcRadioButtonFor(expression, value, htmlAttributes: editorHtmlAttributes);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText, false, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupRadioButtonGroupFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, SelectList selectList, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, string name = null, bool labelTextUp = Constants.DefaultLabelPosition,
            int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcRadioButtonGroupFor(expression, selectList, htmlAttributes: editorHtmlAttributes, name: name);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText, false, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupPillGroupFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, SelectList selectList,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, string name = null, bool labelTextUp = Constants.DefaultLabelPosition,
            bool disabled = false, int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcPillGroupFor(expression, selectList,
                htmlAttributes: editorHtmlAttributes, name: name, disabled: disabled);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText,
                false, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupPillGroupFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList,
            object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            string labelText = null, string name = null, bool labelTextUp = Constants.DefaultLabelPosition,
            bool disabled = false, int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcPillGroupFor(expression, selectList,
                htmlAttributes: editorHtmlAttributes, name: name, disabled: disabled);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText,
                false, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupPillGroup(this HtmlHelper htmlHelper, string name,
            IEnumerable<SelectListItem> selectList, object selectedValue = null,
            string labelText = null, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            bool labelTextUp = Constants.DefaultLabelPosition, bool disabled = false,
            int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcPillGroup(name, selectList, selectedValue,
                htmlAttributes: editorHtmlAttributes, disabled: disabled);
            return htmlHelper.FieldGroupEditor(editor.ToString(), labelHtmlAttributes, labelText,
                false, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupDatePickerDevExFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, DateTime?>> expression, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            bool includeMessageValidation = false, string labelText = null, bool includeTime = false,
            bool labelTextUp = Constants.DefaultLabelPosition, bool readOnly = false,
            DateTime? minDate = null, DateTime? maxDate = null,
            int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcDatePickerDevExFor(expression, editorHtmlAttributes,
                includeMessageValidation: includeMessageValidation, includeTime: includeTime,
                minDate: minDate, maxDate: maxDate, readOnly: readOnly);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText, readOnly, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupDatePickerDevEx(this HtmlHelper htmlHelper, string name, DateTime? value,
            string labelText = null, object labelHtmlAttributes = null, object editorHtmlAttributes = null,
            bool includeTime = false, bool labelTextUp = Constants.DefaultLabelPosition, bool readOnly = false,
            DateTime? minDate = null, DateTime? maxDate = null,
            int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcDatePickerDevEx(name, value, htmlAttributes: editorHtmlAttributes,
                includeTime: includeTime, minDate: minDate, maxDate: maxDate, readOnly: readOnly);
            return htmlHelper.FieldGroupEditor(editor.ToString(), labelHtmlAttributes, labelText, readOnly, labelTextUp, span, htmlAttributes);
        }

        public static MvcHtmlString BcFieldGroupDisplayFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, object labelHtmlAttributes = null, object displayHtmlAttributes = null,
            string labelText = null, bool labelTextUp = Constants.DefaultLabelPosition,
            int span = FieldSpan.Full, object htmlAttributes = null)
        {
            MvcHtmlString editor = htmlHelper.BcDisplayFor(expression, htmlAttributes: displayHtmlAttributes);
            return htmlHelper.FieldGroupEditor(expression, editor.ToString(), labelHtmlAttributes, labelText, true, labelTextUp, span, htmlAttributes);
        }
    }
}
