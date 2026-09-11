using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bc.Web.Mvc.Helper
{
    public class Utils
    {
        public static string ReplaceNameAttributes<TModel, TValue>(
            HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string originalHtml,
            IDictionary<string, object> htmlAttributes)
        {
            if (htmlAttributes.ContainsKey("name"))
            {
                var nameValue = htmlAttributes["name"].ToString();
                // Reemplazar el atributo name en el HTML original
                originalHtml = originalHtml.Replace($"name=\"{htmlHelper.NameFor(expression)}\"", $"name=\"{nameValue}\"");
                // Reemplazar la referencia al nombre en el mensaje de validación, si es aplicable
                originalHtml = originalHtml.Replace($"data-valmsg-for=\"{htmlHelper.NameFor(expression)}\"", $"data-valmsg-for=\"{nameValue}\"");
            }

            return originalHtml;
        }
    }
}