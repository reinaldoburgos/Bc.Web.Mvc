using System.Collections.Generic;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    /// <summary>
    /// Barra de filtros de Index/consultas. Envuelve dp-field-grid + dp-filter-bar.
    /// No persiste valores ni dispara consultas — solo markup/CSS.
    /// </summary>
    public static class FilterBarExtensions
    {
        public static MvcContent BcBeginFilterBar(this HtmlHelper htmlHelper,
            object htmlAttributes = null,
            FilterBarWidth width = FilterBarWidth.Default)
        {
            string css = Constants.Style.ContentClass.FieldGridClass
                + " " + Constants.Style.ContentClass.FilterBarClass;

            if (width == FilterBarWidth.Full)
            {
                css += " " + Constants.Style.ContentClass.FilterBarFullClass;
            }
            else if (width == FilterBarWidth.Narrow)
            {
                css += " " + Constants.Style.ContentClass.FilterBarNarrowClass;
            }

            IDictionary<string, object> resultHtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = css },
                htmlAttributes);

            return new MvcContent(
                () => htmlHelper.BcBeginContent(htmlAttributes: resultHtmlAttributes),
                () => htmlHelper.BcEndContent()
            );
        }
    }
}
