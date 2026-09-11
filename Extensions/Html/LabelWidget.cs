using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.Html;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class LabelWidgetExtensions
    {
        public static MvcHtmlString BcLabelWidget(this HtmlHelper htmlHelper, string text, LabelWidgetType type = LabelWidgetType.Default)
        {
            return MvcHtmlString.Create(string.Format("<span style ='padding: 0.5rem;' class=\"{0} {1}\">{2}</span>", 
                Constants.Style.WidgetClass.LabelClass,
                BcHelper.GetLabelTypeClass(type),
                text));            
        }
    }
}
