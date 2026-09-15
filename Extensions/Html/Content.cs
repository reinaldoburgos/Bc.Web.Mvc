using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public static class ContentExtensions
    {
        internal static void BcBeginContent(this System.Web.Mvc.HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder builder = new TagBuilder("div");
            if (htmlAttributes != null)
            {
                foreach (var attr in htmlAttributes)
                {
                    builder.Attributes.Add(attr.Key, Convert.ToString(attr.Value));
                }
            }
            string result = builder.ToString();
            result = result.Remove(result.IndexOf("</div>"));
            htmlHelper.ViewContext.Writer.Write(result);
        }

        internal static void BcEndContent(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("</div>");
        }

        public static MvcContent BcBeginRow(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            var resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.ContentClass.Row },
                htmlAttributes);

            return new MvcContent(
                () => htmlHelper.BcBeginContent(htmlAttributes: resulthtmlAttributes),
                () => htmlHelper.BcEndContent()
            );
        }


        public static MvcContent BcBeginFlexRow(this HtmlHelper htmlHelper, bool wrap = true,
            FlexDirection flexDirection = FlexDirection.Row,
            int? gap = null,
            FlexStack stackAt = FlexStack.None,
            object htmlAttributes = null)
        {
            string stackClass = stackAt == FlexStack.Tablet
                ? Constants.Style.ContentClass.FlexClass + " " + Constants.Style.ContentClass.FlexStackTabletClass
                : "";

            var resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = stackClass },
                htmlAttributes);

            // Obtener los estilos CSS existentes del atributo "style"
            var estilosExistentes = resulthtmlAttributes.ContainsKey("style") ? resulthtmlAttributes["style"].ToString() : "";

            // Definir los nuevos estilos CSS que deseas agregar
            var nuevosEstilos = $"display: flex; flex-direction: {BcHelper.GetFlexDirection(flexDirection)};";

            if (wrap) nuevosEstilos += " flex-wrap: wrap;";

            if (gap != null)
                nuevosEstilos += $" gap: {gap}px;";

            // Combinar los estilos existentes con los nuevos estilos
            var estilosCombinados = estilosExistentes + " " + nuevosEstilos;

            // Asignar los estilos combinados al atributo "style"
            resulthtmlAttributes["style"] = estilosCombinados;


            return new MvcContent(
                () => htmlHelper.BcBeginContent(htmlAttributes: resulthtmlAttributes),
                () => htmlHelper.BcEndContent()
            );
        }

        public static MvcContent BcBeginTabPane(this HtmlHelper htmlHelper, string id, bool active = false, object htmlAttributes = null)
        {
            var resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { id = id, @class = Constants.Style.ContentClass.TabPane + " " + (active ? "active" : "") },
                htmlAttributes);

            return new MvcContent(
                () => htmlHelper.BcBeginContent(htmlAttributes: resulthtmlAttributes),
                () => htmlHelper.BcEndContent()
            );
        }

        public static MvcContent BcBeginTabPane(this HtmlHelper htmlHelper, TabPane tab, object htmlAttributes = null)
        {
            return htmlHelper.BcBeginTabPane(tab.Key, tab.Selected, htmlAttributes);
        }


        public static MvcContent BcBeginColumn(this HtmlHelper htmlHelper, HtmlColumnSize size, bool paddingContent = true, object htmlAttributes = null)
        {
            var resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = BcHelper.GetColumnSizeClass(size) },
                htmlAttributes);

            if (size == HtmlColumnSize.Auto)
            {
                resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @style = "width:auto" },
                resulthtmlAttributes);
            }

            if (!paddingContent)
            {
                resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.GeneralClass.NoPaddingClass },
                resulthtmlAttributes);
            }

            return new MvcContent(
                () => htmlHelper.BcBeginContent(htmlAttributes: resulthtmlAttributes),
                () => htmlHelper.BcEndContent()
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basis">Por defecto auto si se deja en null. Tamaño base de un elemento flexible antes de que se distribuya el espacio adicional o se apliquen las reglas de crecimiento y contracción. El valor por defecto es auto, lo que significa que el tamaño base se determina por el contenido del elemento.</param>
        /// <param name="minWidthPixel"></param>
        /// <param name="grow">Capacidad de un elemento flexible para crecer en relación con los otros elementos flexibles dentro del contenedor flexbox cuando hay espacio adicional disponible</param>
        /// <param name="shrink">Capacidad de un elemento flexible para contraerse en relación con los otros elementos flexibles dentro del contenedor flexbox cuando hay espacio insuficiente disponible</param>
        /// <param name="paddingContent"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcContent BcBeginFlexColumn(this HtmlHelper htmlHelper,
            int? basis = null, int? minWidthPixel = null,
            int grow = 0, int shrink = 1,

            bool paddingContent = true, object htmlAttributes = null)
        {

            var resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = "bc-custom-flex-column" },
                htmlAttributes);

            // valores por defecto 
            //flex-grow: 0
            //flex-shrink: 1
            //flex-basis: auto

            // Determinar el valor de flex-basis
            string flexBasisValue = basis == null ? "auto" : (basis == 0 ? "0" : $"{basis}%");

            //var style = $"flex-basis: {(basis == 0 ? "auto;" : $"{ basis}% ")}; ";
            //var style = $"flex-basis: {(basis == 0 ? "auto;" : $"{ basis}% ")}; ";
            //style += $" flex-grow: {grow};";
            //style += $" flex-shrink: {shrink};";


            var style = $"flex-basis: {flexBasisValue}; flex-grow: {grow}; flex-shrink: {shrink};";


            if (minWidthPixel != null)
                style += $" min-width: {minWidthPixel}px;"; // Agregar la propiedad min-width al estilo


            resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @style = style },
                resulthtmlAttributes);

            return new MvcContent(
                () => htmlHelper.BcBeginContent(htmlAttributes: resulthtmlAttributes),
                () => htmlHelper.BcEndContent()
            );
        }

    }
}
