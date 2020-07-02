using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PonyForestServer.Core.Services.Implementation
{
    public class ServerSetup : IServerSetup
    {
        private IServiceProvider _serviceProvider;
        
        public async Task SetupAsync()
        {
            ConfigureServices();
        }

        private void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            _serviceProvider = services.BuildServiceProvider();
        }
    }
}