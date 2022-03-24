using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bc.Web.Mvc.Html
{
    public static class HelpInputGroupExtensions
    {
        private static MvcHtmlString HelpInputGroupEditor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, string editor,
            string iconClass, bool includeMessageValidation = true)
        {
            TagBuilder groupTag = new TagBuilder("div");
            groupTag.AddCssClass(Constants.Style.ContentClass.InputGroupClass);

            TagBuilder groupAddOnTag = new TagBuilder("span");
            groupAddOnTag.AddCssClass(Constants.Style.ContentClass.InputGroupAddOnClass);

            groupAddOnTag.InnerHtml = htmlHelper.BcIcon(iconClass).ToString();

            groupTag.InnerHtml = groupAddOnTag.ToString() + editor;

            string messageValidation = string.Empty;
            if (includeMessageValidation)
                messageValidation = htmlHelper.BcValidationMessageFor(expression).ToString();

            return MvcHtmlString.Create(groupTag.ToString() + messageValidation);
        }

        public static MvcHtmlString BcHelpTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, string iconClass, object htmlAttributes = null, bool includeMessageValidation = true)
        {
            string editor = htmlHelper.BcTextBoxFor(expression, htmlAttributes, includeMessageValidation: false).ToString();

            return htmlHelper.HelpInputGroupEditor(expression, editor, iconClass, includeMessageValidation);
        }

        public static MvcHtmlString BcHelpPasswordFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, string iconClass, object htmlAttributes = null,
            bool includeMessageValidation = true)
        {
            string editor = htmlHelper.BcPasswordFor(expression, htmlAttributes, includeMessageValidation: false).ToString();

            return htmlHelper.HelpInputGroupEditor(expression, editor, iconClass, includeMessageValidation);                        
        }
    }
}
