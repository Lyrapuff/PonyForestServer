using System.Threading.Tasks;
using PonyForestServer.Core.Services.Implementation;

namespace ConsoleWrapper
{
    internal static class Program
    {
        public static async Task Main()
        {
            await new ServerSetup().SetupAsync();
            await Task.Delay(-1);
        }
    }
}