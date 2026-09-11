using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public static class OptionActivityButtonExtensions
    {
        public static string BcActivityButtonString(string Id, string ButtonType, string Icon, string Name, object htmlAttributes = null)
        {
            // Crear un objeto TagBuilder
            TagBuilder tagBuilder = new TagBuilder("a");

            // Establecer los atributos del tag
            tagBuilder.MergeAttribute("class", ButtonType);
            tagBuilder.MergeAttribute("id", Id.ToString()); // Asegúrate de convertir Id al tipo correcto si no es una cadena.
            tagBuilder.MergeAttribute("onclick", "ButtonClick(this)");

            // Agregar el contenido del tag
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

                    //tagBuilder.Attributes.Add(attr.Key, attr.Value.ToString());
                }
            }

            return tagBuilder.ToString();

            // return $"<a id=\"{Id}\" class=\"{ButtonType}\" onclick=\"ButtonClick(this)\"><span class=\"{Icon}\"></span>{Name}</a>";
        }

        private static string BcActivityLinkString(string Id, string ButtonType, string Icon, string Name, object htmlAttributes = null)
        {
            // Crear un objeto TagBuilder
            TagBuilder tagBuilder = new TagBuilder("a");

            // Establecer los atributos del tag
            tagBuilder.MergeAttribute("href", "#");
            tagBuilder.MergeAttribute("class", "btn-activity-link");
            tagBuilder.MergeAttribute("id", Id.ToString()); // Asegúrate de convertir Id al tipo correcto si no es una cadena.
            tagBuilder.MergeAttribute("onclick", "ButtonClick(this)");

            // Agregar el contenido del tag
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


            // return $"<a href=\"#\" class=\"btn-activity-link\" id=\"{Id}\" onclick=\"ButtonClick(this)\"><span class=\"{Icon}\"></span>{Name}</a>";
        }

        //public static MvcHtmlString BcOptionActivityButton(this HtmlHelper helper, int optionId, string ButtonId, string ButtonType, string Icon, string Name)
        //{
        //    var result = BcActivityButtonString(ButtonId, ButtonType, Icon, Name);

        //    return MvcHtmlString.Create(result);
        //}

        public static MvcHtmlString BcOptionActivityButton(this HtmlHelper helper, string IdActividad, object htmlAttributes = null,
            bool UseAsLink = false, bool ShowName = true)
        {
            var Current = Bc.Web.Mvc.Session.Current;

            if (Current.IsStarted && Current.CurrentOption != 0)
            {
                if (Current.CurrentActivity == string.Empty)
                {
                    var item = Current.ListActividad.FirstOrDefault(x => x.IdOpcion == Current.CurrentOption && x.IdActividad == IdActividad);

                    if (item != null)
                    {
                        string result = string.Empty;
                        string actividad = ShowName ? item.Actividad : string.Empty;

                        if (UseAsLink)
                            result = BcActivityLinkString(item.IdActividad, item.ButtonType, item.ClassIcon, actividad, htmlAttributes);
                        else
                            result = BcActivityButtonString(item.IdActividad, item.ButtonType, item.ClassIcon, actividad, htmlAttributes);

                        return MvcHtmlString.Create(result);
                    }
                }

                if (Current.CurrentActivity != string.Empty)
                {
                    var item = Current.ListAccion.FirstOrDefault(x => x.IdOpcion == Current.CurrentOption &&
                    x.IdActividad == Current.CurrentActivity && x.IdAccion == IdActividad);

                    if (item != null)
                    {
                        var result = BcActivityButtonString(item.IdAccion, item.ButtonType, item.ClassIcon, item.Accion);
                        return MvcHtmlString.Create(result);
                    }
                }
            }
            return MvcHtmlString.Empty;
        }


        public static MvcHtmlString BcOptionActivityButton(this HtmlHelper helper, object htmlAttributes = null)
        {
            var Current = Bc.Web.Mvc.Session.Current;

            StringBuilder buttons = new StringBuilder();

            if (Current.IsStarted && Current.CurrentOption != 0)
            {
                if (Current.CurrentActivity == string.Empty)
                {
                    foreach (var item in Current.ListActividad.Where(x => x.IdOpcion == Current.CurrentOption).OrderBy(x => x.Orden))
                    {
                        string button = BcActivityButtonString(item.IdActividad, item.ButtonType, item.ClassIcon, item.Actividad);

                        buttons.Append(button);
                    }


                    //var item = Current.ListActividad.FirstOrDefault(x => x.IdOpcion == Current.CurrentOption);

                    //if (item != null)
                    //{
                    //    var result = BcActivityButtonString(item.IdActividad, item.ButtonType, item.ClassIcon, item.Actividad);
                    //    return MvcHtmlString.Create(result);
                    //}
                }

                if (Current.CurrentActivity != string.Empty)
                {
                    foreach (var item in Current.ListAccion.Where(x => x.IdOpcion == Current.CurrentOption && x.IdActividad == Current.CurrentActivity).OrderBy(x => x.Orden))
                    {
                        string button = BcActivityButtonString(item.IdAccion, item.ButtonType, item.ClassIcon, item.Accion);
                        buttons.Append(button);
                    }

                    //var item = Current.ListAccion.FirstOrDefault(x => x.IdOpcion == Current.CurrentOption &&
                    //x.IdActividad == Current.CurrentActivity );

                    //if (item != null)
                    //{
                    //    var result = BcActivityButtonString(item.IdAccion, item.ButtonType, item.ClassIcon, item.Accion);
                    //    return MvcHtmlString.Create(result);
                    //}
                }
            }
            return MvcHtmlString.Create(buttons.ToString());
        }




        public static string BcOptionActivityButton(string IdActividad, object htmlAttributes = null,
          bool UseAsLink = false, bool ShowName = true)
        {
            string result = string.Empty;
            var Current = Bc.Web.Mvc.Session.Current;

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
