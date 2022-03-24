
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class ButtonExtensions
    {
        internal static string ButtonString(string text, HtmlElementSize size, bool includeText = true, 
            string iconClass = null, bool submit = false, ElementThemeType themeType = ElementThemeType.Primary,
            bool blockButton = false, string actionName = null, string toolTip = null,
            string customButtonClick = null, string target = null, object htmlAttributes = null, bool allowCloseModal = false, 
            string targetModal = null)
        {
            
            TagBuilder tagButton = null;
            

            if (submit)
            {
                tagButton = new TagBuilder("input");
                tagButton.Attributes.Add("type", "submit");        
                tagButton.Attributes.Add("value",text);
            }
            else
            {
                tagButton = new TagBuilder("button");
                string icon = "";
                if(!string.IsNullOrWhiteSpace(iconClass))
                    icon = IconExtensions.BcIcon(iconClass).ToString();
                
                if (!string.IsNullOrWhiteSpace(icon) && !string.IsNullOrWhiteSpace(text))
                    icon += " ";

                tagButton.InnerHtml = string.Format("{0}{1}", icon, (includeText ? text : ""));
            }

            //Before attr class
            if (htmlAttributes != null)
            {
                var attrs = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var attr in attrs)
                {
                    tagButton.Attributes.Add(attr.Key, attr.Value.ToString());
                }
            }

            if (allowCloseModal)
            {
                tagButton.Attributes.Add("data-dismiss","modal");
                tagButton.AddCssClass("md-close");
                if (string.IsNullOrWhiteSpace(customButtonClick))
                {
                    tagButton.Attributes.Add("onclick", "return false;");
                }
            }

            tagButton.AddCssClass("btn-" + themeType.GetID());

            if (!string.IsNullOrWhiteSpace(targetModal))
            {
                tagButton.Attributes.Add("data-modal", targetModal);
                if(string.IsNullOrEmpty(customButtonClick))
                {
                    customButtonClick = "return false;";
                }
                tagButton.AddCssClass("md-trigger");
            }
            
            if (!string.IsNullOrEmpty(customButtonClick))
                tagButton.Attributes.Add("onclick", customButtonClick);
            else if (!string.IsNullOrEmpty(target))
                tagButton.Attributes.Add("onclick", "$('" + target + "').submit();");

            if (!String.IsNullOrEmpty(toolTip))
                tagButton.Attributes.Add("title", toolTip);
            
            if(!string.IsNullOrEmpty(actionName))
                tagButton.Attributes.Add("action", actionName);

            tagButton.AddCssClass(Constants.Style.ElementClass.ButtonClass);
            if(blockButton)
                tagButton.AddCssClass(Constants.Style.ElementClass.BlockButtonClass);

            tagButton.AddCssClass(BcHelper.GetButtonSizeClass(size));                        

            return tagButton.ToString();            
        }
        
        public static MvcHtmlString BcTextButton(this HtmlHelper htmlHelper, string text, HtmlElementSize size = HtmlElementSize.Default,
            bool submit = false, ElementThemeType themeType = ElementThemeType.Primary, bool blockButton = false,
            object htmlAttributes = null, bool modalButton = false, string targetModal = null)
        {            
            return MvcHtmlString.Create(ButtonString(text, size, includeText: true, iconClass: null, submit: submit,
                themeType : themeType, blockButton: blockButton, htmlAttributes: htmlAttributes, 
                allowCloseModal: modalButton, targetModal: targetModal));
        }        

        public static MvcHtmlString BcIconButton(this HtmlHelper htmlHelper, string iconClass, HtmlElementSize size = HtmlElementSize.Default,
            bool submit = false, ElementThemeType themeType = ElementThemeType.Primary,
            bool blockButton = false, object htmlAttributes = null, bool allowCloseModal = false, string targetModal = null)
        {
            return MvcHtmlString.Create(ButtonString(null, size, includeText: false, iconClass: iconClass, submit: submit, 
                themeType : themeType,blockButton: blockButton, htmlAttributes: htmlAttributes,
                allowCloseModal: allowCloseModal, targetModal: targetModal));
        }

        public static MvcHtmlString BcIconButton(this HtmlHelper htmlHelper, Icons icon, HtmlElementSize size = HtmlElementSize.Default,
           bool submit = false, ElementThemeType themeType = ElementThemeType.Primary, bool blockButton = false,
            object htmlAttributes = null, bool allowCloseModal = false, string targetModal = null)
        {
            return MvcHtmlString.Create(ButtonString(null, size, includeText: false, iconClass: icon.GetID(), submit: submit, themeType : themeType,
                blockButton: blockButton, htmlAttributes: htmlAttributes, allowCloseModal: allowCloseModal, targetModal: targetModal));
        }

        public static MvcHtmlString BcButton(this HtmlHelper htmlHelper, string text, string iconClass, HtmlElementSize size = HtmlElementSize.Default,
            bool includeText = true, bool submit = false,ElementThemeType themeType = ElementThemeType.Primary,
            bool blockButton = false, string actionName = null, string customButtonClick = null, string target = null,
            object htmlAttributes = null, bool allowCloseModal = false, string targetModal = null)
        {
            return MvcHtmlString.Create(ButtonString(text, size, includeText: true, iconClass: iconClass, submit: submit, themeType : themeType,blockButton: blockButton,
                actionName: actionName, customButtonClick: customButtonClick, target: target, htmlAttributes : htmlAttributes,
                allowCloseModal: allowCloseModal, targetModal: targetModal));
        }

        public static MvcHtmlString BcButton(this HtmlHelper htmlHelper, string text, HtmlElementSize size = HtmlElementSize.Default,
            Icons? icon = null, bool includeText = true, bool submit = false,ElementThemeType themeType = ElementThemeType.Primary,
            bool blockButton = false, string actionName = null, string customButtonClick = null, string target = null,
            object htmlAttributes = null, bool allowCloseModal = false, string targetModal = null)
        {
            return MvcHtmlString.Create(ButtonString(text, size, includeText: true, iconClass: icon==null?"":icon.GetID(), submit: submit, themeType : themeType, blockButton: blockButton,
                actionName: actionName, customButtonClick: customButtonClick, target: target,
                htmlAttributes: htmlAttributes, allowCloseModal: allowCloseModal, targetModal: targetModal));
        }

     //   public static MvcHtmlString BcActionButton(this HtmlHelper helper, string text, string action, string controller, object routeValues = null,
     //Bc.Web.Mvc.Html.HtmlElementSize size = HtmlElementSize.Default, bool includeText = true, Icons? icon = null, ElementThemeType themeType = ElementThemeType.Primary)
     //   {
     //       StringBuilder button = new StringBuilder();
           
     //           button.AppendFormat("<a href=\"{0}\">{1}</a>", Bc.Web.Mvc.Utility.UrlHelper.GetFromContext().Action(
     //               action,
     //               controller, routeValues), ButtonString(text, size, includeText, iconClass: icon == null ? "" : icon.GetID(), themeType: themeType));


     //           return MvcHtmlString.Create(button.ToString());
     //   }

        
        //public static MvcHtmlString BcBasicButton(this HtmlHelper htmlHelper, ActivityActions activityAction, 
        //    ElementThemeType? themeType = null, 
        //    HtmlElementSize size = HtmlElementSize.Default,            
        //    string actionName = null, string customButtonClick = null, string target = null,
        //    object htmlAttributes = null, bool allowCloseModal = false, string targetModal = null, bool submit = false)
        //{
        //    string text = "";
        //    Icons icon = Icons.Circle;            
        //    ElementThemeType defaultTheme = ElementThemeType.Default;
        //    switch(activityAction)
        //    {
        //        case ActivityActions.Approve:
        //            text = Bc.Resources.LabelFor.Approve;
        //            icon = Icons.Check;
        //            defaultTheme = ElementThemeType.Success;
        //            break;
        //        case ActivityActions.Cancel:
        //            text = Bc.Resources.LabelFor.Cancel;
        //            icon = Icons.Cancel;
        //            defaultTheme = ElementThemeType.Default;
        //            break;
        //        case ActivityActions.Continue:
        //            text = Bc.Resources.LabelFor.Continue;
        //            icon = Icons.ArrowRight;
        //            defaultTheme = ElementThemeType.Primary;
        //            break;
        //        case ActivityActions.Create:
        //            text = Bc.Resources.LabelFor.Create;
        //            icon = Icons.Add;
        //            defaultTheme = ElementThemeType.Success;
        //            break;
        //        case ActivityActions.Delete:
        //            text = Bc.Resources.LabelFor.Delete;
        //            icon = Icons.Trash;
        //            defaultTheme = ElementThemeType.Error;
        //            break;
        //        case ActivityActions.Login:
        //            text = Bc.Resources.LabelFor.Login;
        //            icon = Icons.ArrowsV;
        //            defaultTheme = ElementThemeType.Primary;
        //            break;
        //        case ActivityActions.Modify:
        //            text = Bc.Resources.LabelFor.Modify;
        //            icon = Icons.Edit;
        //            defaultTheme = ElementThemeType.Success;
        //            break;
        //        case ActivityActions.Nullify:
        //            text = Bc.Resources.LabelFor.Nullify;
        //            icon = Icons.Remove;
        //            defaultTheme = ElementThemeType.Error;
        //            break;
        //        case ActivityActions.Open:
        //            text = Bc.Resources.LabelFor.Open;
        //            icon = Icons.Open;
        //            defaultTheme = ElementThemeType.Default;
        //            break;
        //        case ActivityActions.Save:
        //            text = Bc.Resources.LabelFor.Save;
        //            icon = Icons.Save;
        //            defaultTheme = ElementThemeType.Success;
        //            break;
        //        case ActivityActions.Send:
        //            text = Bc.Resources.LabelFor.SendDate;
        //            icon = Icons.Send;
        //            defaultTheme = ElementThemeType.Success;
        //            break;
        //        case ActivityActions.Update:
        //            text = Bc.Resources.LabelFor.Update;
        //            icon = Icons.Save;
        //            defaultTheme = ElementThemeType.Success;
        //            break;
        //        case ActivityActions.Upload:
        //            text = Bc.Resources.LabelFor.Upload;
        //            icon = Icons.CloudUpload;
        //            defaultTheme = ElementThemeType.Default;
        //            break;
        //        case ActivityActions.Back:
        //            text = Bc.Resources.LabelFor.Back;
        //            icon = Icons.ArrowLeft;
        //            defaultTheme = ElementThemeType.Default;
        //            break;
        //        case ActivityActions.Refresh:
        //            text = Bc.Resources.LabelFor.Refresh;
        //            icon = Icons.Refresh;
        //            defaultTheme = ElementThemeType.Default;
        //        break;
        //        case ActivityActions.Query:
        //            text = Bc.Resources.LabelFor.Query;
        //            icon = Icons.Refresh;
        //            defaultTheme = ElementThemeType.Default;
        //            break;
        //        case ActivityActions.Excel:
        //            text = Bc.Resources.LabelFor.Excel;
        //            icon = Icons.Excel;
        //            defaultTheme = ElementThemeType.Default;
        //            break;
        //        case ActivityActions.Add:
        //            text = Bc.Resources.LabelFor.Add;
        //            icon = Icons.Add;
        //            defaultTheme = ElementThemeType.Primary;
        //            break;
        //    }

        //    if(themeType.HasValue)
        //        defaultTheme = themeType.Value;

        //    return MvcHtmlString.Create(ButtonString(text, size, includeText: true, iconClass: icon.GetID(), submit: submit, 
        //        themeType: defaultTheme, blockButton: false,
        //        actionName: actionName, customButtonClick: customButtonClick, target: target,
        //        htmlAttributes: htmlAttributes, allowCloseModal: allowCloseModal, targetModal: targetModal));
        //}
    }
}
