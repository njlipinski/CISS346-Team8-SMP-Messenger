using SMP_Library;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace SMPServer
{
    internal class MessageServer
    {
        public static event EventHandler<PacketEventArgs> PacketRecieved;

        public static void Start(object o)
        {
            FormSmpServer form = o as FormSmpServer;

            if (form != null)
            {
                IPAddress iPAddress = IPAddress.Parse(form.IpAddress);
                int port = form.Port;

                TcpListener server = new TcpListener(iPAddress, port);

                server.Start();

                while (true)
                {
                    TcpClient connection = server.AcceptTcpClient();

                    ProcessConnection(connection);
                }
            }
        }

        public static void ProcessConnection(TcpClient connection)
        {
            NetworkStream networkStream = connection.GetStream();

            StreamReader networkStreamReader = new StreamReader(networkStream);

            string version = networkStreamReader.ReadLine();

            if (version == Enumerations.SmpVersion.Version_1_0.ToString())
            {
                string messageType = networkStreamReader.ReadLine();

                if (messageType == Enumerations.SmpMessageType.PutMessage.ToString())
                {
                    string priority = networkStreamReader.ReadLine();
                    string dateTime = networkStreamReader.ReadLine();
                    string message = networkStreamReader.ReadLine();

                    SmpPacket smpPacket = new SmpPacket(version, messageType, priority, dateTime, message);

                    ProcessSmpPutPacket(smpPacket);

                    string responsePacket = "Received Packet: " + DateTime.Now + Environment.NewLine;

                    SendSmpResponsePacket(responsePacket, networkStream);

                    networkStreamReader.Close();

                    PacketEventArgs eventArgs = new PacketEventArgs(smpPacket);

                    if (PacketRecieved != null) PacketRecieved(null, eventArgs);
                }
                else if (messageType == Enumerations.SmpMessageType.GetMessage.ToString())
                {
                    string priority = networkStreamReader.ReadLine();

                    SmpPacket smpPacket = ProcessSmpGetPacket(priority);

                    string record = smpPacket.DateTime + Environment.NewLine;
                    record += smpPacket.Message + Environment.NewLine;

                    string responsePacket = "Message Information: " + Environment.NewLine + record;

                    SendSmpResponsePacket(responsePacket, networkStream);

                    networkStreamReader.Close();

                    PacketEventArgs eventArgs = new PacketEventArgs(smpPacket);

                    if (PacketRecieved != null) PacketRecieved(null, eventArgs);
                }
            }
            else
            {
                string responsePacket = "Unsupported Version: " + version + Environment.NewLine;

                SendSmpResponsePacket(responsePacket, networkStream);

                networkStreamReader.Close();
            }
        }

        private static void ProcessSmpPutPacket(SmpPacket smpPacket)
        {
            try
            {
                if (smpPacket != null)
                {
                    string record = smpPacket.Version + Environment.NewLine;
                    record += smpPacket.Priority + Environment.NewLine;
                    record += smpPacket.DateTime + Environment.NewLine;
                    record += smpPacket.Message + Environment.NewLine;

                    StreamWriter writer = new StreamWriter("Messages.txt", true);

                    writer.WriteLine(record);
                    writer.Flush();

                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }

        private static SmpPacket ProcessSmpGetPacket(string priority)
        {
            SmpPacket smpPacket = null;

            try
            {
                // List to store all the messages read from the file
                List<SmpPacket> allMessages = new List<SmpPacket>();

                StreamReader reader = new StreamReader("Messages.txt");
                
                string smpVersion = reader.ReadLine();

                // Read every message in the file and store it in the list
                while(smpVersion != null){
                    string msgPriority = reader.ReadLine();
                    string dateTime = reader.ReadLine();
                    string message = reader.ReadLine();
                    // Read the empty line that seperates each message
                    string emptyLine = reader.ReadLine();

                    SmpPacket packet = new SmpPacket(smpVersion,Enumerations.SmpMessageType.PutMessage.ToString(),msgPriority,dateTime,message);

                    allMessages.Add(packet);

                    smpVersion = reader.ReadLine();
                }
                reader.Close();

                // Search for first message that matches the requested priority
                SmpPacket found = null;
                for(int i = 0; i < allMessages.Count; i++){
                    if(allMessages[i].Priority == priority){
                        found = allMessages[i];
                        break;
                    }
                }

                // If no message with that requested priority exists let the user know
                if(found == null){
                    return new SmpPacket("1.0",Enumerations.SmpMessageType.GetMessage.ToString(),priority,"N/A","NO MESSAGES HAVE THAT PRIORITY");
                }
                smpPacket = found;
                // Remove the found message from the list
                allMessages.Remove(found);

                StreamWriter writer = new StreamWriter("Messages.txt", false);

                // Rewrite the Messages.txt file without the consumed message
                for(int i = 0; i < allMessages.Count; i++){
                    writer.WriteLine(allMessages[i].Version);
                    writer.WriteLine(allMessages[i].Priority);
                    writer.WriteLine(allMessages[i].DateTime);
                    writer.WriteLine(allMessages[i].Message);
                    writer.WriteLine();
                }
                writer.Close();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }

            return smpPacket;
        }
        private static void SendSmpResponsePacket(String responsePacket, NetworkStream dataStream)
        {
            StreamWriter writer = new StreamWriter(dataStream);

            writer.WriteLine(responsePacket);

            writer.Close();
        }
    }
}
