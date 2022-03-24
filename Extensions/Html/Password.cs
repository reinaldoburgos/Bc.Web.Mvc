using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bc.Web.Mvc.Html
{
    public static class PasswordExtensions
    {
        public static MvcHtmlString BcPasswordFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TValue>> expression, object htmlAttributes = null, bool includeMessageValidation = true)
        {
            var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.ElementClass.TextBoxClass },
                htmlAttributes);

            if (!editorAttr.ContainsKey("autocomplete"))
                editorAttr.Add("autocomplete", "off");

            string editor = htmlHelper.PasswordFor(expression, htmlAttributes: editorAttr).ToString();

            string validationMessage = string.Empty;
            if(includeMessageValidation)
                validationMessage = htmlHelper.BcValidationMessageFor(expression).ToString();

            return MvcHtmlString.Create(editor.ToString() + validationMessage.ToString());
        }

        public static MvcHtmlString BcPassword(this HtmlHelper htmlHelper, string name, string value = "", object htmlAttributes = null)
        {
            var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.ElementClass.TextBoxClass },
                htmlAttributes);

            if (!editorAttr.ContainsKey("autocomplete"))
                editorAttr.Add("autocomplete", "off");

            string editor = htmlHelper.Password(name, value, editorAttr).ToString();

            return MvcHtmlString.Create(editor.ToString());
        }

    }
}
