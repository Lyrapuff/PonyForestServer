using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using PonyForestServer.Core.Models.Messages;
using PonyForestServer.Core.World;
using Steamworks;
using Steamworks.Data;

namespace PonyForestServer.Core.Services
{
    internal class Server : SocketManager
    {
        public Action<MessageBase> OnMessagsReceived { get; set; }
        
        private readonly List<Player> _players = new List<Player>();
        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();
        
        public override void OnConnecting(Connection connection, ConnectionInfo info)
        {
            base.OnConnecting(connection, info);
            
            connection.Accept();
        }

        public override void OnConnected(Connection connection, ConnectionInfo info)
        {
            base.OnConnected(connection, info);
            
            Player player = new Player
            {
                SteamId = info.Identity.SteamId.Value
            };
            
            _players.Add(player);
        }

        public override void OnDisconnected(Connection connection, ConnectionInfo info)
        {
            Player player = _players.FirstOrDefault(p => p.SteamId == info.Identity.SteamId.Value);

            if (player != null)
            {
                _players.Remove(player);
            }
        }

        public override void OnMessage(Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel)
        {
            byte[] bytes = new byte[size];
            Marshal.Copy(data, bytes, 0, size);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(bytes, 0, size);
                stream.Position = 0;

                MessageBase message = _binaryFormatter.Deserialize(stream) as MessageBase;

                if (message != null)
                {
                    OnMessagsReceived?.Invoke(message);
                }
            }
        }
    }
}