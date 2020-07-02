using System.Threading.Tasks;

namespace PonyForestServer.Core.Services
{
    public interface IServerSetup
    {
        Task SetupAsync();
    }
}