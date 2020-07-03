using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PonyForestServer.Core.Models;
using PonyForestServer.Core.Tools;
using Steamworks;
using Steamworks.Data;

namespace PonyForestServer.Core.Services.Implementation
{
    public class ServerSetup : IServerSetup
    {
        public static IServiceProvider ServiceProvider;
        private ServerCore _serverCore;
        
        public async Task SetupAsync()
        {
            ConfigureServices();

            Logger logger = ServiceProvider.GetService<ILoggerProvider>()
                .GetLogger("Server");
            
            logger.LogInformation("Starting server");
            
            ServerConfig config = ServiceProvider.GetService<IConfigProvider>().Config;

            SteamServerInit init = new SteamServerInit
            {
                GamePort = config.Port,
                Secure = true,
                QueryPort = (ushort) (config.Port + 1),
                SteamPort = (ushort) (config.Port + 2),
                VersionString = "0.1",
                DedicatedServer = true,
                IpAddress = IPAddress.Parse(config.IpAddress),
                ModDir = "pf",
                GameDescription = "Pony Forest"
            };

            try
            {
                SteamServer.Init(1026040, init);
                
                SteamServer.ServerName = config.Name;
                SteamServer.AutomaticHeartbeats = true;
                SteamServer.DedicatedServer = true;
                SteamServer.Passworded = false;
                SteamServer.MaxPlayers = config.MaxPlayers;
                
                SteamServer.LogOnAnonymous();
                
                NetAddress address = NetAddress.From(config.IpAddress, config.Port);
                _serverCore = SteamNetworkingSockets.CreateNormalSocket<ServerCore>(address);
            }
            catch (Exception e)
            {
                logger.LogFailure($"Can't start server: {Environment.NewLine + e}");
            }

            try
            {
                IMessageRouter messageRouter = ServiceProvider.GetService<IMessageRouter>();

                logger.LogInformation("Server started");

                _serverCore.OnMessageReceived = messageRouter.Route;

                IMessageListener messageListener = ServiceProvider.GetService<IMessageListener>();
                await messageListener.StartListening(_serverCore);
            }
            catch (Exception e)
            {
                logger.LogFailure($"Error while running server: {Environment.NewLine + e}");
            }
        }

        private void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services
                .AddSingleton<ILoggerProvider, LoggerProvider>()
                .AddSingleton<IConfigProvider, ConfigProvider>()
                .AddSingleton<IModuleProvider, ModuleProvider>()
                .AddSingleton<IMessageListener, MessageListener>()
                .AddSingleton<IMessageRouter, MessageRouter>()
                .AddSingleton<IMessageBroadcaster, MessageBroadcaster>()
                .AddSingleton<IWorld, World>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}