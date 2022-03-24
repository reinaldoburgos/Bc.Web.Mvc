using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bc.Common
{
    public enum MessageType
    {
        Information = 0,
        Error = 1,
        Warning = 2,
        Confirmation = 3,
        Success = 4
    }

    public class Message
    {
        public Message()
            : this("")
        {
        }

        public Message(string textMessage)
            : this(textMessage, MessageType.Information, null)
        {
        }

        public Message(string textMessage, MessageType messageType)
            : this(textMessage, messageType, null)
        {
        }

        public Message(string textMessage, string title)
            : this(textMessage, MessageType.Information, title)
        {
        }

        public Message(string textMessage, MessageType messageType, string title)
        {
            this.TextMessage = textMessage;
            this.MessageType = messageType;
            this.Title = title;
        }

        public string TextMessage { get; set; }

        public MessageType MessageType { get; set; }

        public string Title { get; set; }

        public string Text
        {
            get
            {
                return this.TextMessage;
            }
        }
    }

    public class MessageCollection : List<Message>
    {
        public bool HasError()
        {
            return this.Any(p => p.MessageType == MessageType.Error);
        }

        public void AddMessage(string message)
        {
            Add(new Common.Message(message));
        }

        public void AddMessage(string message, Common.MessageType type)
        {
            Add(new Common.Message(message, type));
        }

        public void AddMessage(string message, Common.MessageType type, string title)
        {
            Add(new Common.Message(message, type, title));
        }

    }
}
