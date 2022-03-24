using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class StageExtensions
    {
        public static void BcRenderStage(this HtmlHelper htmlHelper, int score)
        {
            string html = StageString(score, null);
            htmlHelper.ViewContext.Writer.Write(html);           
        }

        public static MvcHtmlString BcStage(this HtmlHelper htmlHelper, int score)
        {
            string html = StageString(score, null);
            return MvcHtmlString.Create(html);
        }
        public static MvcHtmlString BcStage(this HtmlHelper htmlHelper, int score, string target)
        {
            string html = StageString(score, target);

            return MvcHtmlString.Create(html);
        }

        private static string StageString(int score, string target)
        {
            bool readOnly = string.IsNullOrWhiteSpace(target);

            StringBuilder html = new StringBuilder();
            html.AppendLine("<center>");
            html.AppendFormat("<div class=\"{0}\">", "content_stage_ranking");

            html.AppendFormat("<div stage-value=\"{1}\" class=\"{0}\">", "stage_all", score);

            for (int i = 1; i <= 5; i++)
            {
                html.AppendFormat("<div class=\"stage_content\"><i class=\"fa stage_icon\" style=\"background-color:{0}\"></i><strong class=\"stage_label\">{1}</strong></div>", GetColor(i, score), i);
            }

            html.Append("</div></div></center>");

            return html.ToString();
        }

        public static string GetColor(int i, int score) 
        {
            if (i > score)
                return "#727375";

            switch (i)
            {
                case 1: return "#CE4451";
                case 2: return "#F36E2F";
                case 3: return "#F49630";
                case 4: return "#FDCF4E";
                case 5: return "#17B365";
            }

            return "";
        }
    }
}
