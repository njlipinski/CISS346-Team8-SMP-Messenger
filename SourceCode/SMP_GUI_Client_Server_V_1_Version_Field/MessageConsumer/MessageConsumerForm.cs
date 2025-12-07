using SMP_Library;
using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SMPClientConsumer
{
    public partial class MessageConsumerForm : Form
    {
        public MessageConsumerForm()
        {
            InitializeComponent();

            MessageConsumer.SMPResponsePacketRecieved += SMPClientConsumer_SMPResponsePacketRecieved;
        }

        private void requestPublicKey(string serverIP, int port)
        {
            try
            {
                TcpClient client = new TcpClient(serverIP, port);
                NetworkStream networkStream = client.GetStream();

                // Request the public key
                StreamWriter writer = new StreamWriter(networkStream);
                writer.WriteLine("getKey");
                writer.Flush();

                // Receive the public key
                StreamReader reader = new StreamReader(networkStream);
                string publicKeyContent = reader.ReadToEnd();

                // Save it locally
                string publicKeyFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public.key");
                File.WriteAllText(publicKeyFile, publicKeyContent);

                reader.Close();
                writer.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to get public key: " + ex.Message);
            }
        }

        private void buttonGetMessage_Click(object sender, EventArgs e)
        {
            //Ask for public key
            string serverAddress = textBoxServerIPAddress.Text;
            int port = int.Parse(textBoxApplicationPortNumber.Text);
            string publicKeyFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public.key");
            //Delete old key if it exists
            if (File.Exists(publicKeyFile))
            {
                File.Delete(publicKeyFile);
            }
            requestPublicKey(serverAddress, port);

            //Get the message priority
            int priority;

            if (radioButtonPriorityLow.Checked == true)
            {
                priority = 1;
            }
            else if (radioButtonPriorityMedium.Checked == true)
            {
                priority = 2;
            }
            else
            {
                priority = 3;
            }

            // Encrypted userID and password for 3.0 implementation
            string encryptedUserID = CryptographyUtilities.Encryption.EncryptMessage(textBoxUserID.Text, publicKeyFile);
            string encryptedPassword = CryptographyUtilities.Encryption.EncryptMessage(textBoxPassword.Text, publicKeyFile);

            //Build the SMP packet
            SmpPacket smpPacket = new SmpPacket(Enumerations.SmpVersion.Version_2_0.ToString(), encryptedUserID, encryptedPassword,
                Enumerations.SmpMessageType.GetMessage.ToString(), priority.ToString(), null, null);

            //Send the packet
            MessageConsumer.SendSmpPacket(serverAddress, port, smpPacket);
            MessageBox.Show("Message retrieved...", "Message Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SMPClientConsumer_SMPResponsePacketRecieved(object sender, SMPResponsePacketEventArgs e)
        {
            try
            {
                Invoke(new EventHandler<SMPResponsePacketEventArgs>(SMPResponsePacketRecieved), sender, e);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }
        private void SMPResponsePacketRecieved(object sender, SMPResponsePacketEventArgs eventArgs)
        {
            try
            {
                textBoxMessageContent.Text = eventArgs.ResponseMessage;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }
    }
}
