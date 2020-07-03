using System.Collections.Generic;
using PonyForestServer.Core.Models.Messages;
using PonyForestServer.Core.Models;

namespace PonyForestServer.Core.Services
{
    public interface IWorld
    {
        List<Player> Players { get; }
        void AddPlayer(Player player);
        void RemovePlayer(Player player);
    }
}