using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmClient : Form
    {
        private Thread thread;
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private StreamReader streamReader;
        private StreamWriter streamWriter;

        public delegate void textChange(string text);

        public frmClient()
        {
            InitializeComponent();
        }

        private void frmClient_Load(object sender, EventArgs e)
        {
            comboBoxFill();
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            Connection();
        }

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
        /// Server'e Bağlan
        /// </summary>
        private void Connection()
        {
            try
            {
                string ipAddress = comboBoxActiveIPAdress.SelectedValue.ToString();
                tcpClient = new TcpClient(ipAddress, Convert.ToInt32(txtPort.Text));
                thread = new Thread(new ThreadStart(StartRead));
                thread.Start();
                lblConnectionState.Text = DateTime.Now.ToString() + " Baglanti kuruldu...\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server ile baglanti kurulurken hata olustu !");
            }
        }

        /// <summary>
        /// Okmaya Başla
        /// </summary>
        private void StartRead()
        {
            networkStream = tcpClient.GetStream();
            streamReader = new StreamReader(networkStream);
            while (true)
            {
                try
                {
                    string yazi = streamReader.ReadLine();
                    WriteOnScreen(yazi);
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
                textChange degis = new textChange(WriteOnScreen);
                this.Invoke(degis, s);
            }
            else
            {
                s = "Server: " + s;
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
                streamWriter = new StreamWriter(networkStream);
                streamWriter.WriteLine(txtSendMessage.Text);
                streamWriter.Flush();
                txtGetMessage.AppendText(txtSendMessage.Text + "\n");
                txtSendMessage.Text = "";
            }
        }
    }
}