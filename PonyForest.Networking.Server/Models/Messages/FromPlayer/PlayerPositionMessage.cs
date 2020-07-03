using System;
using PonyForestServer.Core.Models.Types;

namespace PonyForestServer.Core.Models.Messages.FromPlayer
{
    [Serializable]
    public class PlayerPositionMessage : PlayerMessage
    {
        public NetworkVector3 Position;
    }
}