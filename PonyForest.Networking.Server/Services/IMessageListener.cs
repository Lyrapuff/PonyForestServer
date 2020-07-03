using System.Threading.Tasks;
using Steamworks;

namespace PonyForestServer.Core.Services
{
    internal interface IMessageListener
    {
        Task StartListening(ServerCore serverCore);
        void StopListening();
    }
}