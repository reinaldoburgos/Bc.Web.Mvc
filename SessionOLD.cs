//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;


//namespace Bc.Web.Mvc
//{
//    public class SessionKey
//    {
//        public const string RoleId = "RoleId";
//        public const string RoleName = "RoleName";
//        public const string LogonName = "LogonName";
//        public const string UserId = "UserId";
//        public const string UserFullName = "UserFullName";
//        public const string IsLogged = "IsLogged";
//        public const string OrganizationId = "OrganizationId";
//        public const string ApplicationId = "ApplicationId";
//        public const string ApplicationName = "ApplicationName";
//        public const string OrganizationName = "OrganizationName";
//        public const string UrlSecureLastAttempted = "UrlSecureLastAttempted";
//        public const string RoleApplicationOption = "RoleApplicationOption";
//        public const string FilterApplicationId = "FilterApplicationId";
//        public const string FilterApplicationName = "FilterApplicationName";
//        public const string UserPhoto = "UserPhoto";

//        public const string ExpirationDate = "ExpirationDate";
//        public const string PasswordTries = "PasswordTries";
//        public const string ActiveDirectoryEnabled = "ActiveDirectoryEnabled";

//        public const string EmailRepeatedKey = "EmailRepeated";
//        public const string UserImageHideKey = "UserImageHide";
//        public const string SendMailAppKey = "SendMailApp";

//        public const string SideMenuOpen = "SideMenuOpen";
//    }
//    public class Session
//    {

//        public static void End()
//        {
//            SetValue(Bc.Web.Mvc.SessionKey.IsLogged, false);
//            RemoveAll();
//        }

//        public static void RemoveAll()
//        {
//            HttpContext.Current.Session.Clear();
//        }

//        public static void Remove(params string[] exceptions)
//        {
//            if (IsLogged)
//            {
//                int userId = UserId;
//                string logonName = LogonName;
//                int roleId = RoleId;
//                int organizationId = OrganizationId;
//                string userFullName = UserFullName;
//                string roleName = RoleName;
//                string organizationName = OrganizationName;
//                string filterApplicationId = FilterApplicationId;
//                string userPhoto = UserPhoto;
//                string authToken = AuthToken;

//                Dictionary<string, object> backUp = null;
//                if (exceptions != null && exceptions.Any())
//                {
//                    backUp = new Dictionary<string, object>();

//                    foreach (string sessionName in exceptions)
//                    {
//                        object value = GetValue(sessionName);
//                        if (value != null)
//                            backUp[sessionName] = value;
//                    }
//                }

//                RemoveAll();

//                SetValue(Bc.Web.Mvc.SessionKey.IsLogged, true);
//                SetValue(Bc.Web.Mvc.SessionKey.UserId, userId);
//                SetValue(SessionKey.LogonName, logonName);
//                SetValue(SessionKey.RoleId, roleId);
//                SetValue(SessionKey.OrganizationId, organizationId);
//                SetValue(SessionKey.UserFullName, userFullName);
//                SetValue(SessionKey.RoleName, roleName);
//                SetValue(SessionKey.OrganizationName, organizationName);
//                SetValue(SessionKey.FilterApplicationId, filterApplicationId);
//                SetValue(SessionKey.UserPhoto, userPhoto);


//                if (backUp != null)
//                {
//                    foreach (KeyValuePair<string, object> entry in backUp)
//                    {
//                        SetValue(entry.Key, entry.Value);
//                    }
//                }
//            }
//            else
//            {
//                RemoveAll();
//            }
//        }

//        public static bool SideMenuOpen
//        {
//            get
//            {
//                return Convert.ToBoolean(GetValue(SessionKey.SideMenuOpen));
//            }
//        }

//        public static bool IsLogged
//        {
//            get
//            {
//                return Convert.ToBoolean(GetValue(SessionKey.IsLogged));
//            }
//        }

//        public static int UserId
//        {
//            get
//            {
//                return Convert.ToInt32(GetValue(SessionKey.UserId));
//            }
//        }

//        public static string LogonName
//        {
//            get
//            {
//                return Convert.ToString(GetValue(SessionKey.LogonName));
//            }
//        }

//        public static string AuthToken
//        {
//            get
//            {
//                return null;
//            }
//        }

//        public static string UserFullName
//        {
//            get
//            {
//                return Convert.ToString(GetValue(SessionKey.UserFullName));
//            }
//        }

//        public static string UserPhoto
//        {
//            get
//            {
//                return Convert.ToString(GetValue(SessionKey.UserPhoto));
//            }
//        }

//        public static int RoleId
//        {
//            get
//            {
//                return Convert.ToInt32(GetValue(SessionKey.RoleId));
//            }
//        }

//        public static string RoleName
//        {
//            get
//            {
//                return Convert.ToString(GetValue(SessionKey.RoleName));
//            }
//        }

//        public static int OrganizationId
//        {
//            get
//            {
//                return Convert.ToInt32(GetValue(SessionKey.OrganizationId));
//            }
//        }

//        public static string FilterApplicationId
//        {
//            get
//            {
//                return Convert.ToString(GetValue(SessionKey.FilterApplicationId));
//            }
//        }

//        public static string ApplicationId
//        {
//            get
//            {
//                string id = Convert.ToString(GetValue(SessionKey.ApplicationId));
//                return id;
//            }
//        }

//        public static string ApplicationName
//        {
//            get
//            {
//                string name = Convert.ToString(GetValue(SessionKey.ApplicationName));
//                return name;
//            }
//        }

//        public static string FilterApplicationName
//        {
//            get
//            {
//                string name = Convert.ToString(GetValue(SessionKey.FilterApplicationName));
//                return name;
//            }
//        }

//        public static string OrganizationName
//        {
//            get
//            {
//                return Convert.ToString(GetValue(SessionKey.OrganizationName));
//            }
//        }

//        public static string UrlSecureLastAttempted
//        {
//            get
//            {
//                return Convert.ToString(GetValue(SessionKey.UrlSecureLastAttempted));
//            }
//            set
//            {
//                SetValue(SessionKey.UrlSecureLastAttempted, value);
//            }
//        }

//        public static DateTime CurrentDateTime
//        {
//            get
//            {
//                return Bc.Runtime.Current.GetCurrentDateTime();
//            }
//        }

//        public static int PasswordTries
//        {
//            get
//            {
//                object value = GetValue(SessionKey.PasswordTries);

//                if (value != null)
//                    return Convert.ToInt32(value);
//                else
//                    return 0;
//            }
//        }

//        public static DateTime? ExpirationDate
//        {
//            get
//            {
//                object value = GetValue(SessionKey.ExpirationDate);

//                if (value != null)
//                    return Convert.ToDateTime(value);
//                else
//                    return null;
//            }
//        }

//        public static bool ActiveDirectoryEnabled
//        {
//            get
//            {
//                object value = GetValue(SessionKey.ActiveDirectoryEnabled);

//                if (value != null)
//                    return Convert.ToBoolean(value);
//                else
//                    return false;
//            }
//        }


//        public static bool EmailRepeated
//        {
//            get
//            {
//                object value = GetValue(SessionKey.EmailRepeatedKey);

//                if (value != null)
//                    return Convert.ToBoolean(value);
//                else
//                    return false;
//            }
//        }

//        public static bool SendMailApp
//        {
//            get
//            {
//                object value = GetValue(SessionKey.SendMailAppKey);

//                if (value != null)
//                    return Convert.ToBoolean(value);
//                else
//                    return false;
//            }
//        }

//        public static bool UserImageHide
//        {
//            get
//            {
//                object value = GetValue(SessionKey.UserImageHideKey);

//                if (value != null)
//                    return Convert.ToBoolean(value);
//                else
//                    return false;
//            }
//        }

//        public static void SetValue(string key, object value)
//        {
//            if (HttpContext.Current.Session != null)
//                HttpContext.Current.Session[key] = value;
//        }

//        public static object GetValue(string key)
//        {
//            if (HttpContext.Current.Session != null)
//                return HttpContext.Current.Session[key];
//            else
//                return null;
//        }
//    }
//}
