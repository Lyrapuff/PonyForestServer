using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using PonyForestServer.Core.Models.Messages;
using PonyForestServer.Core.World;
using Steamworks;
using Steamworks.Data;

namespace PonyForest.Networking.TestClient
{
    public class Client : ConnectionManager
    {
        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

        public override void OnConnected(ConnectionInfo info)
        {
            base.OnConnected(info);
            
            Console.WriteLine("Connected, sending data");

            try
            {
                PlayerConnectionMessage message = new PlayerConnectionMessage
                {
                    Sender = new Player
                    {
                        SteamId = SteamClient.SteamId.Value
                    }
                };

                using (MemoryStream stream = new MemoryStream())
                {
                    _binaryFormatter.Serialize(stream, message);
                    stream.Position = 0;

                    byte[] bytes = stream.ToArray();

                    Connection.SendMessage(bytes, 0, bytes.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override void OnMessage(IntPtr data, int size, long messageNum, long recvTime, int channel)
        {
            Console.WriteLine("message came");
        }
    }
}