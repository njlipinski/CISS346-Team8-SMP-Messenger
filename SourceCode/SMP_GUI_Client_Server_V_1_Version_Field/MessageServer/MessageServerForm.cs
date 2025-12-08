using SMP_Library;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SMPServer
{
    public partial class FormSmpServer : Form
    {
        public string IpAddress;
        public int Port;

        public FormSmpServer()
        {
            InitializeComponent();
        }

        private void buttonStartServer_Click(object sender, EventArgs e)
        {
            try
            {
                IpAddress = textBoxServerIPAddress.Text;
                Port = int.Parse(textBoxPortNumber.Text);

                MessageServer.PacketRecieved += SMPServer_PacketRecieved;

                ThreadPool.QueueUserWorkItem(MessageServer.Start, this);

                MessageBox.Show("Server started...", "Server Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

                buttonStartServer.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }

        private void SMPServer_PacketRecieved(object sender, PacketEventArgs eventArgs)
        {
            try
            {
                Invoke(new EventHandler<PacketEventArgs>(SMPPacketReceived), sender, eventArgs);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }

        private void SMPPacketReceived(object sender, PacketEventArgs eventArgs)
        {
            try
            {
                if (eventArgs != null)
                {
                    string messageType = eventArgs.SmpPacket.MessageType.ToString();
                    string messagePriority = eventArgs.SmpPacket.Priority;

                    textBoxMessageType.Text = messageType;
                    textBoxMessagePriority.Text = messagePriority;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }

        private void buttonShowMessages_Click(object sender, EventArgs e)
        {
            textBoxMessages.Clear();
            string privateKeyFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "private.key");

            // Checking which priority button was selected
            string selectedPriority = "";
            if(radioButtonPriorityLow.Checked){
                selectedPriority = "1";
            }else if(radioButtonPriorityMedium.Checked){
                selectedPriority = "2";
            }else if(radioButtonPriorityHigh.Checked){
                selectedPriority = "3";
            }else if(radioAll.Checked){
                selectedPriority = "ALL";
            }

            StreamReader reader = new StreamReader("Messages.txt");

            string version = reader.ReadLine();

            while (version != null)
            {
                // userID and password for 2.0
                string userID = reader.ReadLine();
                string password = reader.ReadLine();
                string priority = reader.ReadLine();
                string dateTime = reader.ReadLine();
                string message = reader.ReadLine();
                string emptyLine = reader.ReadLine();


                // Determine if the message should be shown based on selected priority
                bool show =(selectedPriority == "ALL" || priority == selectedPriority);

                if(show){
                    string record = "Version: " + version + Environment.NewLine;
                    record += "User ID: " + userID + Environment.NewLine;
                    record += "Password: " + password + Environment.NewLine;
                    record += "Priority: " + priority + Environment.NewLine;
                    record += "Date/Time: " + dateTime + Environment.NewLine;
                    // decrypt message for viewing
                    string decryptedMessage = CryptographyUtilities.Encryption.DecryptMessage(message, privateKeyFile);

                    record += "Message: " + decryptedMessage + Environment.NewLine;

                    textBoxMessages.AppendText(record + Environment.NewLine);
                }
                version = reader.ReadLine();
            }

            reader.Close();
        }

        private void buttonShowUsers_Click(object sender, EventArgs e)
        {
            registrationsForm.ShowDialog();
        }
        private void buttonShowRegistrations_Click(object sender, EventArgs e)
        {
            string userRecords = null;
            if(radioButtonUserIDs.Checked)
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
            else if(radioButtonUserIDsandPasswords.Checked)
            {
                StreamReader reader = new StreamReader("Users.txt");
                string privateKeyFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "private.key");
                string line = reader.ReadLine();
                while(line != null)
                {
                    userRecords += line + ", ";
                    string password = CryptographyUtilities.Encryption.DecryptMessage(reader.ReadLine(), privateKeyFile);
                    userRecords += password + Environment.NewLine;
                    reader.ReadLine(); // skip date line
                    reader.ReadLine(); // skip empty line
                    line = reader.ReadLine(); // read next user ID
                }
                reader.Close();
            }
            textBoxRegistrations.Text = userRecords;
        }
    }
}
