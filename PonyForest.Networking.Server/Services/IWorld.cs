using System.Collections.Generic;
using PonyForestServer.Core.Models.Messages;
using PonyForestServer.Core.World;

namespace PonyForestServer.Core.Services
{
    public interface IWorld
    {
        List<Player> Players { get; }
        void AddPlayer(Player player);
        void RemovePlayer(Player player);
        void BroadcastMessage(MessageBase message);
    }
}