using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class OptionActivityButtonExtensions
    {
        public static string BcActivityButtonString(string Id, string ButtonType, string Icon, string Name, object htmlAttributes = null)
        {
            TagBuilder tagBuilder = new TagBuilder("a");

            tagBuilder.MergeAttribute("class", ButtonType);
            tagBuilder.MergeAttribute("id", Id.ToString());
            tagBuilder.MergeAttribute("onclick", "ButtonClick(this)");

            tagBuilder.InnerHtml = $"<span class=\"{Icon}\"></span>{Name}";

            if (htmlAttributes != null)
            {
                var attrs = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var attr in attrs)
                {
                    if (attr.Key == "class" && tagBuilder.Attributes.ContainsKey("class"))
                        tagBuilder.Attributes["class"] += " " + attr.Value.ToString();
                    else
                        tagBuilder.MergeAttribute(attr.Key, attr.Value.ToString(), true);
                }
            }

            return tagBuilder.ToString();
        }

        private static string BcActivityLinkString(string Id, string ButtonType, string Icon, string Name, object htmlAttributes = null)
        {
            TagBuilder tagBuilder = new TagBuilder("a");

            tagBuilder.MergeAttribute("href", "#");
            tagBuilder.MergeAttribute("class", "btn-activity-link");
            tagBuilder.MergeAttribute("id", Id.ToString());
            tagBuilder.MergeAttribute("onclick", "ButtonClick(this)");

            tagBuilder.InnerHtml = $"<span class=\"{Icon}\"></span>{Name}";

            if (htmlAttributes != null)
            {
                var attrs = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var attr in attrs)
                {
                    if (attr.Key == "class" && tagBuilder.Attributes.ContainsKey("class"))
                        tagBuilder.Attributes["class"] += " " + attr.Value.ToString();
                    else
                        tagBuilder.MergeAttribute(attr.Key, attr.Value.ToString(), true);
                }
            }

            return tagBuilder.ToString();
        }

        /// <summary>
        /// Sprint 2b: opción/actividad desde request o ViewBag (Resolve*). Sin Session para pintar.
        /// </summary>
        public static MvcHtmlString BcOptionActivityButton(this HtmlHelper helper, string IdActividad, object htmlAttributes = null,
            bool UseAsLink = false, bool ShowName = true, int? idOpcion = null, string actividad = null)
        {
            var current = Session.Current;
            if (!current.IsStarted)
            {
                return MvcHtmlString.Empty;
            }

            int optionId = ActivityButtonExtensions.ResolveIdOpcion(helper, idOpcion);
            string activity = ActivityButtonExtensions.ResolveActividad(helper, actividad);

            if (optionId == 0 || string.IsNullOrEmpty(IdActividad))
            {
                return MvcHtmlString.Empty;
            }

            if (string.IsNullOrEmpty(activity))
            {
                if (current.ListActividad == null)
                {
                    return MvcHtmlString.Empty;
                }

                var item = current.ListActividad.FirstOrDefault(x => x.IdOpcion == optionId && x.IdActividad == IdActividad);
                if (item == null)
                {
                    return MvcHtmlString.Empty;
                }

                string nombre = ShowName ? item.Actividad : string.Empty;
                string result = UseAsLink
                    ? BcActivityLinkString(item.IdActividad, item.ButtonType, item.ClassIcon, nombre, htmlAttributes)
                    : BcActivityButtonString(item.IdActividad, item.ButtonType, item.ClassIcon, nombre, htmlAttributes);
                return MvcHtmlString.Create(result);
            }

            if (current.ListAccion == null)
            {
                return MvcHtmlString.Empty;
            }

            var accion = current.ListAccion.FirstOrDefault(x => x.IdOpcion == optionId
                && x.IdActividad == activity
                && x.IdAccion == IdActividad);
            if (accion == null)
            {
                return MvcHtmlString.Empty;
            }

            return MvcHtmlString.Create(BcActivityButtonString(accion.IdAccion, accion.ButtonType, accion.ClassIcon, accion.Accion, htmlAttributes));
        }

        /// <summary>
        /// Sprint 2b: set de botones de la opción/actividad del request o ViewBag.
        /// Sin idOpcion/actividad en la firma: evita CS0121 con el overload (string IdActividad, ...).
        /// </summary>
        public static MvcHtmlString BcOptionActivityButton(this HtmlHelper helper, object htmlAttributes = null)
        {
            var current = Session.Current;
            if (!current.IsStarted)
            {
                return MvcHtmlString.Empty;
            }

            int optionId = ActivityButtonExtensions.ResolveIdOpcion(helper, null);
            string activity = ActivityButtonExtensions.ResolveActividad(helper, null);

            if (optionId == 0)
            {
                return MvcHtmlString.Empty;
            }

            StringBuilder buttons = new StringBuilder();

            if (string.IsNullOrEmpty(activity))
            {
                if (current.ListActividad == null)
                {
                    return MvcHtmlString.Empty;
                }

                foreach (var item in current.ListActividad.Where(x => x.IdOpcion == optionId).OrderBy(x => x.Orden))
                {
                    buttons.Append(BcActivityButtonString(item.IdActividad, item.ButtonType, item.ClassIcon, item.Actividad, htmlAttributes));
                }
            }
            else
            {
                if (current.ListAccion == null)
                {
                    return MvcHtmlString.Empty;
                }

                foreach (var item in current.ListAccion
                    .Where(x => x.IdOpcion == optionId && x.IdActividad == activity)
                    .OrderBy(x => x.Orden))
                {
                    buttons.Append(BcActivityButtonString(item.IdAccion, item.ButtonType, item.ClassIcon, item.Accion, htmlAttributes));
                }
            }

            return MvcHtmlString.Create(buttons.ToString());
        }

        /// <summary>
        /// Sin HtmlHelper: no hay ViewBag. Solo Session (legado; preferir overload con HtmlHelper).
        /// </summary>
        public static string BcOptionActivityButton(string IdActividad, object htmlAttributes = null,
          bool UseAsLink = false, bool ShowName = true)
        {
            string result = string.Empty;
            var Current = Session.Current;

            if (Current.IsStarted && Current.CurrentOption != 0)
            {
                if (Current.CurrentActivity == string.Empty)
                {
                    var item = Current.ListActividad.FirstOrDefault(x => x.IdOpcion == Current.CurrentOption && x.IdActividad == IdActividad);

                    if (item != null)
                    {
                        string actividad = ShowName ? item.Actividad : string.Empty;

                        if (UseAsLink)
                            result = BcActivityLinkString(item.IdActividad, item.ButtonType, item.ClassIcon, actividad, htmlAttributes);
                        else
                            result = BcActivityButtonString(item.IdActividad, item.ButtonType, item.ClassIcon, actividad, htmlAttributes);

                        return result;
                    }
                }

                if (Current.CurrentActivity != string.Empty)
                {
                    var item = Current.ListAccion.FirstOrDefault(x => x.IdOpcion == Current.CurrentOption &&
                    x.IdActividad == Current.CurrentActivity && x.IdAccion == IdActividad);

                    if (item != null)
                    {
                        result = BcActivityButtonString(item.IdAccion, item.ButtonType, item.ClassIcon, item.Accion);
                        return result;
                    }
                }
            }
            return result;
        }
    }
}
