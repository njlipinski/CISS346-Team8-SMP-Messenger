using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;
using SMP_Library;

namespace SMPClientProducer
{
    internal class MessageProducer
    {
        public static event EventHandler<SMPResponsePacketEventArgs> SMPResponsePacketRecieved;

        public static void SendSmpPacket(string serverIpAddress, int port, SmpPacket smpPacket)
        {
            TcpClient client = new TcpClient(serverIpAddress, port);
            NetworkStream networkStream = client.GetStream();
            StreamReader reader = new StreamReader(networkStream);


            //Send the SMP packet
            StreamWriter writer = new StreamWriter(networkStream);
            writer.WriteLine(smpPacket.Version);
            writer.WriteLine(smpPacket.UserID);
            writer.WriteLine(smpPacket.Password);
            writer.WriteLine(smpPacket.MessageType);
            writer.WriteLine(smpPacket.Priority);
            writer.WriteLine(smpPacket.DateTime);
            string publicKeyFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public.key");
            string encryptedMessage = CryptographyUtilities.Encryption.EncryptMessage(smpPacket.Message, publicKeyFile);
            writer.WriteLine(encryptedMessage);
            writer.Flush();

            //Receive SMP Response from server
            string responsePacket = reader.ReadLine();

            //Done with the server
            reader.Close();
            writer.Close();

            ProcessSmpResponsePacket(responsePacket);
        }
        private static void ProcessSmpResponsePacket(string responsePacket)
        {
            SMPResponsePacketEventArgs eventArgs = new SMPResponsePacketEventArgs(responsePacket);

            if (SMPResponsePacketRecieved != null) SMPResponsePacketRecieved(null, eventArgs);
        }
    }
}
