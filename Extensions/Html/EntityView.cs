using System;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class EntityViewerExtensions
    {
        public static MvcHtmlString BcEntityViewer(this System.Web.Mvc.HtmlHelper htmlHelper,
            string entityTypeId, string entityTypeViewId, string entityKey = null, int height = 300, object htmlAttributes = null)
        {
            TagBuilder tagControl = new TagBuilder("div");
            tagControl.AddCssClass("entityViewer");

            tagControl.Attributes.Add("entityTypeId", entityTypeId);
            tagControl.Attributes.Add("entityTypeViewId", entityTypeViewId);
            tagControl.Attributes.Add("entityKey", entityKey);
            tagControl.Attributes.Add("height", Convert.ToString(height));

            var attr =
                System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            foreach (var a in attr)
                tagControl.Attributes.Add(a.Key, a.Value.ToString());

            return MvcHtmlString.Create(tagControl.ToString());
        }
    }
}
