using System;

namespace PonyForestServer.Core.Modules.Attributes
{
    public class MessageHandlerAttribute : Attribute
    {
        public Type MessageType { get; }
        
        public MessageHandlerAttribute(Type messageType)
        {
            MessageType = messageType;
        }
    }
}