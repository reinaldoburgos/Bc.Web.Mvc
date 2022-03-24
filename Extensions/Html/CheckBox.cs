using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public static class CheckBoxExtensions
    {
        public static MvcHtmlString BcCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, bool>> expression, object htmlAttributes = null, bool readOnly = false)
        {            
            TagBuilder maintag = new TagBuilder("div");
            maintag.AddCssClass("checkbox");

            TagBuilder contentDiv = new TagBuilder("label");

            IDictionary<string, object> editorAttr = null;
            if(readOnly)
            {
                editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { disabled = "", @class = Bc.Web.Mvc.Html.Constants.Style.ElementClass.CheckBoxClass + " " + Bc.Web.Mvc.Html.Constants.Style.GeneralClass.DisabledClass },
                htmlAttributes);
            }
            else
            {
                editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Bc.Web.Mvc.Html.Constants.Style.ElementClass.CheckBoxClass },
                htmlAttributes);
            }
            

            contentDiv.InnerHtml = htmlHelper.CheckBoxFor(expression, editorAttr).ToString();
            maintag.InnerHtml = contentDiv.ToString();

            return MvcHtmlString.Create(maintag.ToString());
        }

        public static MvcHtmlString BcDisableCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, bool>> expression, object htmlAttributes = null)
        {
            var editorAttr = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { disabled = "", @class = Bc.Web.Mvc.Html.Constants.Style.ElementClass.CheckBoxClass },
                htmlAttributes);

            TagBuilder maintag = new TagBuilder("label");
            
            maintag.AddCssClass(Constants.Style.GeneralClass.DisabledClass);            

            maintag.InnerHtml = htmlHelper.CheckBoxFor(expression, editorAttr).ToString();

            return MvcHtmlString.Create(maintag.ToString());
        }

        public static MvcHtmlString BcCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked = false, object value = null,
           object htmlAttributes = null, bool readOnly = false)
        {
            IDictionary<string, object> resulthtmlAttributes = null;
            if (readOnly)
            {
                resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { name = name, value = value, disabled = "" },
                htmlAttributes);
            }
            else
            {
                resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { name = name, value = value },
                htmlAttributes);
            }

            TagBuilder maintag = new TagBuilder("label");
            TagBuilder checkBox = new TagBuilder("input");

            checkBox.AddCssClass(Bc.Web.Mvc.Html.Constants.Style.ElementClass.CheckBoxClass);

            if(readOnly)
                checkBox.AddCssClass(Bc.Web.Mvc.Html.Constants.Style.GeneralClass.DisabledClass);

            checkBox.Attributes.Add("type", "checkbox");
            foreach (var attr in resulthtmlAttributes)
            {
                checkBox.Attributes.Add(attr.Key, Convert.ToString(attr.Value));
            }

            if (isChecked)
                checkBox.Attributes.Add("checked", "checked");

            //Create input hidden
            //maintag.InnerHtml = htmlHelper.CheckBox(name, isChecked, resulthtmlAttributes).ToString();
            maintag.InnerHtml = checkBox.ToString();

            return MvcHtmlString.Create(maintag.ToString());
        }

//        public static MvcHtmlString BcCheckBox(this HtmlHelper htmlHelper, string name, bool isChecked = false, object value = null,
//          object htmlAttributes = null)
//        {
//            IDictionary<string, object> resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
//                new { name = name, value = value },
//                htmlAttributes);

////            <div class="squaredFour">
////    <input type="checkbox" value="None" id="squaredFour" name="check" />
////    <label for="squaredFour"></label>
////</div>

//            TagBuilder maintag = new TagBuilder("div");
//            TagBuilder checkBox = new TagBuilder("input");
//            TagBuilder label = new TagBuilder("label");

//            maintag.AddCssClass(Constants.Style.ElementClass.CheckBoxClass);

//            label.Attributes.Add

//            checkBox.Attributes.Add("type", "checkbox");
//            foreach (var attr in resulthtmlAttributes)
//            {
//                checkBox.Attributes.Add(attr.Key, Convert.ToString(attr.Value));
//            }

//            if (isChecked)
//                checkBox.Attributes.Add("checked", "checked");

//            //Create input hidden
//            //maintag.InnerHtml = htmlHelper.CheckBox(name, isChecked, resulthtmlAttributes).ToString();
//            maintag.InnerHtml = checkBox.ToString();

//            return MvcHtmlString.Create(maintag.ToString());
//        }
    }
}
