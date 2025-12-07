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
                // Clear Messages.txt when server starts
                File.WriteAllText("Messages.txt", string.Empty);

                // Clear Users.txt when server starts
                File.WriteAllText("Users.txt", string.Empty);

                // Generate new RSA key pair
                string publicKeyFile = "public.key";
                string privateKeyFile = "private.key";

                // Delete old keys if they exist
                if (File.Exists(publicKeyFile))
                {
                    File.Delete(publicKeyFile);
                }
                if (File.Exists(privateKeyFile))
                {
                    File.Delete(privateKeyFile);
                }

                CryptographyUtilities.Encryption.GeneratePublicPrivateKeyPair(publicKeyFile, privateKeyFile);

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
            string privateKeyFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "private.key");

            if (version == "getKey")
            {
                string publicKeyContent = File.ReadAllText("public.key");
                StreamWriter writer = new StreamWriter(networkStream);
                writer.Write(publicKeyContent);

                writer.Flush();
                writer.Close();

                networkStreamReader.Close();
                return;
            } else if (version == Enumerations.SmpVersion.Version_2_0.ToString())
            {
                // User ID and password for 2.0 implementation
                // Modified for 3.0 implementation
                // TODO: Implement authentication
                string encryptedUserID = networkStreamReader.ReadLine();
                string encryptedPassword = networkStreamReader.ReadLine();
                string messageType = networkStreamReader.ReadLine();
                
                string userID = CryptographyUtilities.Encryption.DecryptMessage(encryptedUserID, privateKeyFile);
                string password = CryptographyUtilities.Encryption.DecryptMessage(encryptedPassword, privateKeyFile);

                if (messageType == Enumerations.SmpMessageType.PutMessage.ToString())
                {
                    if (!ProcessAuthenticateUser(userID, password))
                    {
                        SendSmpResponsePacket("AUTHENTICATION FAILED", networkStream);
                        networkStreamReader.Close();
                        return;
                    }
                    string priority = networkStreamReader.ReadLine();
                    string dateTime = networkStreamReader.ReadLine();
                    string message = networkStreamReader.ReadLine();

                    SmpPacket smpPacket = new SmpPacket(version, userID, encryptedPassword, messageType, priority, dateTime, message);

                    ProcessSmpPutPacket(smpPacket);

                    string responsePacket = "Received Packet: " + DateTime.Now + Environment.NewLine;

                    SendSmpResponsePacket(responsePacket, networkStream);

                    networkStreamReader.Close();

                    PacketEventArgs eventArgs = new PacketEventArgs(smpPacket);

                    if (PacketRecieved != null) PacketRecieved(null, eventArgs);
                }
                else if (messageType == Enumerations.SmpMessageType.GetMessage.ToString())
                {
                    if (!ProcessAuthenticateUser(userID, password))
                    {
                        SendSmpResponsePacket("AUTHENTICATION FAILED", networkStream);
                        networkStreamReader.Close();
                        return;
                    }
                    string priority = networkStreamReader.ReadLine();

                    SmpPacket smpPacket = ProcessSmpGetPacket(priority);

                    // No message with that priority exists at all
                    if(smpPacket.Message == "NO MESSAGES HAVE THAT PRIORITY"){
                        SendSmpResponsePacket("NO MESSAGES HAVE THAT PRIORITY", networkStream);
                        return;
                    }
                    // Message exists but belongs to a different user
                    if(smpPacket.UserID != userID){
                        SendSmpResponsePacket("NO MESSAGE WITH THAT PRIORITY EXISTS FOR THIS USER", networkStream);
                        return;
                    }
                    // Message exists but user enters wrong password
                    string decryptedPassword = CryptographyUtilities.Encryption.DecryptMessage(smpPacket.EncryptedPassword, privateKeyFile);
                    if (decryptedPassword != password)
                    {
                        SendSmpResponsePacket("INCORRECT PASSWORD", networkStream);
                        return;
                    }

                    // Remove only the authenticated matching message
                    List<SmpPacket> allMessages = new List<SmpPacket>();

                    StreamReader reader = new StreamReader("Messages.txt");
                    string smpVersion = reader.ReadLine();

                    while(smpVersion != null){
                        string userID2 = reader.ReadLine();
                        string password2 = reader.ReadLine();
                        string msgPriority = reader.ReadLine();
                        string dateTime = reader.ReadLine();
                        string message = reader.ReadLine();
                        string emptyLine = reader.ReadLine();

                        // match all the fields to see if this is the message that we want consumed
                        bool isSameMessage =(userID2 == smpPacket.UserID) &&(password2 == smpPacket.EncryptedPassword) &&(msgPriority == smpPacket.Priority) &&(dateTime == smpPacket.DateTime) &&(message == smpPacket.Message);

                        // Keep everything except the message we want to consume
                        if(!isSameMessage){
                            SmpPacket packet = new SmpPacket(
                                smpVersion,userID2,password2,Enumerations.SmpMessageType.PutMessage.ToString(),msgPriority,dateTime,message);
                            allMessages.Add(packet);
                        }
                        smpVersion = reader.ReadLine();
                    }

                    reader.Close();

                    // Rewrite the file with remaining messages
                    StreamWriter writer = new StreamWriter("Messages.txt", false);

                    for(int i = 0; i < allMessages.Count; i++){
                        writer.WriteLine(allMessages[i].Version);
                        writer.WriteLine(allMessages[i].UserID);
                        writer.WriteLine(allMessages[i].EncryptedPassword);
                        writer.WriteLine(allMessages[i].Priority);
                        writer.WriteLine(allMessages[i].DateTime);
                        writer.WriteLine(allMessages[i].Message);
                        writer.WriteLine();
                    }
                    writer.Close();

                    // decrypt message for consumer
                    string decryptedMessage = CryptographyUtilities.Encryption.DecryptMessage(smpPacket.Message, privateKeyFile);

                    string record = smpPacket.DateTime + Environment.NewLine;
                    record += decryptedMessage + Environment.NewLine;

                    string responsePacket = "Message Information: " + Environment.NewLine + record;

                    SendSmpResponsePacket(responsePacket, networkStream);

                    networkStreamReader.Close();

                    PacketEventArgs eventArgs = new PacketEventArgs(smpPacket);

                    if (PacketRecieved != null) PacketRecieved(null, eventArgs);
                } 
                else if (messageType == Enumerations.SmpMessageType.RegisterUser.ToString())
                {
                    string dateTime = networkStreamReader.ReadLine();
                    SmpPacket smpPacket = new SmpPacket(version, userID, encryptedPassword, messageType, null, dateTime, null);
                    string responsePacket = "User Registered: " + userID + Environment.NewLine + DateTime.Now;
                    
                    if (ProcessSmpPutUser(smpPacket))
                    {
                        responsePacket = "Error: " + userID + " Already Exists: " + Environment.NewLine + DateTime.Now;
                    }

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
                    // 2.0 Add UserID and Password to the record
                    record += smpPacket.UserID + Environment.NewLine;
                    record += smpPacket.EncryptedPassword + Environment.NewLine;
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
        private static bool ProcessSmpPutUser(SmpPacket smpPacket)
        {
            bool userExists = false;
            try
            {
                string currentUsers = ProcessSmpGetUsers();
                if (smpPacket != null && !currentUsers.Contains(smpPacket.UserID))
                {
                    // 3.0 Add UserID and Password to the users record
                    string user = smpPacket.UserID + Environment.NewLine;
                    user += smpPacket.EncryptedPassword + Environment.NewLine;
                    user += smpPacket.DateTime + Environment.NewLine;

                    StreamWriter writer = new StreamWriter("Users.txt", true);

                    writer.WriteLine(user);
                    writer.Flush();

                    writer.Close();
                } else
                {
                    userExists = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
            return userExists;
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
                    // 2.0 read UserID and Password
                    string userID = reader.ReadLine();
                    string password = reader.ReadLine();
                    string msgPriority = reader.ReadLine();
                    string dateTime = reader.ReadLine();
                    string message = reader.ReadLine();
                    // Read the empty line that seperates each message
                    string emptyLine = reader.ReadLine();

                    SmpPacket packet = new SmpPacket(smpVersion,userID,password,Enumerations.SmpMessageType.PutMessage.ToString(),msgPriority,dateTime,message);

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
                    return new SmpPacket(Enumerations.SmpVersion.Version_2_0.ToString(), "N/A", "N/A",Enumerations.SmpMessageType.GetMessage.ToString(),priority,"N/A","NO MESSAGES HAVE THAT PRIORITY");
                }
                smpPacket = found;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }

            return smpPacket;
        }
        private static string ProcessSmpGetUsers()
        {
            string userRecords = null;
            try
            {
                StreamReader reader = new StreamReader("Users.txt");
                string line = reader.ReadLine();
                while(line != null)
                {
                    userRecords += line + Environment.NewLine;
                    reader.ReadLine(); // skip password line
                    reader.ReadLine(); // skip date line
                    reader.ReadLine(); // skip empty line
                    line = reader.ReadLine(); // read next user ID
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
            return userRecords + Environment.NewLine;
        }
        private static string ProcessSmpGetUsersandPasswords()
        {
            string userRecords = null;
            try
            {
                StreamReader reader = new StreamReader("Users.txt");
                string privateKeyFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "private.key");
                string line = reader.ReadLine();
                while(line != null)
                {
                    userRecords += line + Environment.NewLine;
                    string password = CryptographyUtilities.Encryption.DecryptMessage(reader.ReadLine(), privateKeyFile);
                    userRecords += password + Environment.NewLine;
                    reader.ReadLine(); // skip date line
                    reader.ReadLine(); // skip empty line
                    line = reader.ReadLine(); // read next user ID
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
            return userRecords + Environment.NewLine;
        }
        private static bool ProcessAuthenticateUser(string userID, string password)
        {
            bool authenticated = false;
            try
            {
                StreamReader reader = new StreamReader("Users.txt");
                string privateKey = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "private.key");
                string line = reader.ReadLine();
                while(line != null)
                {
                    if(line == userID)
                    {
                        string encryptedPassword = reader.ReadLine();
                        string decryptedPassword = CryptographyUtilities.Encryption.DecryptMessage(encryptedPassword, privateKey);
                        if(decryptedPassword == password)
                        {
                            authenticated = true;
                            break;
                        }
                    } else
                    {
                        reader.ReadLine(); // skip password line
                    }
                    reader.ReadLine(); // skip date line
                    reader.ReadLine(); // skip empty line
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
            return authenticated;
        }
        private static void SendSmpResponsePacket(String responsePacket, NetworkStream dataStream)
        {
            StreamWriter writer = new StreamWriter(dataStream);

            writer.WriteLine(responsePacket);

            writer.Close();
        }
    }
}
