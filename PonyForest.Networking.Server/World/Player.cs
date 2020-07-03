using System;
using Steamworks.Data;

namespace PonyForestServer.Core.World
{
    [Serializable]
    public class Player
    {
        public ulong SteamId { get; set; }
        [field: NonSerialized]
        internal Connection Connection { get; set; }
    }
}