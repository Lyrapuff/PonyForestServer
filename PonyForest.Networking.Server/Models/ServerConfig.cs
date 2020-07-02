namespace PonyForestServer.Core.Models
{
    public class ServerConfig
    {
        public string IpAddress { get; } = "0.0.0.0";
        public ushort Port { get; } = 54321;
        public int TickRate { get; } = 20;
        public int MaxPlayers { get; } = 10;
        public string Name { get; } = "Pony Forest: Yet another server";
    }
}