using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Runtime.CompilerServices;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.Common;
using System.Web;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using Bc.Data.DbConnection;
using Bc.Data.Common.Models.Definition;

using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Data.Linq;
using DevExpress.Data.Linq.Helpers;
using System.Linq.Expressions;
using Bc.Web.Proxies;

namespace Bc.Web.Mvc.Html
{
    public class ExtensionsFactory
    {
        private static ExtensionsFactory _Instance;
        public static ExtensionsFactory Instance
        {
            get
            {
                if (_Instance == null) _Instance = new ExtensionsFactory();
                return _Instance;
            }
        }

        internal HtmlHelper HtmlHelper { get; set; }




        public GridViewExtension GridView(Action<GridViewSettings> method, List<OptionAction> optionActions = null, List<CustomGridViewAction> customActions = null,
            bool autoFixedHeight = false, int? autoFixedHeightMarginTop = null)
        {
            GridViewSettings settings = new GridViewSettings();
            SetDefaultGridViewSettings(settings, optionActions, customActions);
            method(settings);
            
            return GridView(settings, optionActions, customActions, autoFixedHeight : autoFixedHeight, autoFixedHeightMarginTop : autoFixedHeightMarginTop);
        }

        public static void SetCustomButtonClickMode(GridViewSettings settings, bool samePage)
        {
            if(samePage)
                settings.ClientSideEvents.CustomButtonClick = "function(s, e) { document.location= e.buttonID + '/' + s.GetRowKey(e.visibleIndex); }";
            else 
                settings.ClientSideEvents.CustomButtonClick = "function(s, e) { var value = s.GetRowKey(e.visibleIndex); if(value) { window.open(e.buttonID + '/' + s.GetRowKey(e.visibleIndex), '_blank'); } }";
        }

        private static void SetDefaultGridViewSettings(GridViewSettings settings, List<OptionAction> optionActions = null, List<CustomGridViewAction> customOptionActions = null)
        {
            //settings.ClientSideEvents.CustomButtonClick = "function(s, e) { document.location= e.buttonID + '/' + s.GetRowKey(e.visibleIndex); }";
            settings.ClientSideEvents.CustomButtonClick = "function(s, e) { var value = s.GetRowKey(e.visibleIndex); if(value) { window.open(e.buttonID + '/' + s.GetRowKey(e.visibleIndex), '_blank'); } }";

            settings.Settings.HorizontalScrollBarMode = ScrollBarMode.Hidden;
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);

            settings.CommandColumn.ButtonType = GridCommandButtonRenderMode.Image;
            settings.Styles.CommandColumn.Spacing = new Unit(4, UnitType.Pixel);

            settings.Settings.ShowFilterRow = true;
            settings.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;

            settings.Styles.Header.BackgroundImage.ImageUrl = "none";
            settings.Styles.Header.BackColor = System.Drawing.ColorTranslator.FromHtml("#FBF7EA");
            settings.Styles.Header.Font.Bold = true;

            settings.Styles.Row.Wrap = DevExpress.Utils.DefaultBoolean.False;

            settings.SettingsPager.Position = System.Web.UI.WebControls.PagerPosition.Bottom;
            settings.SettingsPager.FirstPageButton.Visible = true;
            settings.SettingsPager.LastPageButton.Visible = true;            
            settings.SettingsPager.PageSizeItemSettings.Visible = true;
            settings.SettingsPager.PageSizeItemSettings.Items = new string[] { "10", "15","20", "50","100", "200", "500", "800", "1000" };
            settings.SettingsPager.PageSize = 20;

            if (optionActions != null || customOptionActions != null)
            {
                settings.CommandColumn.Visible = true;
                settings.CommandColumn.Width = 110;
                settings.CommandColumn.FixedStyle = GridViewColumnFixedStyle.Left;
            }            
        }

        private static void SetAutoFilterColumns(GridViewSettings settings)
        {
            foreach (GridViewColumn col in settings.Columns)
            {
                if (col is MVCxGridViewColumn)
                    ((MVCxGridViewColumn)col).Settings.AutoFilterCondition = AutoFilterCondition.Contains;

                if (col is MVCxGridViewBandColumn)
                {
                    foreach(GridViewColumn colBand in ((MVCxGridViewBandColumn)col).Columns)
                    {
                        if (colBand is MVCxGridViewColumn)
                            ((MVCxGridViewColumn)colBand).Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
            }
        }

        public static GridViewExtension GridView(GridViewSettings settings, List<OptionAction> optionActions = null, List<CustomGridViewAction> customOptionActions = null,
            bool autoFixedHeight = false, int? autoFixedHeightMarginTop = null)
        {
            if (optionActions != null)
            {
                foreach (var action in optionActions)
                {
                    var button = new BcGridViewCommandColumnCustomButton();

                    button.SetOptionActivityAction(action.OptionId, action.ActivityId, null, action.ApplicationId);

                    if (button.Allow)
                        settings.CommandColumn.CustomButtons.Add(button);
                }
            }

            if (customOptionActions != null)
            {
                foreach (var action in customOptionActions)
                {
                    var button = new BcGridViewCommandColumnCustomButton();
                    button.SetCustomAction(action.ActivityId, action.Name, action.IconClass, action.Action,action.Controller,action.Area, null);

                    if (button.Allow)
                        settings.CommandColumn.CustomButtons.Add(button);
                }
            }

            SetAutoFilterColumns(settings);

            if (!autoFixedHeightMarginTop.HasValue)
                autoFixedHeightMarginTop = 30;

            string setHeightScript =
                string.Format(@"
                            function(element) {{ 
                            var item = {{ element: element, marginTop: {0}, autoFixedHeight: {1} }};
                            BcGridViewElementObjects.push(item);
                            if(item.autoFixedHeight)
                                BcGridViewAdjustSize(item.element, item.marginTop);
                            }}
                            ", autoFixedHeightMarginTop, autoFixedHeight.ToString().ToLower());


            settings.ClientSideEvents.Init = setHeightScript;

            return DevExpress.Web.Mvc.UI.HtmlHelperExtension.DevExpress(ExtensionsFactory.Instance.HtmlHelper).GridView(settings);

            //return DevExpress.Web.Mvc.UI.ExtensionsFactory.Instance.GridView(settings);
        }       
    }

    public static class HtmlHelperExtension
    {
        public static ExtensionsFactory Bc(this HtmlHelper helper)
        {
            ExtensionsFactory.Instance.HtmlHelper = helper;
            return ExtensionsFactory.Instance;
        }
        
    }

    public class OptionAction
    {
        public string OptionId { get; set; }
        public string ActivityId { get; set; }
        public string ApplicationId { get; set; }

        public OptionAction(string OptionId, string ActivityId, string ApplicationId)
        {
            this.OptionId = OptionId;
            this.ActivityId = ActivityId;
            this.ApplicationId = ApplicationId;            
        }
    }

    public class CustomGridViewAction
    {
        public string Name {get; set;}
        public string ActivityId { get; set; }
        public string IconClass {get; set;}

        public string Action { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }

        public CustomGridViewAction(string activityId, string name, string iconClass)
        {
            this.IconClass = iconClass;
            this.Name = name;
            this.ActivityId = activityId;                    
        }

        public CustomGridViewAction(string activityId, string name, string iconClass, string action, string controller, string area)
        {
            this.IconClass = iconClass;
            this.Name = name;
            this.ActivityId = activityId;
            this.Action = action;
            this.Controller = controller;
            this.Area = area;
        }

        public CustomGridViewAction(string activityId, string name, Icons icon)
        {
            this.IconClass = icon.GetID();
            this.Name = name;
            this.ActivityId = activityId;
        }

        public CustomGridViewAction(string activityId, string name, Icons icon, string action, string controller, string area)
        {
            this.IconClass = icon.GetID();
            this.Name = name;
            this.ActivityId = activityId;
            this.Action = action;
            this.Controller = controller;
            this.Area = area;
        }
    }

    public class BcGridViewCommandColumnCustomButton : GridViewCommandColumnCustomButton
    {
        private const string imagePath = "~/Content/Base/images/activities";

        public bool Allow { get; set; }
        public string OptionId { get; set; }
        public string ActivityId { get; set; }
        public string ApplicationId { get; set; }
        public object RouteValues { get; set; }
        public string UrlAction { get; set; }
        public bool CustomActivity { get; set; }


        

        public void SetCustomAction(string activityId, string name, string iconClass, string action, string controller, string area, object routeValues = null)
        {
            Allow = true;
            CustomActivity = true;
            this.Text = name;
            ActivityId = activityId;
            RouteValues = routeValues;            

            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            if (action == null)
                this.ID = activityId;
            else
            {
                this.ID = url.Action(action, controller, new { area = area });
            }

            if (!string.IsNullOrEmpty(iconClass))
                this.Image.Url = String.Format("{0}/{1}.png", url.Content(imagePath), iconClass);                        
        }

        public void SetOptionActivityAction(string optionId, string activityId, object routeValues = null, string applicationId = null)
        {
            OptionId = optionId;
            ActivityId = activityId;
            RouteValues = routeValues;
            ApplicationId = applicationId;

            RefreshUrlAction();
        }

        public void RefreshUrlAction()
        {
            UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            UrlAction = String.Empty;

            //ApplicationProxy proxy = new ApplicationProxy(HttpContext.Current.Request.RequestContext.HttpContext);            

            if (string.IsNullOrEmpty(ApplicationId))
                ApplicationId = Bc.Configuration.BcConfigurationSection.Current.Application.ApplicationId;

            Bc.Web.Models.Definition.ApplicationOptionActivity applicationOptionActivity = Bc.Web.Mvc.Session.ApplicationOptionActivity.SingleOrDefault(p => 
                p.OptionId == OptionId
                && p.ActivityId == ActivityId && p.ApplicationId == ApplicationId);

            if (applicationOptionActivity != null)
            {
                Allow = true;                
                Bc.Web.Models.Definition.Activity activity = applicationOptionActivity.Activity;

                UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);

                this.Text = activity.Name;
                this.ID = helper.Action(applicationOptionActivity.Action, applicationOptionActivity.Controller, new { area = applicationOptionActivity.Area });

                if(!string.IsNullOrEmpty(activity.IconClass))
                    this.Image.Url = String.Format("{0}/{1}.png", url.Content(imagePath), activity.IconClass);
            }
        }        
    }

    public static class GridViewExtensions
    {
        public static MvcHtmlString BcButtonGridViewExportXls(this HtmlHelper htmlHelper, string controllerName, HtmlElementSize size = HtmlElementSize.Default,
            ElementThemeType themeType = ElementThemeType.Default, object htmlAttributes = null, string customText = null)
        {
            string url = Bc.Web.Mvc.Utility.UrlHelper.GetFromContext().Action("ExportXlsGridViewAction", controllerName);
            
            if (string.IsNullOrWhiteSpace(customText))
            {
                customText = Bc.Resources.LabelFor.Excel;
            }

            return htmlHelper.BcButton(customText, icon: Icons.Excel, customButtonClick: string.Format("event.preventDefault(); window.open('{0}')", url),
                size: size, themeType: themeType, submit: false, htmlAttributes: htmlAttributes);
        }

        public static void SetDefaultCallbackRoute(this GridViewSettings settings, string controllerName, string area)
        {
            if(!string.IsNullOrEmpty(area))
                settings.CallbackRouteValues = new { Controller = controllerName, Action = "GetGridViewIndex", Area = area };
            else
                settings.CallbackRouteValues = new { Controller = controllerName, Action = "GetGridViewIndex" };
        }

        public static void AddDefaultCustomBindingActions(this GridViewSettings settings, string controllerName)
        {
            AddDefaultCustomBindingActions(settings, controllerName, null);
        }

        public static void AddDefaultCustomBindingActions(this GridViewSettings settings, string controllerName, string area)
        {
            settings.SetDefaultCallbackRoute(controllerName, area);
            settings.AddDefaultCustomBindingActionForFiltering(controllerName, area);
            settings.AddDefaultCustomBindingActionForSorting(controllerName, area);
            settings.AddDefaultCustomBindingActionForPaging(controllerName, area);
        }

        public static void AddDefaultCustomBindingActionForPaging(this GridViewSettings settings, string controllerName, string area)
        {
            if (!String.IsNullOrEmpty(area))
            {
                settings.CustomBindingRouteValuesCollection.Add(
                    GridViewOperationType.Paging,
                    new { Controller = controllerName, Action = "ApplyGridViewPagingAction", Area = area }
                    );
            }
            else
            {
                settings.CustomBindingRouteValuesCollection.Add(
                                   GridViewOperationType.Paging,
                                   new { Controller = controllerName, Action = "ApplyGridViewPagingAction" }
                                   );
            }
        }

        public static void AddDefaultCustomBindingActionForSummaryValues(this GridViewSettings settings, string controllerName, string area)
        {
            if (!String.IsNullOrEmpty(area))
            {
                settings.CustomBindingRouteValuesCollection.Add(
                    GridViewOperationType.Paging,
                    new { Controller = controllerName, Action = "ApplyGridViewPagingAction", Area = area }
                    );
            }
            else
            {
                settings.CustomBindingRouteValuesCollection.Add(
                                  GridViewOperationType.Paging,
                                  new { Controller = controllerName, Action = "ApplyGridViewPagingAction" }
                                  );
            }
        }

        public static void AddDefaultCustomBindingActionForSorting(this GridViewSettings settings, string controllerName, string area)
        {
            if (!String.IsNullOrEmpty(area))
            {
                settings.CustomBindingRouteValuesCollection.Add(
                GridViewOperationType.Sorting,
                    new { Controller = controllerName, Action = "ApplyGridViewSortingAction", Area = area }
                    );
            }
            else
            {
                settings.CustomBindingRouteValuesCollection.Add(
                               GridViewOperationType.Sorting,
                                   new { Controller = controllerName, Action = "ApplyGridViewSortingAction" }
                                   );
            }
        }

        public static void AddDefaultCustomBindingActionForFiltering(this GridViewSettings settings, string controllerName, string area)
        {
            if (!String.IsNullOrEmpty(area))
            {
                settings.CustomBindingRouteValuesCollection.Add(
                GridViewOperationType.Filtering,
                new { Controller = controllerName, Action = "ApplyGridViewFilteringAction", Area = area }
                );
            }
            else
            {
                settings.CustomBindingRouteValuesCollection.Add(
               GridViewOperationType.Filtering,
               new { Controller = controllerName, Action = "ApplyGridViewFilteringAction" }
               );
            }
        }

        public static MVCxGridViewColumn AddCurrencyColumn(this MVCxGridViewColumnCollection columns, string fieldName, string caption)
        {
            MVCxGridViewColumn column = new MVCxGridViewColumn();
            column.Caption = caption;
            column.FieldName = fieldName;
            column.Width = 100;
            column.PropertiesEdit.DisplayFormatString = "c2";            

            columns.Add(column);
            return column;
        }

        public static MVCxGridViewColumn AddIntegerColumn(this MVCxGridViewColumnCollection columns, string fieldName, string caption)
        {
            MVCxGridViewColumn column = new MVCxGridViewColumn();
            column.Caption = caption;
            column.FieldName = fieldName;
            column.Width = 100;
            column.PropertiesEdit.DisplayFormatString = "n0";

            columns.Add(column);
            return column;
        }

        public static MVCxGridViewColumn AddDateColumn(this MVCxGridViewColumnCollection columns, string fieldName, string caption, bool includeTime = true)
        {
            MVCxGridViewColumn column = new MVCxGridViewColumn();
            column.Caption = caption;
            column.FieldName = fieldName;
            if(includeTime)
                column.PropertiesEdit.DisplayFormatString = Bc.Web.Mvc.Html.Constants.Formats.DateTimeFormat;
            else
                column.PropertiesEdit.DisplayFormatString = Bc.Web.Mvc.Constants.Formats.DateFormat;

            column.Width = 120;

            columns.Add(column);

            return column;
        }

        public static MVCxGridViewColumn AddUserInsertColumn(this MVCxGridViewColumnCollection columns)
        {
            MVCxGridViewColumn column = new MVCxGridViewColumn();
            column.Caption = Bc.Resources.LabelFor.UserInsertAudit;
            column.FieldName = "UserInsert";
            column.Width = 120;

            columns.Add(column);
            return column;
        }

        public static MVCxGridViewColumn AddDateInsertColumn(this MVCxGridViewColumnCollection columns)
        {
            return AddDateColumn(columns, "DateInsert", Bc.Resources.LabelFor.DateInsertAudit);
        }

        public static MVCxGridViewColumn AddUserUpdateColumn(this MVCxGridViewColumnCollection columns)
        {
            MVCxGridViewColumn column = new MVCxGridViewColumn();
            column.Caption = Bc.Resources.LabelFor.UserUpdateAudit;
            column.FieldName = "UserUpdate";
            column.Width = 120;

            columns.Add(column);
            return column;
        }

        public static MVCxGridViewColumn AddDateUpdateColumn(this MVCxGridViewColumnCollection columns)
        {
            return AddDateColumn(columns, "DateUpdate", Bc.Resources.LabelFor.DateUpdateAudit);
        }


        public static MVCxGridViewColumn AddUsrIngColumn(this MVCxGridViewColumnCollection columns)
        {
            MVCxGridViewColumn column = new MVCxGridViewColumn();
            column.Caption = Bc.Resources.LabelFor.UserInsertAudit;
            column.FieldName = "UsrIng";
            column.Width = 120;

            columns.Add(column);
            return column;
        }

        public static MVCxGridViewColumn AddFecIngColumn(this MVCxGridViewColumnCollection columns)
        {
            return AddDateColumn(columns, "FecIng", Bc.Resources.LabelFor.DateInsertAudit);
        }

        public static MVCxGridViewColumn AddUsrModColumn(this MVCxGridViewColumnCollection columns)
        {
            MVCxGridViewColumn column = new MVCxGridViewColumn();
            column.Caption = Bc.Resources.LabelFor.UserUpdateAudit;
            column.FieldName = "UsrMod";
            column.Width = 120;

            columns.Add(column);
            return column;
        }

        public static MVCxGridViewColumn AddFecModColumn(this MVCxGridViewColumnCollection columns)
        {
            return AddDateColumn(columns, "FecMod", Bc.Resources.LabelFor.DateUpdateAudit);
        }


    }
}
