using SMP_Library;
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;

namespace SMPClientRegister
{
    public partial class ClientRegistrationForm : Form
    {
        public ClientRegistrationForm()
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

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            //Ask for public key
            string serverAddress = "127.0.0.1";
            int port = 50400;
            string publicKeyFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public.key");
            //Delete old key if it exists
            if (File.Exists(publicKeyFile))
            {
                File.Delete(publicKeyFile);
            }
            requestPublicKey(serverAddress, port);

            // User authentication for 2.0 implementation
            string userID = textBoxUserID.Text;
            string password = textBoxPassword.Text;

            string encryptedUserID = CryptographyUtilities.Encryption.EncryptMessage(userID, publicKeyFile);
            string encryptedPassword = CryptographyUtilities.Encryption.EncryptMessage(password, publicKeyFile);

            //Build the SMP packet
            SmpPacket smpPacket = new SmpPacket(Enumerations.SmpVersion.Version_2_0.ToString(), encryptedUserID, encryptedPassword, 
                Enumerations.SmpMessageType.RegisterUser.ToString(), null, DateTime.Now.ToString(), null);

            //Send the packet
            ClientRegisteration.SendSmpPacket(serverAddress, port, smpPacket);

            MessageBox.Show("User Registration sent...", "Registration Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
