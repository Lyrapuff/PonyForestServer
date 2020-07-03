using PonyForestServer.Core.Models.Messages;
using Steamworks.Data;

namespace PonyForestServer.Core.Services
{
    public interface IMessageBroadcaster
    {
        void Broadcast(MessageBase message, Connection connection);
    }
}