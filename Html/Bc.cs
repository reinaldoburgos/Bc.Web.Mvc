//using Bc.Data;
//using Bc.Data.Common;
//using Bc.Data.DbConnection;
//using Bc.Data.Models.Definition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;


namespace Bc.Web.Mvc.Html
{
    public enum ElementThemeType
    {
        [ID("default")]
        Default,
        [ID("danger")]
        Error,
        [ID("primary")]
        Primary,
        [ID("warning")]
        Warning,
        [ID("success")]
        Success,
    }    

    public enum ModalEffect
    {
        [ID("md-effect-1")]
        Default,
        [ID("md-effect-1")]
        FadeIn,
        [ID("md-effect-2")]
        SlideLef,
        [ID("md-effect-3")]
        SlideUp,
        [ID("md-effect-5")]
        Fall,
        [ID("md-effect-6")]
        SideFall,
        [ID("md-effect-7")]
        StickyUp,
        [ID("md-effect-8")]
        FlipHorizontal3D,
        [ID("md-effect-9")]
        FlipVertical3D,
        [ID("md-effect-10")]
        Sign3D,
        [ID("md-effect-11")]
        SuperScaled,
        [ID("md-effect-12")]
        JustModal,
        [ID("md-effect-14")]
        RotateUp3D,
        [ID("md-effect-15")]
        RotateRight3D
    }

    public enum HtmlElementSize
    {
        Small,
        Large,
        Default,
        Mini
    }

    public enum LabelWidgetType
    {
        Warning,
        Error,
        Default,
        Info,
        Inverse,
        Success
    }

    public enum MessageType
    {
        Information = 0,
        Error = 1,
        Warning = 2,
        Confirmation = 3,
        Success = 4
    }

    public enum HtmlColumnSize
    {
        Size_1,
        Size_2,
        Size_3,
        Size_4,
        Size_5,
        Size_6,
        Size_7,
        Size_8,
        Size_9,
        Size_10,
        Size_11,
        Size_12,
        Auto
    }

    public enum ListBoxMode
    {
        Default = 0,
        Tags = 1,
        CheckedBoxes = 2
    }

    [Flags]
    public enum FileGroupExtensions
    {      
        Common,
        Custom,
        Image,
        Audio,
        Video,
        Text,
        MediaType,
        Pdf,
        Html,
        Office
    }

    internal static class Constants
    {
        public class DateFormat
        {
            public static string ServerInputDateTimeFormat
            {
                get
                {
                    return "dd/MM/yyyy HH:mm";
                }
            }

            public static string ServerInputDateFormat 
            {
                get
                {
                    return "dd/MM/yyyy";
                }
            }

            public static string InputDateFormat
            {
                get
                {
                    return "dd/mm/yyyy";
                }
            }
            public static string InputDateTimeFormat
            {
                get
                {
                    return "dd/mm/yyyy HH:mm";
                }
            }
        }

        public static class Style
        {
            public static class ElementClass
            {
                public const string ButtonClass = "btn";
                public const string BlockButtonClass = "btn-block";
                public const string DatePickerClass = "datepicker form-control BcDateInput datetime";
                //  public const string DatePickerClass = "datepicker form-control input-sm BcDateInput datetime";
                public const string FormClass = "form-horizontal";
                public const string TextBoxClass = "form-control";
                public const string ContentCheckBoxClass = "icheckbox_square-blue checkbox";
                public const string CheckBoxClass = "icheck";
                public const string RadioButtonClass = "icheck";
                public const string RadioButtonInlineClass = "radio-inline";
                public const string LabelClass = "control-label";               
                public const string Table = "table";
                public const string SelectClass = "form-control";
            }            

            public static class AlertClass
            {
                public const string BlockAlertClass = "alert alert-block";
                public const string SimpleAlertClass = "alert";

                public const string HeadingBlockAlertClass = "alert-heading";
            }

            public static class AlertTypeClass
            {
                public const string WarningClass = "alert-warning";
                public const string SuccessClass = "alert-success";
                public const string InfoClass = "alert-info";
                public const string ErrorClass = "alert-danger";
            }

            public static class LabelTypeClass
            {
                public const string DefaultClass = "label-default";
                public const string SuccessClass = "label-success";
                public const string InfoClass = "label-info";
                public const string WarningClass = "label-warning";
                public const string InverseClass = "alert-inverse";
                public const string ErrorClass = "label-danger";
            }

            public static class ContentClass
            {
                public const string FormActionClass = "form-actions";
                public const string FormGroupClass = "form-group";
                public const string InputControlClass = "controls";
                public const string InputGroupClass = "input-group";
                public const string InputGroupAddOnClass = "input-group-addon";
                public const string Row = "row";
                public const string TabPane = "tab-pane";                
            }

            public static class GeneralClass
            {
                public const string NoPaddingClass = "no-padding";
                public const string IconClass = "icon";
                public const string InlineMessageValidationClass = "help-inline text-danger";
                public const string DisabledClass = "disabled";
                public const string CloseClass = "close";
                public const string DataTableClass = "data-table";                
            }

            public static class WidgetClass
            {
                public const string BoxClass = "block-flat";
                public const string PanelGroupClass = "panel-group";                
                public const string PanelGroupDefaultClass = "panel panel-default";
                public const string PanelGroupHeadingClass = "panel-heading";                
                public const string PanelTitleClass = "panel-title";
                public const string TitleClass = "title-block";
                public const string ContentClass = "widget-content";
                public const string TabContentClass = "tab-container";
                public const string LabelClass = "label";

            }

            public static class NavClass
            {
                public const string BreadCrumbClass = "breadcrumb";                
            }

            public static class InputSizeClass
            {
                public const string Mini = "input-xs";
                public const string Small = "input-sm";
                public const string Default = "";
                public const string Large = "input-lg";
            }

            public static class ButtonSizeClass
            {
                public const string Mini = "btn-xs";
                public const string Small = "btn-sm";
                public const string Default = "";
                public const string Large = "btn-lg";
            }

            public static class ElementSizeClass
            {
                public const string Mini = "xs";
                public const string Small = "sm";
                public const string Default = "";
                public const string Large = "lg";
            }

            public static class ColumnSizeClass
            {
                public const string Size_1 = "col-sm-1";
                public const string Size_2 = "col-sm-2";
                public const string Size_3 = "col-sm-3";
                public const string Size_4 = "col-sm-4";
                public const string Size_5 = "col-sm-5";
                public const string Size_6 = "col-sm-6";
                public const string Size_7 = "col-sm-7";
                public const string Size_8 = "col-sm-8";
                public const string Size_9 = "col-sm-9";
                public const string Size_10 = "col-sm-10";
                public const string Size_11 = "col-sm-11";
                public const string Size_12 = "col-sm-12";
            }
        }
    }

    public static class BcHelper
    {
        public static string GetButtonSizeClass(HtmlElementSize size)
        {
            string styleClass = "";
            switch (size)
            {
                case HtmlElementSize.Default:
                    styleClass = Constants.Style.ButtonSizeClass.Default;
                    break;
                case HtmlElementSize.Mini:
                    styleClass = Constants.Style.ButtonSizeClass.Mini;
                    break;
                case HtmlElementSize.Small:
                    styleClass = Constants.Style.ButtonSizeClass.Small;
                    break;
                case HtmlElementSize.Large:
                    styleClass = Constants.Style.ButtonSizeClass.Large;
                    break;
            }
            return styleClass;
        }

        public static string GetElementSizeClass(HtmlElementSize size)
        {
            string styleClass = "";
            switch (size)
            {
                case HtmlElementSize.Default:
                    styleClass = Constants.Style.ElementSizeClass.Default;
                    break;
                case HtmlElementSize.Mini:
                    styleClass = Constants.Style.ElementSizeClass.Mini;
                    break;
                case HtmlElementSize.Small:
                    styleClass = Constants.Style.ElementSizeClass.Small;
                    break;
                case HtmlElementSize.Large:
                    styleClass = Constants.Style.ElementSizeClass.Large;
                    break;
            }
            return styleClass;
        }

        public static string GetColumnSizeClass(HtmlColumnSize size)
        {
            string styleClass = "";
            switch (size)
            {
                case HtmlColumnSize.Size_1:
                    styleClass = Constants.Style.ColumnSizeClass.Size_1;
                    break;
                case HtmlColumnSize.Size_2:
                    styleClass = Constants.Style.ColumnSizeClass.Size_2;
                    break;
                case HtmlColumnSize.Size_3:
                    styleClass = Constants.Style.ColumnSizeClass.Size_3;
                    break;
                case HtmlColumnSize.Size_4:
                    styleClass = Constants.Style.ColumnSizeClass.Size_4;
                    break;
                case HtmlColumnSize.Size_5:
                    styleClass = Constants.Style.ColumnSizeClass.Size_5;
                    break;
                case HtmlColumnSize.Size_6:
                    styleClass = Constants.Style.ColumnSizeClass.Size_6;
                    break;
                case HtmlColumnSize.Size_7:
                    styleClass = Constants.Style.ColumnSizeClass.Size_7;
                    break;
                case HtmlColumnSize.Size_8:
                    styleClass = Constants.Style.ColumnSizeClass.Size_8;
                    break;
                case HtmlColumnSize.Size_9:
                    styleClass = Constants.Style.ColumnSizeClass.Size_9;
                    break;
                case HtmlColumnSize.Size_10:
                    styleClass = Constants.Style.ColumnSizeClass.Size_10;
                    break;
                case HtmlColumnSize.Size_11:
                    styleClass = Constants.Style.ColumnSizeClass.Size_11;
                    break;
                case HtmlColumnSize.Size_12:
                    styleClass = Constants.Style.ColumnSizeClass.Size_12;
                    break;
                case HtmlColumnSize.Auto:
                    styleClass = Constants.Style.ColumnSizeClass.Size_1;
                    break;

            }
            return styleClass;
        }

        public static string GetAlertTypeClass(MessageType messageType)
        {
            string styleClass = "";
            switch (messageType)
            {
                case MessageType.Information:
                    styleClass = Constants.Style.AlertTypeClass.InfoClass;
                    break;
                case MessageType.Warning:
                    styleClass = Constants.Style.AlertTypeClass.WarningClass;
                    break;
                case MessageType.Error:
                    styleClass = Constants.Style.AlertTypeClass.ErrorClass;
                    break;
                case MessageType.Success:
                    styleClass = Constants.Style.AlertTypeClass.SuccessClass;
                    break;
                default:
                    styleClass = Constants.Style.AlertTypeClass.InfoClass;
                    break;
            }
            return styleClass;
        }

        public static string GetLabelTypeClass(LabelWidgetType type)
        {
            string styleClass = "";
            switch (type)
            {
                case LabelWidgetType.Info:
                    styleClass = Constants.Style.LabelTypeClass.InfoClass;
                    break;
                case LabelWidgetType.Warning:
                    styleClass = Constants.Style.LabelTypeClass.WarningClass;
                    break;
                case LabelWidgetType.Error:
                    styleClass = Constants.Style.LabelTypeClass.ErrorClass;
                    break;
                case LabelWidgetType.Success:
                    styleClass = Constants.Style.LabelTypeClass.SuccessClass;
                    break;
                case LabelWidgetType.Inverse:
                    styleClass = Constants.Style.LabelTypeClass.InverseClass;
                    break;
                default:
                    styleClass = Constants.Style.LabelTypeClass.DefaultClass;
                    break;
            }
            return styleClass;
        }      
    }
}
