using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public partial class frmServer : Form
    {
        //Yine gerekli Siniflarin nesneleri tanimlaniyor
        private Thread thread;

        private IPAddress myIpAddress;
        private TcpListener tcpListener;
        private Socket socket;
        private NetworkStream networkStream;
        private StreamReader streamReader;
        private StreamWriter streamWriter;

        public delegate void textChange(string text);

        public frmServer()
        {
            InitializeComponent();
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            comboBoxFill();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            StartListening();
        }

        /// <summary>
        /// Combobox Doldur
        /// </summary>
        private void comboBoxFill()
        {
            //Aktif IP adresleri bulunup combobox a ekleniyor.ç
            Dictionary<string, IPAddress> comboFill = new Dictionary<string, IPAddress>();
            foreach (NetworkInterface NI in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation IP in NI.GetIPProperties().UnicastAddresses)
                {
                    if (IP.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        comboFill.Add(IP.Address.ToString() + " - " + NI.Description, IP.Address);
                    }
                }
            }

            comboBoxActiveIPAdress.DataSource = new BindingSource(comboFill, null);
            comboBoxActiveIPAdress.DisplayMember = "Key";
            comboBoxActiveIPAdress.ValueMember = "Value";
        }

        /// <summary>
        /// Dinlemeyi Başlat
        /// </summary>
        private void StartListening()
        {
            try
            {
                string ipAddress = comboBoxActiveIPAdress.SelectedValue.ToString();
                myIpAddress = IPAddress.Parse(ipAddress);
                tcpListener = new TcpListener(myIpAddress, Convert.ToInt32(txtPort.Text));
                tcpListener.Start();
                thread = new Thread(new ThreadStart(StartRead));
                thread.Start();
                lblConnectionState.Text = DateTime.Now.ToString() + " Dinleme baslatildi..\n";
            }
            catch (Exception)
            {
                MessageBox.Show("Dinleme baslatilamadi!");
            }
        }

        /// <summary>
        /// Okumayı Başlat
        /// </summary>
        private void StartRead()
        {
            socket = tcpListener.AcceptSocket();
            networkStream = new NetworkStream(socket);
            streamReader = new StreamReader(networkStream);
            while (true)
            {
                try
                {
                    string text = streamReader.ReadLine();
                    WriteOnScreen(text);
                }
                catch
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Ekrana Yaz
        /// </summary>
        /// <param name="s"></param>
        private void WriteOnScreen(string s)
        {
            //Okunan Veri TextBox icine yaziliyor
            if (this.InvokeRequired)
            {
                textChange change = new textChange(WriteOnScreen);
                this.Invoke(change, s);
            }
            else
            {
                s = "Cilent: " + s;
                txtGetMessage.AppendText(s + "\n");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtSendMessage.Text == "")
            {
                MessageBox.Show("Mesaj alanı boş!");
            }
            else
            {
                if (networkStream != null)
                {
                    streamWriter = new StreamWriter(networkStream);
                    streamWriter.WriteLine(txtSendMessage.Text);
                    streamWriter.Flush();
                    txtGetMessage.AppendText(txtSendMessage.Text + "\n");
                    txtSendMessage.Text = "";
                }
                else
                {
                    MessageBox.Show("Client ile bağlantı kurulmadı!");
                }
            }
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            networkStream.Close();
        }
    }
}