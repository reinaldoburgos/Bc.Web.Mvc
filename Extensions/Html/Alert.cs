using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class AlertExtensions
    {
        public static MvcHtmlString BcAlertBlock(this HtmlHelper htmlHelper, string message, Bc.Web.Mvc.Html .MessageType type = MessageType.Information,
            string title = null, bool closeButton = false)
        {
            TagBuilder alertBlock = new TagBuilder("div");
            alertBlock.AddCssClass(Constants.Style.AlertClass.BlockAlertClass);
            alertBlock.AddCssClass(BcHelper.GetAlertTypeClass(type));

            string closeButtonHtml = " ";
            if (closeButton)
            {
                TagBuilder closeButtonBlock = new TagBuilder("a");
                closeButtonBlock.AddCssClass(Constants.Style.GeneralClass.CloseClass);
                closeButtonBlock.Attributes.Add("data-dismiss", "alert");
                closeButtonBlock.Attributes.Add("href", "#");

                closeButtonHtml = closeButtonBlock.ToString();
            }

            string titleHtml = " ";
            if (!string.IsNullOrWhiteSpace(title))
            {
                TagBuilder titleBlock = new TagBuilder("h4");
                titleBlock.AddCssClass(Constants.Style.AlertClass.HeadingBlockAlertClass);
                titleBlock.InnerHtml = title;
                titleHtml = titleBlock.ToString();
            }

            alertBlock.InnerHtml = closeButtonHtml + titleHtml + message;

            return MvcHtmlString.Create(alertBlock.ToString());
        }
    }
}
