using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Bc.Web.Mvc.Html
{
    public static class FormExtensions
    {
        internal static void BcEndForm(this System.Web.Mvc.HtmlHelper htmlHelper, string[] activityActions = null)
        {
            //if (activityActions == null)
            //{

            //}
            MvcHtmlString antiForgeryToken = htmlHelper.AntiForgeryToken();

            htmlHelper.ViewContext.Writer.Write(antiForgeryToken.ToString() + "</form>");
        }

        public static MvcContent BcBeginForm(this HtmlHelper htmlHelper, string actionName = null,
            string controllerName = null, object routeValues = null, object htmlAttributes = null,
            FormMethod method = FormMethod.Post, bool includeAction = true, string[] activityActions = null)
        {
            var resulthtmlAttributes = Bc.Web.Mvc.Utility.HtmlHelper.MergeAnonymousObjectHtmlAttributes(
                new { @class = Constants.Style.ElementClass.FormClass },
                htmlAttributes);

            RouteValueDictionary route = routeValues != null ?
                System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(routeValues) : null;

            return new MvcContent(
                () =>
                htmlHelper.BeginForm(actionName, controllerName, route, method, htmlAttributes: resulthtmlAttributes),
                () => htmlHelper.BcEndForm()
            );
        }

        //public static MvcContent BcBeginForm(this HtmlHelper htmlHelper, ActivityAction[] activityActions)
        //{
        //    string[] activityActionKeys = new string[activityActions.Count];

        //}


        //public static class FormActions(this HtmlHelper htmlHelper)
        //{

        //}
    }
}
