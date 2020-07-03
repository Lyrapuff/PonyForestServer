using System.Collections.Generic;
using PonyForestServer.Core.Models.Messages;
using PonyForestServer.Core.World;

namespace PonyForestServer.Core.Services.Implementation
{
    public class World : IWorld
    {
        public List<Player> Players { get; }

        private IMessageBroadcaster _messageBroadcaster;

        public World(IMessageBroadcaster messageBroadcaster)
        {
            _messageBroadcaster = messageBroadcaster;
            
            Players = new List<Player>();
        }
        
        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }

        public void BroadcastMessage(MessageBase message)
        {
            foreach (Player player in Players)
            {
                _messageBroadcaster.Broadcast(message, player.Connection);
            }
        }
    }
}