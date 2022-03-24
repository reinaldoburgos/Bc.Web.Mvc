using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bc.Web.Mvc.Html
{
    public static class SelectGroupExtensions
    {
        public static MvcHtmlString BcDropDownGroupListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,
            TProperty>> expression,
            IEnumerable<GroupedSelectListItem> selectList,
            object htmlAttributes = null)
        {
            var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
               new { @class = Constants.Style.ElementClass.SelectClass, Bc_select = "" },
               htmlAttributes);

            string editor = htmlHelper.DropDownGroupListFor(expression, selectList, htmlAttributes: editorAttr).ToString();

            string validationMessage = htmlHelper.BcValidationMessageFor(expression).ToString();

            return MvcHtmlString.Create(editor.ToString() + validationMessage.ToString());
        }

        public static MvcHtmlString BcDropDownGroupList(this HtmlHelper htmlHelper,
            IEnumerable<GroupedSelectListItem> selectList, object htmlAttributes = null, string name = "")
        {
            var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
               new { @class = Constants.Style.ElementClass.SelectClass, Bc_select = "" },
               htmlAttributes);

            string editor = htmlHelper.DropDownGroupList(name, selectList, htmlAttributes: editorAttr).ToString();

            return MvcHtmlString.Create(editor.ToString());
        }
    }
}
