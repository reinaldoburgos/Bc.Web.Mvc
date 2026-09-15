using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Bc;
using Dece.Common.Security;

namespace Bc.Web.Mvc.Html
{
    /// <summary>
    /// Helpers aditivos de botones (Sprint 2.3). No reemplazan BcOptionActivityButton / BcButton.
    /// Fuente de opción/actividad: parámetros → ViewBag → Session (fallback; 2b limpia la sesión).
    /// </summary>
    public static class ActivityButtonExtensions
    {
        public const string ViewBagIdOpcion = "IdOpcion";
        public const string ViewBagIdActividad = "IdActividad";

        public static MvcContent BcBeginContentHeader(this HtmlHelper htmlHelper, string title,
            string subtitle = null, object htmlAttributes = null)
        {
            return new MvcContent(
                () => htmlHelper.BeginContentHeader(title, subtitle, htmlAttributes),
                () => htmlHelper.EndContentHeader()
            );
        }

        private static void BeginContentHeader(this HtmlHelper htmlHelper, string title, string subtitle, object htmlAttributes)
        {
            IDictionary<string, object> attrs = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.ContentClass.DetailHeaderClass },
                htmlAttributes);

            TagBuilder header = new TagBuilder("div");
            foreach (var attr in attrs)
            {
                header.Attributes[attr.Key] = Convert.ToString(attr.Value);
            }

            string open = header.ToString();
            int closeAt = open.IndexOf("</div>", StringComparison.Ordinal);
            if (closeAt >= 0)
            {
                open = open.Remove(closeAt);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(open);

            sb.AppendFormat("<div class=\"{0}\">", Constants.Style.ContentClass.DetailHeaderTextClass);
            sb.AppendFormat("<h2>{0}</h2>", HttpUtility.HtmlEncode(title ?? string.Empty));
            if (!string.IsNullOrWhiteSpace(subtitle))
            {
                sb.AppendFormat("<div class=\"{0}\">{1}</div>",
                    Constants.Style.ContentClass.DetailHeaderSubClass,
                    HttpUtility.HtmlEncode(subtitle));
            }
            sb.Append("</div>");

            sb.AppendFormat("<div class=\"{0}\">", Constants.Style.ContentClass.FormActionClass);

            htmlHelper.ViewContext.Writer.Write(sb.ToString());
        }

        private static void EndContentHeader(this HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("</div></div>");
        }

        public static MvcHtmlString BcUiButton(this HtmlHelper htmlHelper, string text,
            ElementThemeType themeType = ElementThemeType.Primary,
            string iconClass = null, string id = null, bool submit = false,
            object htmlAttributes = null)
        {
            string buttonClass = Constants.Style.ElementClass.ButtonClass + " btn-" + themeType.GetID();
            return BcUiButton(htmlHelper, text, buttonClass, iconClass, id, submit, htmlAttributes);
        }

        public static MvcHtmlString BcUiButton(this HtmlHelper htmlHelper, string text, string buttonClass,
            string iconClass = null, string id = null, bool submit = false,
            object htmlAttributes = null)
        {
            TagBuilder tag;
            if (submit)
            {
                tag = new TagBuilder("input");
                tag.Attributes["type"] = "submit";
                tag.Attributes["value"] = text ?? string.Empty;
            }
            else
            {
                tag = new TagBuilder("button");
                tag.Attributes["type"] = "button";
                string iconHtml = string.Empty;
                if (!string.IsNullOrWhiteSpace(iconClass))
                {
                    iconHtml = string.Format("<span class=\"{0}\"></span>", iconClass);
                }

                if (!string.IsNullOrEmpty(iconHtml) && !string.IsNullOrEmpty(text))
                {
                    tag.InnerHtml = iconHtml + HttpUtility.HtmlEncode(text);
                }
                else if (!string.IsNullOrEmpty(iconHtml))
                {
                    tag.InnerHtml = iconHtml;
                }
                else
                {
                    tag.InnerHtml = HttpUtility.HtmlEncode(text ?? string.Empty);
                }
            }

            if (!string.IsNullOrWhiteSpace(buttonClass))
            {
                tag.AddCssClass(buttonClass);
            }

            if (!string.IsNullOrWhiteSpace(id))
            {
                tag.Attributes["id"] = id;
            }

            MergeAttributes(tag, htmlAttributes);
            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString BcActivityButtons(this HtmlHelper helper,
            object htmlAttributes = null, int? idOpcion = null, string actividad = null)
        {
            int optionId = ResolveIdOpcion(helper, idOpcion);
            string activity = ResolveActividad(helper, actividad);

            if (optionId == 0)
            {
                return MvcHtmlString.Empty;
            }

            var current = Session.Current;
            if (!current.IsStarted)
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

                foreach (var item in current.ListActividad
                    .Where(x => x.IdOpcion == optionId)
                    .OrderBy(x => x.Orden))
                {
                    buttons.Append(BuildActivityButton(item.IdActividad, item.ButtonType, item.ClassIcon,
                        item.Actividad, useAsLink: false, htmlAttributes: htmlAttributes));
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
                    buttons.Append(BuildActivityButton(item.IdAccion, item.ButtonType, item.ClassIcon,
                        item.Accion, useAsLink: false, htmlAttributes: htmlAttributes));
                }
            }

            return MvcHtmlString.Create(buttons.ToString());
        }

        public static MvcHtmlString BcActivityButton(this HtmlHelper helper, string IdActividad,
            object htmlAttributes = null, bool UseAsLink = false, bool ShowName = true,
            int? idOpcion = null, string actividad = null)
        {
            int optionId = ResolveIdOpcion(helper, idOpcion);
            string activity = ResolveActividad(helper, actividad);

            if (optionId == 0 || string.IsNullOrEmpty(IdActividad))
            {
                return MvcHtmlString.Empty;
            }

            var current = Session.Current;
            if (!current.IsStarted)
            {
                return MvcHtmlString.Empty;
            }

            if (string.IsNullOrEmpty(activity))
            {
                if (current.ListActividad == null)
                {
                    return MvcHtmlString.Empty;
                }

                OpcionActividadQuery item = current.ListActividad
                    .FirstOrDefault(x => x.IdOpcion == optionId && x.IdActividad == IdActividad);

                if (item == null)
                {
                    return MvcHtmlString.Empty;
                }

                string name = ShowName ? item.Actividad : string.Empty;
                return MvcHtmlString.Create(BuildActivityButton(item.IdActividad, item.ButtonType,
                    item.ClassIcon, name, UseAsLink, htmlAttributes));
            }

            if (current.ListAccion == null)
            {
                return MvcHtmlString.Empty;
            }

            OpcionActividadAccionQuery accion = current.ListAccion
                .FirstOrDefault(x => x.IdOpcion == optionId
                    && x.IdActividad == activity
                    && x.IdAccion == IdActividad);

            if (accion == null)
            {
                return MvcHtmlString.Empty;
            }

            string accionName = ShowName ? accion.Accion : string.Empty;
            return MvcHtmlString.Create(BuildActivityButton(accion.IdAccion, accion.ButtonType,
                accion.ClassIcon, accionName, UseAsLink, htmlAttributes));
        }

        internal static int ResolveIdOpcion(HtmlHelper helper, int? idOpcion)
        {
            if (idOpcion.HasValue && idOpcion.Value != 0)
            {
                return idOpcion.Value;
            }

            object fromView = helper != null ? helper.ViewContext.ViewData[ViewBagIdOpcion] : null;
            if (fromView != null)
            {
                int parsed;
                if (int.TryParse(Convert.ToString(fromView), out parsed) && parsed != 0)
                {
                    return parsed;
                }
            }

            var current = Session.Current;
            if (current.IsStarted && current.CurrentOption != 0)
            {
                return current.CurrentOption;
            }

            return 0;
        }

        /// <summary>
        /// null = resolver ViewBag/sesión; "" = modo Index (ListActividad).
        /// </summary>
        internal static string ResolveActividad(HtmlHelper helper, string actividad)
        {
            if (actividad != null)
            {
                return actividad;
            }

            object fromView = helper != null ? helper.ViewContext.ViewData[ViewBagIdActividad] : null;
            if (fromView != null)
            {
                return Convert.ToString(fromView) ?? string.Empty;
            }

            var current = Session.Current;
            if (current.IsStarted)
            {
                return current.CurrentActivity ?? string.Empty;
            }

            return string.Empty;
        }

        private static string BuildActivityButton(string id, string buttonType, string icon, string name,
            bool useAsLink, object htmlAttributes)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.Attributes["id"] = id ?? string.Empty;
            tag.Attributes["onclick"] = "ButtonClick(this)";

            if (useAsLink)
            {
                tag.Attributes["href"] = "#";
                tag.AddCssClass(Constants.Style.ContentClass.ActivityLinkClass);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(buttonType))
                {
                    tag.AddCssClass(buttonType);
                }
            }

            string iconHtml = string.IsNullOrWhiteSpace(icon)
                ? string.Empty
                : string.Format("<span class=\"{0}\"></span>", icon);
            tag.InnerHtml = iconHtml + HttpUtility.HtmlEncode(name ?? string.Empty);

            MergeAttributes(tag, htmlAttributes);
            return tag.ToString();
        }

        private static void MergeAttributes(TagBuilder tag, object htmlAttributes)
        {
            if (htmlAttributes == null)
            {
                return;
            }

            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            foreach (var attr in attrs)
            {
                if (attr.Key == "class" && tag.Attributes.ContainsKey("class"))
                {
                    tag.Attributes["class"] += " " + Convert.ToString(attr.Value);
                }
                else
                {
                    tag.MergeAttribute(attr.Key, Convert.ToString(attr.Value), true);
                }
            }
        }
    }
}
