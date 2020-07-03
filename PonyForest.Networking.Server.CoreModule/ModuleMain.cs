using PonyForestServer.Core.Models.Messages.FromPlayer;
using PonyForestServer.Core.Models.Messages.FromServer;
using PonyForestServer.Core.Models.Types;
using PonyForestServer.Core.Modules;
using PonyForestServer.Core.Modules.Attributes;
using PonyForestServer.Core.Services;
using PonyForestServer.Core.Tools;

namespace PonyForest.Networking.Server.CoreModule
{
    public class ModuleMain : ServerModule
    {
        private readonly Logger _logger;
        private readonly IMessageBroadcaster _messageBroadcaster;
        private readonly IWorld _world;

        public ModuleMain(ILoggerProvider loggerProvider, IMessageBroadcaster messageBroadcaster, IWorld world)
        {
            _logger = loggerProvider.GetLogger("CoreModule");
            _messageBroadcaster = messageBroadcaster;
            _world = world;
            
            _logger.LogInformation("CoreModule was loaded!");
        }

        [MessageHandler(typeof(PlayerJoinMessage))]
        public void PlayerJoin(PlayerJoinMessage message)
        {
            _logger.LogInformation($"{message.Sender.SteamId} joined");

            _world.Players.Add(message.Sender);
            
            ServerPlayerSpawnMessage spawn = new ServerPlayerSpawnMessage
            {
                Position = new NetworkVector3(0f, 10f, 0f),
                Player = message.Sender
            };
            
            _messageBroadcaster.Broadcast(spawn);
            
            ServerPlayerListMessage list = new ServerPlayerListMessage
            {
                Players = _world.Players
            };
            
            _messageBroadcaster.Broadcast(list, message.Sender);
        }

        [MessageHandler(typeof(PlayerPositionMessage))]
        public void PlayerPosition(PlayerPositionMessage message)
        {
            ServerPlayerPositionMessage position = new ServerPlayerPositionMessage
            {
                Player = message.Sender,
                Position = message.Position
            };
            
            _messageBroadcaster.Broadcast(position);
        }
    }
}