using SMP_Library;
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;

namespace SMPClientProducer
{
    public partial class MessageProducerForm : Form
    {
        public MessageProducerForm()
        {
            InitializeComponent();

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

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
            //Ask for public key
            string serverAddress = textBoxServerIPAddress.Text;
            int port = int.Parse(textBoxApplicationPortNumber.Text);
            string publicKeyFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public.key");
            if (!File.Exists(publicKeyFile))
            {
                requestPublicKey(serverAddress, port);
            }
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

            string message = textBoxMessageContent.Text;

            // User authentication for 2.0 implementation
            string userID = textBoxUserID.Text;
            string password = textBoxPassword.Text;
            //Build the SMP packet
            SmpPacket smpPacket = new SmpPacket(Enumerations.SmpVersion.Version_2_0.ToString(),userID, password, Enumerations.SmpMessageType.PutMessage.ToString(), priority.ToString(), DateTime.Now.ToString(),message);

            //Send the packet
            MessageProducer.SendSmpPacket(textBoxServerIPAddress.Text,
                int.Parse(textBoxApplicationPortNumber.Text), smpPacket);

            MessageBox.Show("Message sent...", "Message Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SMPClientProducer_SMPResponsePacketRecieved(object sender, SMPResponsePacketEventArgs e)
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
                textBoxServerResponse.Text = eventArgs.ResponseMessage;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }
    }
}
