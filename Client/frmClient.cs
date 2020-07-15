using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
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
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            Connection();
        }

        /// <summary>
        /// Server'e Bağlan
        /// </summary>
        private void Connection()
        {
            try
            {
                tcpClient = new TcpClient(maskedTextBoxIPAddress.Text, Convert.ToInt32(txtPort.Text));
                thread = new Thread(new ThreadStart(StartRead));
                thread.Start();
                lblConnectionState.Text = DateTime.Now.ToString() + " Baglanti kuruldu...\n";
            }
            catch (Exception)
            {
                lblConnectionState.Text = "Server ile baglanti kurulurken hata oluştu!";
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