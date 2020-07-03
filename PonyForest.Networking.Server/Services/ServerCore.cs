using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using PonyForestServer.Core.Models.Messages;
using Steamworks;
using Steamworks.Data;

namespace PonyForestServer.Core.Services
{
    internal class ServerCore : SocketManager
    {
        public Action<PlayerMessage> OnMessageReceived { get; set; }
        
        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

        public override void OnMessage(Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel)
        {
            byte[] bytes = new byte[size];
            Marshal.Copy(data, bytes, 0, size);

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(bytes, 0, size);
                stream.Position = 0;

                if (_binaryFormatter.Deserialize(stream) is PlayerMessage message)
                {
                    message.Sender.Connection = connection;
                    OnMessageReceived?.Invoke(message);
                }
            }
        }
    }
}