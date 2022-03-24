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
            ElementThemeType themeType = ElementThemeType.Primary, ModalEffect effect = ModalEffect.Fall, int? customWidth = null, bool includeOverlay = true, bool isClosable = true)
        {
            return new MvcContent(
                () => htmlHelper.BeginModal(id, title, themeType, effect, customWidth, isClosable),
                () => htmlHelper.EndModal(includeOverlay)
            );
        }

        internal static void BeginModal(this HtmlHelper htmlHelper, string id, string title = null,
            ElementThemeType themeType = ElementThemeType.Primary, ModalEffect effect = ModalEffect.Default, int? customWidth = null, bool isClosable = true)
        {
            StringBuilder html = new StringBuilder();
            string customWidthStyle = "";
            string displayClosable = "none";
            if (customWidth.HasValue)
                customWidthStyle = string.Format("style=\"width:{0}px; max-width:{0}px;\"", customWidth);

            if (isClosable)
                displayClosable = "block";

            //html.AppendFormat("<div class=\"md-modal {1} {2} {3}\" {4} id=\"{0}\" >", id,
            //     string.IsNullOrWhiteSpace(title) ? "" : "colored-header", effect.GetID(), themeType.GetID(), customWidthStyle);
            ////html.AppendLine("<div class=\"modal-dialog\">");
            //html.AppendLine("<div class=\"md-content\" " + customWidthStyle + ">");
            //html.AppendLine("<div class=\"modal-content\" " + customWidthStyle + ">");
            //html.AppendLine("<div class=\"modal-header\">");


            html.AppendFormat("<div id=\"{0}\" class=\"modal bootstrap-dialog set-dialog type-primary fade size-normal in \" role=\"dialog\" aria-hidden=\"true\" >", id);
            html.AppendFormat("<div class=\"modal-dialog\" {0}>", customWidthStyle);
            html.AppendLine("<div class=\"modal-content\">");

            html.AppendLine("<div class=\"modal-header bootstrap-dialog-draggable\">");
            html.AppendLine("<div class=\"bootstrap-dialog-header\">");
            html.AppendFormat("<div class=\"bootstrap-dialog-close-button\" style=\"display: {0};\">", displayClosable);
            html.AppendLine("<button class=\"close\" data-dismiss=\"modal\">×</button>");
            html.AppendLine("</div>");
            html.AppendLine("<div class=\"bootstrap-dialog-title\">");
            if (!string.IsNullOrWhiteSpace(title))
                html.AppendLine(title);
            html.AppendLine("</div>");
            html.AppendLine(" </div>");
            html.AppendLine(" </div>");

            html.AppendLine("<div class=\"modal-body\">");
            html.AppendLine("<div class=\"bootstrap-dialog-body\">");
            html.AppendLine("<div class=\"bootstrap-dialog-message\">");


            html.AppendFormat("<div id=\"{0}-content\">", id);

            // html.AppendLine("Esta seguro que desea transferir");
            html.AppendLine("</div>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");


            //html.AppendLine("<div class=\"modal-footer\" style=\"display: block;\">");
            //html.AppendLine("<div class=\"bootstrap-dialog-footer\">");
            //html.AppendLine("<div class=\"bootstrap-dialog-footer-buttons\">");
            //html.AppendLine("<button class=\"btn btn-default\" >Cancelar</button>");
            //html.AppendLine("<button class=\"btn btn-primary\" >Grabar</button>");
            //html.AppendLine("</div>");
            //html.AppendLine("</div>");

            //html.AppendLine("</div>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");
    






            /*
             
             <div class="modal bootstrap-dialog set-dialog type-primary fade size-normal in" role="dialog" aria-hidden="true"
     id="f97083f5-57fa-43fa-957c-bd35ff56a58f" aria-labelledby="f97083f5-57fa-43fa-957c-bd35ff56a58f_title" tabindex="-1"
     style="z-index: 1050; display: block; padding-right: 17px;">
    <div class="modal-dialog" style="top: 179px; left: 21px;">
        <div class=\"modal-content\">
            <div class=\"modal-header bootstrap-dialog-draggable\">
                <div class=\\"bootstrap-dialog-header\\">
                    <div class=\"bootstrap-dialog-close-button\" style=\"display: none;\">
                        <button class=\"close\">×</button>
                    </div>
                    <div class=\"bootstrap-dialog-title\" id=\"f97083f5-57fa-43fa-957c-bd35ff56a58f_title\">
                        Transferir ticket
                    </div>
                </div>
            </div>
            <div class=\"modal-body\">
                <div class=\"bootstrap-dialog-body\">
                    <div class=\"bootstrap-dialog-message\">
                        Esta seguro que desea transferir
                    </div>
                </div>
            </div>
            <div class=\"modal-footer\" style=\"display: block;\">
                <div class=\"bootstrap-dialog-footer\">
                    <div class=\"bootstrap-dialog-footer-buttons\">
                        <button class=\"btn btn-default\" id=\"43c95d8f-bf6b-4b5d-b853-961b12d561ae\">No</button>
                        <button class=\"btn btn-primary\" id=\"ad6e1a73-1d8a-48af-96b9-5e17381a890c\">Si</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
             
             
             */





            // < div id = 'myModal' class='modal' style="text-align:right;">
            //    <div class="modal-dialog" style="width:900px; height:400px; padding:10px;">
            //        <div class="modal-content" style="overflow: auto; padding:10px; background-color:#d2f5f4;">
            //            <button type = "button" id="closbtn" onclick="$('#myModal').modal('hide');">x</button>
            //            <div style = "height:10px;" >
            //            </ div >
            //            < div id='myModalContent' style="width:850px; height:400px; padding:10px;">
            //            </div>
            //        </div>
            //    </div>
            //</div>

            //if (!string.IsNullOrWhiteSpace(title))
            //{
            //    html.AppendFormat("<h3>{0}</h3>", title);
            //}

            //html.AppendLine("<button type=\"button\" class=\"close md-close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>");


            //html.AppendLine("</div>"); //header            
            //html.AppendLine("<div class=\"modal-body\">");

            //string trigger = htmlHelper.BcModalHiddenTrigger(id + "-trigger", id).ToString();

            // htmlHelper.ViewContext.Writer.Write(trigger + html.ToString());
            htmlHelper.ViewContext.Writer.Write(html.ToString());
        }

        internal static void EndModal(this HtmlHelper htmlHelper, bool includeOverlay = true)
        {
            //htmlHelper.ViewContext.Writer.Write("</div></div></div>" + (includeOverlay ? htmlHelper.BcModalOverlay().ToString() : ""));
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