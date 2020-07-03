using System;
using Steamworks.Data;

namespace PonyForestServer.Core.Models
{
    [Serializable]
    public class Player : IMessageSender
    {
        public ulong SteamId { get; set; }
        [field: NonSerialized]
        internal Connection Connection { get; set; }
    }
}