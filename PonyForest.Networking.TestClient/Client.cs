using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using PonyForestServer.Core.Models.Messages;
using PonyForestServer.Core.Models;
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

            
        }

        public override void OnMessage(IntPtr data, int size, long messageNum, long recvTime, int channel)
        {
            Console.WriteLine("message came");
        }
    }
}