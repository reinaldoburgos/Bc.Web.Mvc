using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class FormActionGroupExtensions
    {
        internal static void BeginFormActionGroup(this HtmlHelper htmlHelper, HtmlColumnSize marginLeft = HtmlColumnSize.Size_4)
        {
            int labelSize = FormGroupExtensions.GetLabelColSize(marginLeft);
            int controlSize = 12 - labelSize;

            htmlHelper.ViewContext.Writer.Write(string.Format("<div class=\"{0}\"><div class=\"col-sm-{1}\"> </div> <div class=\"col-sm-{2}\"> <div class=\"{3}\">",
                Bc.Web.Mvc.Html.Constants.Style.ContentClass.FormGroupClass,
                labelSize,
                controlSize,
                Constants.Style.ContentClass.FormActionClass));
        }

        internal static void EndFormActionGroup(this HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("</div></div></div>");
        }

        public static MvcContent BcBeginFormActionGroup(this HtmlHelper htmlHelper, HtmlColumnSize marginLeft = HtmlColumnSize.Size_4)
        {
            return new MvcContent(
                () => htmlHelper.BeginFormActionGroup(marginLeft),
                () => htmlHelper.EndFormActionGroup()
            );
        }

        //public static MvcHtmlString BcFormActionGroup(this HtmlHelper htmlHelper, params ActivityActions[] activityActions)
        //{
        //    return htmlHelper.BcFormActionGroup(HtmlColumnSize.Size_4, activityActions.Select(p => p.GetID()).ToArray());
        //}

        //public static MvcHtmlString BcFormActionGroup(this HtmlHelper htmlHelper, HtmlColumnSize marginLeft, params ActivityActions[] activityActions)
        //{
        //    return htmlHelper.BcFormActionGroup(marginLeft, activityActions.Select(p => p.GetID()).ToArray());
        //}

        //public static MvcHtmlString BcFormActionGroup(this HtmlHelper htmlHelper, HtmlColumnSize marginLeft, params string[] activityActions)
        //{
        //    ApplicationProxy applicationProxy = new ApplicationProxy();

        //    IEnumerable<Bc.Data.Common.Models.Definition.ActivityAction> modelActivityActions =
        //        applicationProxy.GetActivityActions(activityActions);

        //    StringBuilder builderButton = new StringBuilder();
        //    for (int i = 0; i < activityActions.Count(); i++)
        //    {
        //        Bc.Data.Common.Models.Definition.ActivityAction activityAction = modelActivityActions.FirstOrDefault(p => p.ActivityActionId == activityActions[i]);
        //        builderButton.Append(htmlHelper.BcActivityActionButton(activityAction, submit: true).ToString() + " ");
        //    }

        //    return htmlHelper.FormGroupEditor(builderButton.ToString(), labelSize: marginLeft);
        //}
    }
}