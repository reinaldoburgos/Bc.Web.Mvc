using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bc.Web.Mvc.Html
{
    public static class ListBoxExtensions
    {
        public static MvcHtmlString BcListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,
            TProperty>> expression, IEnumerable<SelectListItem> selectList, ListBoxMode mode = ListBoxMode.Default, object htmlAttributes = null)
        {
            if (mode == ListBoxMode.Default)
                mode = ListBoxMode.Tags;

            string modeAttr = ((int)mode).ToString();

            var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
               new { @class = Constants.Style.ElementClass.SelectClass, Bc_select = "", Bc_listbox_mode = modeAttr, place_holder = "Seleccione un Item" },
               htmlAttributes);

            string editor = htmlHelper.ListBoxFor(expression, selectList, htmlAttributes: editorAttr).ToString();

            string validationMessage = htmlHelper.BcValidationMessageFor(expression).ToString();

            return MvcHtmlString.Create(editor.ToString() + validationMessage.ToString());
        }

        //public static MvcHtmlString BcListBoxGeneralValuesFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,
        //    TProperty>> expression, short id, object htmlAttributes = null, Func<Bc.Web.Mvc.Helper.GeneralValues, object> sortexpression = null, ListBoxMode mode = ListBoxMode.Default)
        //{
        //    Proxies.GeneralProxy genaralProxy = new Proxies.GeneralProxy();

        //    if (sortexpression == null)
        //        return htmlHelper.BcListBoxFor(expression, genaralProxy.GetGeneralValuesById(id).ToSelectList("Code", "Content"),  mode: mode, htmlAttributes : htmlAttributes);
        //    else
        //        return htmlHelper.BcListBoxFor(expression, genaralProxy.GetGeneralValuesById(id).OrderBy(sortexpression).ToSelectList("Code", "Content"), mode : mode, htmlAttributes : htmlAttributes);
        //}

        public static MvcHtmlString BcListBox(this HtmlHelper htmlHelper,
            IEnumerable<SelectListItem> selectList, ListBoxMode mode = ListBoxMode.Default, object htmlAttributes = null, string name = "")
        {
            if (mode == ListBoxMode.Default)
                mode = ListBoxMode.Tags;

            string modeAttr = ((int)mode).ToString();

            IDictionary<string, object> editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
               new { @class = Constants.Style.ElementClass.SelectClass, Bc_select = "", Bc_listbox_mode = modeAttr, place_holder = "Seleccione un Item" },
               htmlAttributes);

            string editor = htmlHelper.ListBox(name, selectList, htmlAttributes: htmlAttributes).ToString();

            return MvcHtmlString.Create(editor.ToString());
        }



    }
}
