using PonyForestServer.Core.Models.Messages;

namespace PonyForestServer.Core.Services
{
    internal interface IMessageRouter
    {
        void Route(PlayerMessage message);
    }
}