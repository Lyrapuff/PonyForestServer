using System;
using System.Collections.Generic;
using PonyForestServer.Core.Models.Messages;
using PonyForestServer.Core.Models;

namespace PonyForestServer.Core.Services.Implementation
{
    public class World : IWorld
    {
        public List<Player> Players { get; }

        public World()
        {
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
    }
}