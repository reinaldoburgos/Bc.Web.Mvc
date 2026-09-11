using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class ModalExtensions
    {
        public static MvcHtmlString BcModalHiddenTrigger(this HtmlHelper htmlHelper, string id, string target)
        {
            TagBuilder tag = new TagBuilder("button");
            tag.AddCssClass("md-trigger");
            tag.Attributes.Add("style", "display:none");
            tag.Attributes.Add("data-modal", target);
            tag.Attributes.Add("id", id);
            tag.Attributes.Add("onclick", "return false;");
            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString BcModalOverlay(this HtmlHelper htmlHelper)
        {
            return MvcHtmlString.Create("<div class=\"md-overlay\"></div>");
        }

        public static MvcContent BcBeginModal(this HtmlHelper htmlHelper, string id, string title = null,
            ElementThemeType themeType = ElementThemeType.Primary, ModalEffect effect = ModalEffect.Fall,
            int? customWidth = null, bool isPercent = true, int? customMaxWidth = null, bool includeOverlay = true, bool isClosable = true,
            int? zindex = null)
        {
            return new MvcContent(
                () => htmlHelper.BeginModal(id: id, title: title, themeType: themeType, effect: effect, customWidth: customWidth,
                           isPercent: isPercent, customMaxWidth: customMaxWidth, isClosable: isClosable, zindex: zindex),
                () => htmlHelper.EndModal(includeOverlay)
            );
        }


        internal static void BeginModal(this HtmlHelper htmlHelper, string id, string title = null,
            ElementThemeType themeType = ElementThemeType.Primary, ModalEffect effect = ModalEffect.Default,
            int? customWidth = null, bool isPercent = true, int? customMaxWidth = null, bool isClosable = true, int? zindex = null)
        {
            StringBuilder html = new StringBuilder();
            string customWidthStyle = "";
            string displayClosable = "none";
            string maxWidth = "90%";
            string Width = "90%";
            string customZIndex = "";

            if (customWidth.HasValue)
                Width = Convert.ToString(customWidth) + (isPercent ? "%" : "px");

            if (customMaxWidth.HasValue)
                maxWidth = Convert.ToString(customMaxWidth) + "%";

            // hay que tener cuidado con esta z-index, cuando le di un valor alto dejo de funcionar el Select2
            if (zindex.HasValue)
                customZIndex = $"style=\"z-index: {zindex};\"";

            customWidthStyle = $"style=\"width:{Width}; max-width:{maxWidth}\"";

            if (isClosable)
                displayClosable = "block";

            html.AppendFormat("<div id=\"{0}\" class=\"modal bootstrap-dialog set-dialog type-{2} fade size-normal in \" role=\"dialog\" aria-hidden=\"true\" {1} >", id, customZIndex, themeType.GetID());
            html.AppendFormat("<div class=\"modal-dialog\" {0}>", customWidthStyle);
            html.AppendLine("<div class=\"modal-content\">");
            html.AppendLine("<div class=\"modal-header bootstrap-dialog-draggable\">");
            html.AppendLine("<div class=\"bootstrap-dialog-header\">");
            html.AppendFormat("<div class=\"bootstrap-dialog-close-button\" style=\"display: {0};\">", displayClosable);
            html.AppendLine("<button class=\"close\" data-dismiss=\"modal\">X</button>");
            html.AppendLine("</div>");
            html.AppendLine("<div class=\"bootstrap-dialog-title\">");
            if (!string.IsNullOrWhiteSpace(title))
            {
                html.AppendLine(title);
            }
            html.AppendLine("</div>");
            html.AppendLine(" </div>");
            html.AppendLine(" </div>");

            //html.AppendLine("<div class=\"modal-body\" style=\"display:block;\">");
            //html.AppendFormat("<div class=\"row\" id=\"{0}-content\" style=\"height:auto;\">", id);

            htmlHelper.ViewContext.Writer.Write(html.ToString());
        }

        internal static void EndModal(this HtmlHelper htmlHelper, bool includeOverlay = true)
        {
            htmlHelper.ViewContext.Writer.Write("</div></div></div>" + (includeOverlay ? htmlHelper.BcModalOverlay().ToString() : ""));
        }


        public static MvcContent BcBeginModalBody(this HtmlHelper htmlHelper, string id, int? viewHeight = null)
        {
            return new MvcContent(
                () => htmlHelper.BeginModalBody(id, viewHeight),
                () => htmlHelper.EndModalBody()
            );
        }

        internal static void BeginModalBody(this HtmlHelper htmlHelper, string id, int? viewHeight = null)
        {
            string height = string.Empty;
            if (Convert.ToInt32(viewHeight) > 0)
                height = $"height:{viewHeight}vh; overflow-y:auto";

            StringBuilder html = new StringBuilder();
            html.AppendLine($"<div class=\"modal-body\" style=\"display:block; {height}\">");
            html.AppendFormat("<div class=\"row\" id=\"{0}-content\" style=\"height:auto;\">", id);

            htmlHelper.ViewContext.Writer.Write(html.ToString());
        }

        internal static void EndModalBody(this HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("</div></div>");
        }


        public static MvcContent BcBeginModalAction(this HtmlHelper htmlHelper)
        {
            return new MvcContent(
                () => htmlHelper.BeginModalAction(),
                () => htmlHelper.EndModalAction()
            );
        }

        internal static void BeginModalAction(this HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("<div class=\"modal-footer\">");
        }

        internal static void EndModalAction(this HtmlHelper htmlHelper)
        {
            htmlHelper.ViewContext.Writer.Write("</div>");
        }

    }
}