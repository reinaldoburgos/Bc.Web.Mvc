using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public static class CheckBoxToogleExtensions
    {
        public static MvcHtmlString BcCheckBoxToggleFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression,
            Icons dataOnIcon = Icons.Check, Icons dataOffIcon = Icons.Cancel,
            string dataOn = "Si", ElementThemeType dataOnthemeType = ElementThemeType.Primary,
            string dataOff = "No", ElementThemeType dataOffthemeType = ElementThemeType.Default,
            int? width = null,
            string labelText = null, HtmlColumnSize labelSize = HtmlColumnSize.Size_4, object labelHtmlAttributes = null, bool labelTextUp = false,
            object htmlAttributes = null, bool readOnly = false)
        {
            // Crear un diccionario para los atributos adicionales que se agregarán al checkbox
            //var additionalAttributes = new
            //{
            //    data_toggle = "toggle",
            //    data_on = $"<i class='{dataOnIcon.GetID()}'></i> {dataOn}",
            //    data_onstyle = dataOnthemeType.GetID(),
            //    data_off = $"<i class='{dataOffIcon.GetID()}'></i> {dataOff}",
            //    data_offstyle = dataOnthemeType.GetID(),
            //    data_size = "small"
            //};

            // Combinar los atributos adicionales con los atributos HTML proporcionados
            var mergedAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            mergedAttributes["data-toggle"] = "toggle";

            mergedAttributes["data-on"] = $"<i class='{dataOnIcon.GetID()}'></i> {dataOn}";
            mergedAttributes["data-onstyle"] = dataOnthemeType.GetID();

            mergedAttributes["data-off"] = $"<i class='{dataOffIcon.GetID()}'></i> {dataOff}";
            mergedAttributes["data-offstyle"] = dataOffthemeType.GetID();

            mergedAttributes["data-size"] = "small";
            if (width != null) { mergedAttributes["data-width"] = $"{width}px"; };

            if (readOnly) { mergedAttributes["disabled"] = "disabled"; }

            mergedAttributes["type"] = "checkbox";
            mergedAttributes["bcType"] = "CheckBox";


            //var finalAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(additionalAttributes);
            //foreach (var item in finalAttributes)
            //{
            //    mergedAttributes[item.Key] = item.Value;
            //}

            // Generar el checkbox utilizando CheckBoxFor con los atributos combinados
            MvcHtmlString checkBoxHtml = htmlHelper.CheckBoxFor(expression, mergedAttributes);




            // Envolver el checkbox en un div y etiqueta label como se requiere
            var divTag = new TagBuilder("div");

            var labelTag = new TagBuilder("label");
            labelTag.AddCssClass("checkbox-inline");
            //labelTag.InnerHtml = $"{labelText} " + checkBoxHtml;
            labelTag.InnerHtml = "" + checkBoxHtml;

            divTag.InnerHtml = labelTag.ToString();

            return MvcHtmlString.Create(divTag.ToString());
        }

        public static MvcHtmlString BcCheckBoxToogle(this HtmlHelper htmlHelper, string name, bool isChecked = false, object value = null,
           object htmlAttributes = null, bool readOnly = false)
        {
            IDictionary<string, object> resulthtmlAttributes = null;
            if (readOnly)
            {
                resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { name = name, value = value, disabled = "" },
                htmlAttributes);
            }
            else
            {
                resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { name = name, value = value },
                htmlAttributes);
            }

            TagBuilder maintag = new TagBuilder("label");
            TagBuilder checkBox = new TagBuilder("input");

            checkBox.AddCssClass(Bc.Web.Mvc.Html.Constants.Style.ElementClass.CheckBoxClass);

            if (readOnly)
                checkBox.AddCssClass(Bc.Web.Mvc.Html.Constants.Style.GeneralClass.DisabledClass);

            checkBox.Attributes.Add("type", "checkbox");
            checkBox.Attributes.Add("bcType", "CheckBox");

            foreach (var attr in resulthtmlAttributes)
            {
                checkBox.Attributes.Add(attr.Key, Convert.ToString(attr.Value));
            }

            if (isChecked)
                checkBox.Attributes.Add("checked", "checked");

            //Create input hidden
            //maintag.InnerHtml = htmlHelper.CheckBox(name, isChecked, resulthtmlAttributes).ToString();
            maintag.InnerHtml = checkBox.ToString();

            return MvcHtmlString.Create(maintag.ToString());
        }
    }
}
