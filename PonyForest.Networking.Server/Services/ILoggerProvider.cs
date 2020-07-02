using PonyForestServer.Core.Tools;

namespace PonyForestServer.Core.Services
{
    public interface ILoggerProvider
    {
        Logger GetLogger(string name);
    }
}