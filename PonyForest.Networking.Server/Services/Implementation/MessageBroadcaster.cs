using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PonyForestServer.Core.Models.Messages;
using Steamworks.Data;

namespace PonyForestServer.Core.Services.Implementation
{
    public class MessageBroadcaster : IMessageBroadcaster
    {
        private BinaryFormatter _binaryFormatter;

        public MessageBroadcaster()
        {
            _binaryFormatter = new BinaryFormatter();
        }
        
        public void Broadcast(MessageBase message, Connection connection)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                _binaryFormatter.Serialize(stream, message);
                stream.Position = 0;

                byte[] bytes = stream.ToArray();

                connection.SendMessage(bytes, 0, bytes.Length);
            }
        }
    }
}