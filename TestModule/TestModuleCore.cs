using PonyForestServer.Core.Models;
using PonyForestServer.Core.Models.Messages;
using PonyForestServer.Core.Modules;
using PonyForestServer.Core.Modules.Attributes;
using PonyForestServer.Core.Services;
using PonyForestServer.Core.Tools;

namespace TestModule
{
    public class TestModuleCore : ServerModule
    {
        private readonly Logger _logger;
        private readonly ServerConfig _config;
        
        public TestModuleCore(ILoggerProvider loggerProvider, IConfigProvider configProvider)
        {
            _logger = loggerProvider.GetLogger("TestModule");
            _config = configProvider.Config;
        }
        
        public override void OnEnabled()
        {
            _logger.LogInformation($"Current server name is: {_config.Name}");
        }

        [MessageHandler(typeof(PlayerConnectionMessage))]
        public void PlayerConnected(PlayerConnectionMessage message)
        {
            _logger.LogInformation($"Welcome, {message.Sender.SteamId}!");
        }
    }
}