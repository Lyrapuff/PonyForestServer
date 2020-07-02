using PonyForestServer.Core.Models;

namespace PonyForestServer.Core.Services
{
    public interface IConfigProvider
    {
        ServerConfig Config { get; }
        void Load();
    }
}