using PonyForestServer.Core.Models.Messages;
using PonyForestServer.Core.Modules;
using PonyForestServer.Core.Modules.Attributes;
using PonyForestServer.Core.Services;
using PonyForestServer.Core.Tools;

namespace PonyForest.Networking.Server.CoreModule
{
    public class ModuleMain : ServerModule
    {
        private readonly Logger _logger;
        private readonly IWorld _world;

        public ModuleMain(ILoggerProvider loggerProvider, IWorld world)
        {
            _logger = loggerProvider.GetLogger("CoreModule");
            _world = world;
        }

        [MessageHandler(typeof(PlayerConnectionMessage))]
        public PlayerConnectionMessage PlayerConnection(PlayerConnectionMessage message)
        {
            _world.AddPlayer(message.Sender);
            
            _logger.LogInformation($"Welcome, {message.Sender.SteamId}!");

            return message;
        }
    }
}