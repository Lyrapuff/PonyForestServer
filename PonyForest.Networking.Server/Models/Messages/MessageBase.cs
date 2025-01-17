﻿using System;

namespace PonyForestServer.Core.Models.Messages
{
    [Serializable]
    public class MessageBase<T> where T : IMessageSender
    {
        public T Sender { get; set; }
    }
}