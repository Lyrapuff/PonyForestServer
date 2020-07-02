using System.IO;
using Newtonsoft.Json;
using PonyForestServer.Core.Models;
using PonyForestServer.Core.Tools;

namespace PonyForestServer.Core.Services.Implementation
{
    public class ConfigProvider : IConfigProvider
    {
        public ServerConfig Config { get; private set; }

        private readonly Logger _logger;
        
        public ConfigProvider(ILoggerProvider loggerProvider)
        {
            _logger = loggerProvider.GetLogger("Server");
            
            Load();
        }
        
        public void Load()
        {
            string path = "Config.json";

            if (File.Exists(path))
            {
                Config = JsonConvert.DeserializeObject<ServerConfig>(File.ReadAllText(path));
            }
            else
            {
                Config = new ServerConfig();
                
                string json = JsonConvert.SerializeObject(Config, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            
            _logger.LogInformation("Config was loaded");
        }
    }
}