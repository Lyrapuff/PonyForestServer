using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PonyForestServer.Core.Services.Implementation
{
    public class ServerSetup : IServerSetup
    {
        private IServiceProvider _serviceProvider;
        
        public Task SetupAsync()
        {
            ConfigureServices();
            
            return Task.CompletedTask;
        }

        private void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            
            services
                .AddSingleton<ILoggerProvider, LoggerProvider>()
                .AddSingleton<IConfigProvider, ConfigProvider>()
                .AddSingleton<Server>();
            
            _serviceProvider = services.BuildServiceProvider();
            
            // triggering the server to trigger it's constructor
            _serviceProvider.GetService<Server>();
        }
    }
}