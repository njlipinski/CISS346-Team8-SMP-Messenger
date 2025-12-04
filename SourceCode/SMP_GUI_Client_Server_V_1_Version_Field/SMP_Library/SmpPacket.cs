using System;

namespace SMP_Library
{
    public class SmpPacket
    {
        public string Version;
        public string MessageType;
        public string Priority;
        public string DateTime;
        public string Message;

        // Added these for our 2.0 implementation
        public string UserID;
        // Modified for 3.0 implementation
        public string EncryptedPassword;

        public SmpPacket(string version, string userID, string encryptedPassword, string messageType, string priority, string dateTime, string message)
        {
            Version = version;
            UserID = userID;
            EncryptedPassword = encryptedPassword;
            MessageType = messageType;
            Priority = priority;
            DateTime = dateTime;
            Message = message;
        }

        public override string ToString()
        {
            string packet = Version + Environment.NewLine;
            packet += UserID + Environment.NewLine;
            packet += EncryptedPassword + Environment.NewLine;
            packet += MessageType + Environment.NewLine;
            packet += Priority + Environment.NewLine;
            packet += DateTime + Environment.NewLine;
            packet += Message + Environment.NewLine;

            return packet;
        }
    }
}
