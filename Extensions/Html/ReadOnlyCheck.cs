using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bc.Web.Mvc.Html
{
    public static class ReadOnlyCheckExtensions
    {
        public static MvcHtmlString BcReadOnlyCheck(this HtmlHelper htmlHelper, bool check)
        {
            if (check)
                return MvcHtmlString.Create(string.Format("<i class=\"{0}\"></i>", "fa fa-check"));
            else
                return MvcHtmlString.Empty;
        }     
    }
}
