using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bc.Web.Mvc.Html
{
    public static class WebViewPageExtensions
    {
        public static DateTime GetCurrentDateTime(this WebViewPage webViewPage)
        {
            return Bc.Web.Mvc.Session.CurrentDateTime;
        }

        public static string GetLogonName(this WebViewPage webViewPage)
        {
            return Bc.Web.Mvc.Session.LogonName;
        }

        public static int GetOrganizationId(this WebViewPage webViewPage)
        {
            return Bc.Web.Mvc.Session.OrganizationId;
        }

        public static string GetOrganizationName(this WebViewPage webViewPage)
        {
            return Bc.Web.Mvc.Session.OrganizationName;
        }

        public static int GetRoleId(this WebViewPage webViewPage)
        {
            return Bc.Web.Mvc.Session.RoleId;
        }

        public static string GetRoleName(this WebViewPage webViewPage)
        {
            return Bc.Web.Mvc.Session.RoleName;
        }

        public static string GetUserFullName(this WebViewPage webViewPage)
        {
            return Bc.Web.Mvc.Session.UserFullName;
        }

        public static int GetUserId(this WebViewPage webViewPage)
        {
            return Bc.Web.Mvc.Session.UserId;
        }

        public static void SetTitle(this WebViewPage webViewPage, string title)
        {
            webViewPage.ViewBag.Title = title;
        }

        public static void SetImportFormElements(this WebViewPage webViewPage, bool value)
        {
            webViewPage.ViewBag.BcWebPageImportFormElements = value;
        }

        public static bool ImportFormElements(this WebViewPage webViewPage)
        {
            return Convert.ToBoolean(webViewPage.ViewBag.BcWebPageImportFormElements);
        }

        public static void SetImportTables(this WebViewPage webViewPage, bool value)
        {
            webViewPage.ViewBag.BcWebPageImportTables = value;
        }

        public static void SetImportFileInput(this WebViewPage webViewPage, bool value)
        {
            webViewPage.ViewBag.BcWebPageImportFileInput = value;
        }

        public static void SetImportAdvTables(this WebViewPage webViewPage, bool value)
        {
            webViewPage.ViewBag.BcWebPageImportAdvTables = value;
        }

        public static void SetImportCharts(this WebViewPage webViewPage, bool value)
        {
            webViewPage.ViewBag.BcWebPageImportCharts = value;
        }

        public static bool ImportTables(this WebViewPage webViewPage)
        {
            return Convert.ToBoolean(webViewPage.ViewBag.BcWebPageImportTables);
        }

        public static bool ImportFileInput(this WebViewPage webViewPage)
        {
            return Convert.ToBoolean(webViewPage.ViewBag.BcWebPageImportFileInput);
        }

        public static bool ImportAdvTables(this WebViewPage webViewPage)
        {
            return Convert.ToBoolean(webViewPage.ViewBag.BcWebPageImportAdvTables);
        }

        public static bool ImportCharts(this WebViewPage webViewPage)
        {
            return Convert.ToBoolean(webViewPage.ViewBag.BcWebPageImportCharts);
        }

        public static void SetImportFormValidations(this WebViewPage webViewPage, bool value)
        {
            webViewPage.ViewBag.BcWebPageImportFormValidations = value;
        }

        public static bool ImportFormValidations(this WebViewPage webViewPage)
        {
            return Convert.ToBoolean(webViewPage.ViewBag.BcWebPageImportFormValidations);
        }

        public static void SetInlineNotificationMessage(this WebViewPage webViewPage, bool value, bool canClose)
        {
            webViewPage.ViewBag.InlineNotificationMessage = value;
            webViewPage.ViewBag.InlineNotificationMessageCanClose = value;
        }

        public static bool InlineNotificationMessage(this WebViewPage webViewPage)
        {
            return Convert.ToBoolean(webViewPage.ViewBag.InlineNotificationMessage);
        }

        public static bool InlineNotificationMessageCanClose(this WebViewPage webViewPage)
        {
            return Convert.ToBoolean(webViewPage.ViewBag.InlineNotificationMessageCanClose);
        }       
    }
}
