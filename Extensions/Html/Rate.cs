using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class RateExtensions
    {
        public static void BcRenderRate(this HtmlHelper htmlHelper, int score)
        {
            string html = RateString(score, null);
            htmlHelper.ViewContext.Writer.Write(html);           
        }

        public static MvcHtmlString BcRate(this HtmlHelper htmlHelper, int score)
        {
            string html = RateString(score, null);
            return MvcHtmlString.Create(html);
        }
        public static MvcHtmlString BcRate(this HtmlHelper htmlHelper, int score, string target)
        {
            string html = RateString(score,target);

            return MvcHtmlString.Create(html);
        }

        private static string RateString(int score, string target)
        {
            bool readOnly = string.IsNullOrWhiteSpace(target);

            StringBuilder html = new StringBuilder();
            html.AppendLine("<center>");
            html.AppendFormat("<div rate-widget-content class=\"{0}\">", "content_star_ranking");
            if (!readOnly)
            {
                html.AppendFormat("<div rate-widget-cancel class=\"{0}\"></div>", "cancel_rate");
            }

            html.AppendFormat("<div rate-widget {2} rate-value=\"{1}\" class=\"{0}\"", "star_all", score, readOnly?"readonly":"");
            if (!readOnly)
            {
                html.AppendFormat("target=\"{0}\"", target);
            }

            html.Append(">");

            for (int i = 1; i <= 5; i++)
            {
                html.AppendFormat("<div rate-widget-step rate-value=\"{0}\" class=\"{1}\"></div>", i, i > score ? "star_empty" : "star_full");
            }

            html.Append("</div></div></center>");

            return html.ToString();
        }
    }
}
