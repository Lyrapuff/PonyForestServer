using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PonyForestServer.Core.Models;
using PonyForestServer.Core.Models.Messages;
using Steamworks.Data;

namespace PonyForestServer.Core.Services.Implementation
{
    internal class MessageBroadcaster : IMessageBroadcaster
    {
        private BinaryFormatter _binaryFormatter;
        private IWorld _world;

        public MessageBroadcaster(IWorld world)
        {
            _world = world;
            
            _binaryFormatter = new BinaryFormatter();
        }
        
        public void Broadcast(ServerMessage message, Player player)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                _binaryFormatter.Serialize(stream, message);
                stream.Position = 0;

                byte[] bytes = stream.ToArray();

                player.Connection.SendMessage(bytes, 0, bytes.Length);
            }
        }

        public void Broadcast(ServerMessage message)
        {
            foreach (Player player in _world.Players)
            {
                Broadcast(message, player);
            }
        }
    }
}