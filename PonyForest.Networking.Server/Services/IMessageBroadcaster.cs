using PonyForestServer.Core.Models;
using PonyForestServer.Core.Models.Messages;

namespace PonyForestServer.Core.Services
{
    public interface IMessageBroadcaster
    {
        void Broadcast(ServerMessage message, Player player);
        void Broadcast(ServerMessage message);
    }
}