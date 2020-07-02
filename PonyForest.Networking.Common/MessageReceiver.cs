using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using PonyForest.Networking.Common.Messages;

namespace PonyForest.Networking.Common
{
    public class MessageReceiver : IMessageReceiver
    {
        private readonly List<KeyValuePair<Type, IMessageHandler>> _handlers = new List<KeyValuePair<Type, IMessageHandler>>();
        private readonly BinaryFormatter _binaryFormatter;

        public MessageReceiver()
        {
            _binaryFormatter = new BinaryFormatter();
        }
        
        public MessageBase Receive(IntPtr data, int size)
        {
            byte[] bytes = new byte[size];
            Marshal.Copy(data, bytes, 0, size);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(bytes, 0, size);
                stream.Position = 0;

                MessageBase message = _binaryFormatter.Deserialize(stream) as MessageBase;

                if (message != null)
                {
                    foreach (KeyValuePair<Type, IMessageHandler> handler in _handlers)
                    {
                        if (handler.Key == message.GetType())
                        {
                            handler.Value.Handle(ref message);
                        }
                    }

                    return message;
                }
            }

            return null;
        }

        public void RegisterHandler(Type messageType, IMessageHandler handler)
        {
            _handlers.Add(new KeyValuePair<Type, IMessageHandler>(messageType, handler));
        }

        public void RemoveHandler(IMessageHandler handler)
        {
            KeyValuePair<Type, IMessageHandler> element = _handlers
                .FirstOrDefault(x => x.Value == handler);

            _handlers.Remove(element);
        }
    }
}