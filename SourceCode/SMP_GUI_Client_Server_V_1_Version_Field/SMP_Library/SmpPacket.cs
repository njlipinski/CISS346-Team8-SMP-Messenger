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
        public string Password;

        public SmpPacket(string version, string userID, string password, string messageType, string priority, string dateTime, string message)
        {
            Version = version;
            UserID = userID;
            Password = password;
            MessageType = messageType;
            Priority = priority;
            DateTime = dateTime;
            Message = message;
        }

        public override string ToString()
        {
            string packet = Version + Environment.NewLine;
            packet += UserID + Environment.NewLine;
            packet += Password + Environment.NewLine;
            packet += MessageType + Environment.NewLine;
            packet += Priority + Environment.NewLine;
            packet += DateTime + Environment.NewLine;
            packet += Message + Environment.NewLine;

            return packet;
        }
    }
}
