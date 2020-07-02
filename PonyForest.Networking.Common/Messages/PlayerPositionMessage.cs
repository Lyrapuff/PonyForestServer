using System;
using PonyForest.Networking.Common.Types;

namespace PonyForest.Networking.Common.Messages
{
    [Serializable]
    public class PlayerPositionMessage : MessageBase
    {
        public NetworkVector3 Position { get; set; }
    }
}