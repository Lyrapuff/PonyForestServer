using System;
using PonyForestServer.Core.World;

namespace PonyForestServer.Core.Models.Messages
{
    [Serializable]
    public class MessageBase
    {
        public Player Sender { get; set; }
    }
}