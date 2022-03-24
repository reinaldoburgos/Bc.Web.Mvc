using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class EmbedResourceExtensions
    {
        public static MvcHtmlString EmbedTextResource(this HtmlHelper htmlHelper, string path)
        {
            // take a path that starts with "~" and map it to the filesystem.
            var cssFilePath = HttpContext.Current.Server.MapPath(path);
            // load the contents of that file
            string cssText;
            try
            {
                cssText = System.IO.File.ReadAllText(cssFilePath);
            }
            catch (Exception)
            {
                // blank string if we can't read the file for any reason
                cssText = "";
            }
            return MvcHtmlString.Create(cssText);
        }
    }
}
