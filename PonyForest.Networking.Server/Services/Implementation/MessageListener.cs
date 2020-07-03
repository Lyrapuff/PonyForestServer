using System.Threading.Tasks;
using PonyForestServer.Core.Models;
using Steamworks;

namespace PonyForestServer.Core.Services.Implementation
{
    internal class MessageListener : IMessageListener
    {
        private ServerConfig _config;
        private bool _running;

        public MessageListener(IConfigProvider configProvider)
        {
            _config = configProvider.Config;
        }
        
        public async Task StartListening(ServerCore serverCore)
        {
            _running = true;

            while (_running)
            {
                serverCore.Receive();
                await Task.Delay(1000 / _config.TickRate);
            }
        }

        public void StopListening()
        {
            _running = false;
        }
    }
}