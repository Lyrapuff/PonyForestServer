using System;
using PonyForestServer.Core.Models.Types;

namespace PonyForestServer.Core.Models.Messages.FromServer
{
    [Serializable]
    public class ServerPlayerSpawnMessage : ServerMessage
    {
        public Player Player;
        public NetworkVector3 Position;
    }
}