using System;
using PonyForest.Networking.Common.Messages;

namespace PonyForest.Networking.Common
{
    public interface IMessageReceiver
    {
        MessageBase Receive(IntPtr data, int size);
        void RegisterHandler(Type messageType, IMessageHandler handler);
        void RemoveHandler(IMessageHandler handler);
    }
}