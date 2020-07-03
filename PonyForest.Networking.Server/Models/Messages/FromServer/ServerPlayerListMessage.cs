using System;
using System.Collections.Generic;

namespace PonyForestServer.Core.Models.Messages.FromServer
{
    [Serializable]
    public class ServerPlayerListMessage : ServerMessage
    {
        public List<Player> Players;
    }
}