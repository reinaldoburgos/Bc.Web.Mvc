using Bc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bc.Web.Mvc.Base
{
    public abstract class BaseViewModels
    {
        public string Error { get; set; }

        public string CurrentController { get; set; }
        public string CurrentActivity { get; set; }
        public string CurrentAction { get; set; }

        public string CurrentNavigationScreen { get; set; }
        public string TypeOther { get; set; }
        public bool FromOther { get; set; }

        public bool IsNew { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowDelete { get; set; }

        public bool AllowPartialEdit { get; set; }
        public bool AutoExecute { get; set; }

        public bool AccesoDatos { get; set; }

        //public bool HasError
        //{
        //    get
        //    {
        //        return this.MessageCollection.HasError();
        //    }
        //}

        //public bool IsValid
        //{
        //    get
        //    {
        //        return !this.MessageCollection.HasError();
        //    }
        //}

        //public bool AllowEdit { get; set; }
        //public bool IsNew { get; set; }


        //private Bc.Common.MessageCollection messages;
        //public Bc.Common.MessageCollection MessageCollection
        //{
        //    get
        //    {
        //        if (messages == null)
        //            messages = new Bc.Common.MessageCollection();
        //        return messages;
        //    }
        //}

        //public void AddDefaultSuccessMessage()
        //{
        //    this.AddMessage("La operación fue realizada exitosamente", MessageType.Success);
        //}

        //public void AddDefaultErrorMessage()
        //{
        //    this.AddMessage("Ocurrió un problema al intentar realizar la transacción", MessageType.Error);
        //}

        //public void AddErrorMessage(string message)
        //{
        //    MessageCollection.Add(new Bc.Common.Message(message, Bc.Common.MessageType.Error));
        //}

        //public void AddErrorMessage(string message, string title)
        //{
        //    MessageCollection.Add(new Bc.Common.Message(message, MessageType.Error, title));
        //}

        //public void AddInformationMessage(string message)
        //{
        //    MessageCollection.Add(new Bc.Common.Message(message, MessageType.Information));
        //}

        //public void AddInformationMessage(string message, string title)
        //{
        //    MessageCollection.Add(new Bc.Common.Message(message, MessageType.Information, title));
        //}

        //public void AddSuccessMessage(string message)
        //{
        //    MessageCollection.Add(new Message(message, MessageType.Success));
        //}

        //public void AddSuccessMessage(string message, string title)
        //{
        //    MessageCollection.Add(new Message(message, MessageType.Success, title));
        //}
        //public void AddWarningMessage(string message)
        //{
        //    MessageCollection.Add(new Message(message, MessageType.Warning));
        //}
        //public void AddWarningMessage(string message, string title)
        //{
        //    MessageCollection.Add(new Message(message, MessageType.Warning, title));
        //}
        //public void AddConfirmationMessage(string message)
        //{
        //    MessageCollection.Add(new Message(message, MessageType.Confirmation));
        //}
        //public void AddConfirmationMessage(string message, string title)
        //{
        //    MessageCollection.Add(new Message(message, MessageType.Confirmation, title));
        //}


        //public void AddMessage(Message message)
        //{
        //    MessageCollection.Add(message);
        //}

        //public void AddMessage(string message)
        //{
        //    MessageCollection.Add(new Message(message));
        //}

        //public void AddMessage(string message, MessageType type)
        //{
        //    MessageCollection.Add(new Message(message, type));
        //}

        //public void AddMessage(string message, string title)
        //{
        //    MessageCollection.Add(new Message(message, MessageType.Information, title));
        //}

        //public void AddMessage(string message, MessageType type, string title)
        //{
        //    MessageCollection.Add(new Message(message, type, title));
        //}

        //public void AddMessages(IEnumerable<Message> messages)
        //{
        //    MessageCollection.AddRange(messages);
        //}

        //public void ClearMessages()
        //{
        //    MessageCollection.Clear();
        //}


    }
}
