using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class EntityFormExtensions
    {
        public static MvcHtmlString BcEntityForm(this System.Web.Mvc.HtmlHelper htmlHelper,
            string entityTypeId, string entityTypeViewId, long? generalEntityId = null, bool readOnly = true, string afterLoadMethod = null, object htmlAttributes = null)
        {
            TagBuilder tagControl = new TagBuilder("div");
            tagControl.AddCssClass("entityForm");

            tagControl.Attributes.Add("entityTypeId", entityTypeId);
            tagControl.Attributes.Add("entityTypeViewId", entityTypeViewId);
            tagControl.Attributes.Add("generalEntityId", generalEntityId.HasValue ? generalEntityId.ToString() : String.Empty);
            tagControl.Attributes.Add("isReadOnly", readOnly ? "1" : "0");
            tagControl.Attributes.Add("afterLoadMethod", afterLoadMethod);

            var attr =
                System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            foreach (var a in attr)
                tagControl.Attributes.Add(a.Key, a.Value.ToString());

            return MvcHtmlString.Create(tagControl.ToString());
        }
    }
}
