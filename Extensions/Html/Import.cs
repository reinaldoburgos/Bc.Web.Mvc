using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class ImportExtensions
    {
        //public static MvcHtmlString ImportScript(this HtmlHelper htmlHelper, string src, string type = null, bool includeVersion = false)
        //{
        //    type = type ?? "text/javascript";
        //    return MvcHtmlString.Create($"<script src=\"{ ContentVersion(src)}\" type = \"{type}\"></script>");
        //}

        //public static MvcHtmlString ImportStyleSheet(this HtmlHelper helper, string href = null, string rel = null, string type = null, bool includeVersion = false)
        //{
        //    type = type ?? "text/css";
        //    rel = rel ?? "stylesheet";


        //    return MvcHtmlString.Create($"<link rel=\"{rel}\" href=\"{ContentVersion(href)}\" type = \"{type}\"/>");
        //}

        //public static string ContentVersion(string contentPath)
        //{
        //    string version = ConfigurationManager.AppSettings["version"];
        //    return string.Format("{0}?v={1}", contentPath, version);
        //}

        public static MvcHtmlString ImportScript(this HtmlHelper htmlHelper, string src, string type = null)
        {
            type = type ?? "text/javascript";
            return MvcHtmlString.Create($"<script src=\"{ContentVersion(htmlHelper.ViewContext, src)}\" type = \"{type}\"></script>");
        }

        public static MvcHtmlString ImportStyleSheet(this HtmlHelper helper, string href = null, string rel = null, string type = null)
        {
            type = type ?? "text/css";
            rel = rel ?? "stylesheet";
            return MvcHtmlString.Create($"<link rel=\"{rel}\" href=\"{ContentVersion(helper.ViewContext, href)}\" type = \"{type}\"/>");
        }

        public static string ContentVersion(ViewContext viewContext, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
            {
                return string.Empty;
            }

            // Mapea la ruta virtual a la ruta física del servidor
            var absolutePath = viewContext.HttpContext.Server.MapPath(contentPath);

            if (File.Exists(absolutePath))
            {
                // Obtiene el timestamp del archivo
                var fileInfo = new FileInfo(absolutePath);
                var version = fileInfo.LastWriteTimeUtc.ToString("yyyyMMddHHmmss");

                // Construye la URL con el timestamp
                return UrlHelper.GenerateContentUrl(contentPath + "?v=" + version, viewContext.HttpContext);
            }

            // Si el archivo no existe, devuelve la ruta original sin el timestamp
            return UrlHelper.GenerateContentUrl(contentPath, viewContext.HttpContext);
        }
    }
}
