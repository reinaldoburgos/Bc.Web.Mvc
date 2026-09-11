//using Bc.Common;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Web;
//using System.Web.Caching;
//using System.Web.Mvc;

//namespace Bc.Web.Mvc.Base
//{
//    public abstract class BaseController : System.Web.Mvc.Controller
//    {
//        public BaseController()
//        {
//        }


//        // This method helps to render a partial view into html string.
//        // Credit: Kevin Craft
//        public string RenderPartialViewToString(string viewName, object model)
//        {
//            ViewData.Model = model;
//            using (var sw = new StringWriter())
//            {
//                var viewResult =
//                    ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
//                var viewContext = new ViewContext(ControllerContext,
//                    viewResult.View, ViewData, TempData, sw);
//                viewResult.View.Render(viewContext, sw);

//                return sw.GetStringBuilder().ToString();
//            }
//        }

//        // This method helps to get the error information from the MVC "ModelState".
//        // We can not directly send the ModelState to the client in Json. The "ModelState"
//        // object has some circular reference that prevents it to be serialized to Json.
//        public Dictionary<string, object> GetErrorsFromModelState()
//        {
//            var errors = new Dictionary<string, object>();
//            foreach (var key in ModelState.Keys)
//            {
//                // Only send the errors to the client.
//                if (ModelState[key].Errors.Count > 0)
//                {
//                    errors[key] = ModelState[key].Errors;
//                }
//            }

//            return errors;
//        }



//        public bool HasError
//        {
//            get
//            {
//                return this.MessageCollection.HasError();
//            }
//        }

//        public bool IsValid
//        {
//            get
//            {
//                return !this.MessageCollection.HasError();
//            }
//        }

//        bool isValidRequestSetter;
//        bool? isValidRequest;
//        public bool? IsValidRequest
//        {
//            get
//            {
//                if (!isValidRequestSetter)
//                {
//                    if (this.MessageCollection.Any())
//                    {
//                        return !this.MessageCollection.HasError();
//                    }
//                }

//                return isValidRequest;
//            }
//            set
//            {
//                isValidRequestSetter = true;
//                isValidRequest = value;
//            }
//        }

//        private int organizationId = 0;
//        internal protected int OrganizationId
//        {
//            get
//            {
//                return organizationId;
//            }
//        }

//        protected void SetOrganizationId(int organizationId)
//        {
//            this.organizationId = organizationId;
//        }

//        public bool ExcessiveRequestAttemptsDetected { get; set; }

//        private string applicationId;
//        internal protected string ApplicationId
//        {
//            get
//            {
//                return applicationId;
//            }
//        }

//        protected string ApplicationName
//        {
//            get
//            {
//                return Bc.Web.Mvc.Session.ApplicationName;
//            }
//        }

//        protected string SourceApplicationId
//        {
//            get
//            {
//                return Bc.Web.Mvc.Session.FilterApplicationId;
//            }
//        }

//        protected bool IsActiveSourceApplication
//        {
//            get
//            {
//                return !string.IsNullOrEmpty(Bc.Web.Mvc.Session.FilterApplicationId);
//            }
//        }

//        protected string SourceUrlApplication
//        {
//            get
//            {
//                // return Bc.Web.Http.CookieHelper.GetUrlApplicationSourceValue(this.ApplicationId);
//                return null;
//            }
//        }

//        protected string UserLogonName
//        {
//            get
//            {
//                return Bc.Web.Mvc.Session.LogonName;
//            }
//        }

//        protected string UserFullName
//        {
//            get
//            {
//                return Bc.Web.Mvc.Session.UserFullName;
//            }
//        }

//        protected int UserId
//        {
//            get
//            {
//                return Bc.Web.Mvc.Session.UserId;
//            }
//        }

//        protected bool IsLogged
//        {
//            get
//            {
//                return Bc.Web.Mvc.Session.IsLogged;
//            }
//        }

//        protected string OrganizationName
//        {
//            get
//            {
//                return Bc.Web.Mvc.Session.OrganizationName;
//            }
//        }

//        protected int RoleId
//        {
//            get
//            {
//                return Bc.Web.Mvc.Session.RoleId;
//            }
//        }

//        protected string RoleName
//        {
//            get
//            {
//                return Bc.Web.Mvc.Session.RoleName;
//            }
//        }

//        protected string UrlSecureLastAttempted
//        {
//            get
//            {
//                return Bc.Web.Mvc.Session.UrlSecureLastAttempted;
//            }
//        }

//        protected DateTime GetCurrentDateTime()
//        {
//            return Bc.Web.Mvc.Session.CurrentDateTime;
//        }

//        protected JsonResult Json()
//        {
//            return base.Json(
//                new { IsValid = !MessageCollection.HasError(), HasError = MessageCollection.HasError(), Message = MessageCollection.FirstOrDefault(), Messages = MessageCollection });
//        }

//        protected new JsonResult Json(object data, JsonRequestBehavior behavior)
//        {
//            return base.Json(
//                new { IsValid = !MessageCollection.HasError(), HasError = MessageCollection.HasError(), Message = MessageCollection.FirstOrDefault(), Messages = MessageCollection, Content = data }, behavior);
//        }

//        protected new JsonResult Json(object data)
//        {
//            return base.Json(new { IsValid = !MessageCollection.HasError(), HasError = MessageCollection.HasError(), Message = MessageCollection.FirstOrDefault(), MessageCollection = MessageCollection, Content = data });
//        }

//        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
//        {
//            return base.Json(new { IsValid = !MessageCollection.HasError(), HasError = MessageCollection.HasError(), Message = MessageCollection.FirstOrDefault(), Messages = MessageCollection, Content = data },
//                contentType, contentEncoding);
//        }

//        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
//        {
//            return base.Json(new { IsValid = !MessageCollection.HasError(), HasError = MessageCollection.HasError(), Message = MessageCollection.FirstOrDefault(), Messages = MessageCollection, Content = data },
//                contentType, contentEncoding, behavior);
//        }


//        protected WrappedJsonResult WrappedJson()
//        {
//            return new WrappedJsonResult()
//            {
//                Data = new { IsValid = !MessageCollection.HasError(), HasError = MessageCollection.HasError(), Message = MessageCollection.FirstOrDefault(), Messages = MessageCollection }
//            };
//        }

//        protected WrappedJsonResult WrappedJson(object data)
//        {
//            return new WrappedJsonResult()
//            {
//                Data = new { IsValid = !MessageCollection.HasError(), HasError = MessageCollection.HasError(), Message = MessageCollection.FirstOrDefault(), Messages = MessageCollection, Content = data }
//            };
//        }

//        private bool IsSharingMessages = false;

//        private Bc.Common.MessageCollection messages;
//        public Bc.Common.MessageCollection MessageCollection
//        {
//            get
//            {
//                if (messages == null)
//                    messages = new Bc.Common.MessageCollection();
//                return messages;
//            }
//        }

//        protected virtual ActionResult HttpCustomNotFound()
//        {
//            return RedirectToAction("Error404", "Error", new { area = "" });
//        }

//        protected override RedirectResult Redirect(string url)
//        {
//            SharedActionMessage();
//            return base.Redirect(url);
//        }

//        protected override RedirectToRouteResult RedirectToAction(string actionName, string controllerName, System.Web.Routing.RouteValueDictionary routeValues)
//        {
//            SharedActionMessage();
//            return base.RedirectToAction(actionName, controllerName, routeValues);
//        }

//        protected new RedirectToRouteResult RedirectToAction(string actionName, string controllerName, object routeValues)
//        {
//            SharedActionMessage();
//            return base.RedirectToAction(actionName, controllerName, routeValues);
//        }

//        protected RedirectToRouteResult RedirectToAction(string actionName, string controllerName, object routeValues, bool sharedMessage)
//        {
//            if (sharedMessage && this.MessageCollection.Any()) SharedActionMessage();
//            return base.RedirectToAction(actionName, controllerName, routeValues);
//        }

//        public void SharedActionMessage()
//        {
//            if (this.MessageCollection.Any())
//            {
//                var cache = this.HttpContext.Cache;
//                if (cache["sharedMessages"] == null)
//                    cache.Add("sharedMessages", this.MessageCollection, null, DateTime.Now.AddSeconds(30), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
//                else
//                    cache["sharedMessages"] = this.MessageCollection;

//                IsSharingMessages = true;
//            }
//        }

//        private void AddSharedActionMessage()
//        {
//            var cache = this.HttpContext.Cache;
//            if (cache["sharedMessages"] != null)
//            {
//                Bc.Common.MessageCollection sharedMessageCollection = (Bc.Common.MessageCollection)cache["sharedMessages"];
//                if (sharedMessageCollection.Any())
//                    this.MessageCollection.InsertRange(0, sharedMessageCollection);
//                cache.Remove("sharedMessages");
//            }
//        }

//        public string ErrorMessage { get; set; }
//        public string ErrorAdditionalInformation { get; set; }

     
      
//        public void AddDefaultSuccessMessage()
//        {
//            this.AddMessage("La operación fue realizada exitosamente", MessageType.Success);
//            //this.AddMessage(Bc.Resources.MessageFor.DefaultSuccessTransactionMessage, MessageType.Success);
//        }

//        public void AddDefaultErrorMessage()
//        {
//            this.AddMessage("Ocurrió un problema al intentar realizar la transacción", MessageType.Error);
//            // this.AddMessage(Bc.Resources.MessageFor.DefaultErrorTransactionMessage, MessageType.Error);
//        }

//        public void AddErrorMessage(string message)
//        {
//            MessageCollection.Add(new Bc.Common.Message(message, MessageType.Error));
//        }

//        public void AddErrorMessage(string message, string title)
//        {
//            MessageCollection.Add(new Bc.Common.Message(message, MessageType.Error, title));
//        }

//        public void AddInformationMessage(string message)
//        {
//            MessageCollection.Add(new Bc.Common.Message(message, MessageType.Information));
//        }

//        public void AddInformationMessage(string message, string title)
//        {
//            MessageCollection.Add(new Bc.Common.Message(message, MessageType.Information, title));
//        }

//        public void AddSuccessMessage(string message)
//        {
//            MessageCollection.Add(new Message(message, MessageType.Success));
//        }

//        public void AddSuccessMessage(string message, string title)
//        {
//            MessageCollection.Add(new Message(message, MessageType.Success, title));
//        }
//        public void AddWarningMessage(string message)
//        {
//            MessageCollection.Add(new Message(message, MessageType.Warning));
//        }
//        public void AddWarningMessage(string message, string title)
//        {
//            MessageCollection.Add(new Message(message, MessageType.Warning, title));
//        }
//        public void AddConfirmationMessage(string message)
//        {
//            MessageCollection.Add(new Message(message, MessageType.Confirmation));
//        }
//        public void AddConfirmationMessage(string message, string title)
//        {
//            MessageCollection.Add(new Message(message, MessageType.Confirmation, title));
//        }


//        public void AddMessage(Message message)
//        {
//            MessageCollection.Add(message);
//        }

//        public void AddMessage(string message)
//        {
//            MessageCollection.Add(new Message(message));
//        }

//        public void AddMessage(string message, MessageType type)
//        {
//            MessageCollection.Add(new Message(message, type));
//        }

//        public void AddMessage(string message, string title)
//        {
//            MessageCollection.Add(new Message(message, MessageType.Information, title));
//        }

//        public void AddMessage(string message, MessageType type, string title)
//        {
//            MessageCollection.Add(new Message(message, type, title));
//        }

//        public void AddMessages(IEnumerable<Message> messages)
//        {
//            MessageCollection.AddRange(messages);
//        }

//        public void ClearMessages()
//        {
//            MessageCollection.Clear();
//        }

//        protected override void ExecuteCore()
//        {
//            base.ExecuteCore();
//        }
      
//    }

//}
