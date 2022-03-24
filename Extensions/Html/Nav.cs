using System.Text;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class NavExtensions
    {
        public static MvcHtmlString BcNavBreadCrumb(this HtmlHelper htmlHelper, NavCollection navs)
        {
            TagBuilder maintag = new TagBuilder("div");
            maintag.AddCssClass(Bc.Web.Mvc.Html.Constants.Style.NavClass.BreadCrumbClass);
            maintag.Attributes.Add("id", Bc.Web.Mvc.Html.Constants.Style.NavClass.BreadCrumbClass);

            StringBuilder navBuilder = new StringBuilder();
            foreach (NavItem nav in navs)
            {
                navBuilder.AppendFormat("<{4} class=\"bnav {5}\" {0} title=\"{1}\">{2}{3}</{4}>",
                    nav.UrlTarget != null ? "href=\"" + nav.UrlTarget + "\"" : "",
                    nav.ToolTip,
                    !string.IsNullOrEmpty(nav.IconClass) ? htmlHelper.BcIcon(nav.IconClass).ToString() : "",
                    nav.Name,
                    nav.UrlTarget != null ? "a" : "div",
                    nav.Current ? "current" : "");
            }
            maintag.InnerHtml = navBuilder.ToString();

            return MvcHtmlString.Create(maintag.ToString());
        }
    }
}
