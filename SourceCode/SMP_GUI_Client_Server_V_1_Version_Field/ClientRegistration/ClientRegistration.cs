using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;
using SMP_Library;

namespace SMPClientRegister
{
    internal class ClientRegisteration
    {
        public static event EventHandler<SMPResponsePacketEventArgs> SMPResponsePacketRecieved;

        public static string SendSmpPacket(string serverIpAddress, int port, SmpPacket smpPacket)
        {
            TcpClient client = new TcpClient(serverIpAddress, port);
            NetworkStream networkStream = client.GetStream();
            StreamReader reader = new StreamReader(networkStream);

            //Send the SMP packet
            StreamWriter writer = new StreamWriter(networkStream);
            writer.WriteLine(smpPacket.Version);
            writer.WriteLine(smpPacket.UserID);
            writer.WriteLine(smpPacket.EncryptedPassword);
            writer.WriteLine(smpPacket.MessageType);
            writer.WriteLine(smpPacket.DateTime);
            writer.Flush();

            //Receive SMP Response from server
            string responsePacket = reader.ReadToEnd();

            //Done with the server
            reader.Close();
            writer.Close();

            return responsePacket;
        }
    }
}
