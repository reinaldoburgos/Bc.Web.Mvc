using Bc.Web.Mvc.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public static class EntityLogExtensions
    {
        public static MvcHtmlString BcEntityLogViewer(this System.Web.Mvc.HtmlHelper htmlHelper, 
            long generalEntityId, object htmlAttributes = null)
        {
            TagBuilder tagControl = new TagBuilder("div");
            tagControl.AddCssClass("entityLogViewer");
            //tagControl.Attributes.Add("id","entityLogViewer-" + generalEntityId.ToString());
            tagControl.Attributes.Add("entityLogViewer", generalEntityId.ToString());

            var attr =
                System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            foreach (var a in attr)
                tagControl.Attributes.Add(a.Key, a.Value.ToString());

            return MvcHtmlString.Create(tagControl.ToString());
        }
    }
}
