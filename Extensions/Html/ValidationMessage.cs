using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public static class ValidationMessageExtensions
    {
        public static MvcHtmlString BcValidationMessageFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var messageAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.GeneralClass.InlineMessageValidationClass },
                htmlAttributes);


            

            return htmlHelper.ValidationMessageFor(expression, null, messageAttr);
        }
    }
}