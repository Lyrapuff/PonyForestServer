using PonyForestServer.Core.Models;
using PonyForestServer.Core.Tools;
using Steamworks;

namespace PonyForestServer.Core.Services
{
    public class Server : SocketManager
    {
        private readonly Logger _logger;
        private readonly ServerConfig _config;
        
        public Server(ILoggerProvider loggerProvider, IConfigProvider configProvider)
        {
            _logger = loggerProvider.GetLogger("Server");
            _logger.LogInformation("Starting server");

            _config = configProvider.Config;
        }
    }
}