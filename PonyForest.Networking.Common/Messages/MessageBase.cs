using System;

namespace PonyForest.Networking.Common.Messages
{
    [Serializable]
    public abstract class MessageBase
    {
        public ulong From { get; set; }
        public bool Canceled { get; set; }
    }
}