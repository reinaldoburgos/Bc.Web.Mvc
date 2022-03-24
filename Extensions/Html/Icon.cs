using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class IconExtensions
    {
        public static MvcHtmlString BcIcon(this HtmlHelper htmlHelper, string iconClass)
        {
            return new MvcHtmlString(string.Format("<i class=\"{0}\"></i>", iconClass));
        }

        public static MvcHtmlString BcIcon(this HtmlHelper htmlHelper, Icons icon)
        {
            return new MvcHtmlString(string.Format("<i class=\"{0}\"></i>", icon.GetID()));
        }

        public static MvcHtmlString BcIcon(string iconClass)
        {
            return new MvcHtmlString(string.Format("<i class=\"{0}\"></i>", iconClass));
        }

        public static MvcHtmlString BcIcon(Icons icon)
        {
            return new MvcHtmlString(string.Format("<i class=\"{0}\"></i>", icon.GetID()));
        }
    }
}
