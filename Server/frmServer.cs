using Entities.Concrete;
using Entities.Concrete.Request;
using Server.Helpers;
using Services.Abstract;
using Services.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class frmServer : Form
    {
        private readonly IServerService _serverService = new ServerManager();

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
            ControlHelpers.ComboBoxFill(comboBoxActiveIPAdress);
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            StartListeningRequest baseRequest = new StartListeningRequest()
            {
                IpAddress = comboBoxActiveIPAdress.SelectedValue.ToString(),
                Port = Convert.ToInt32(txtPort.Text),
            };

            if (_serverService.StartListening(baseRequest))
            {
                lblConnectionState.Text = DateTime.Now.ToString() + " Dinleme baslatildi..\n";
                Task.Run(() =>
                {
                    _serverService.StartReadAsync();
                });
                ServerManager.WriteOnScreenEvent += this.WriteOnScreenEvent;
            }
        }

        private void WriteOnScreenEvent(string message)
        {
            message = "Cilent: " + message;
            this.Invoke(new Action(() =>
            {
                txtGetMessage.AppendText(message + "\n");
            }));
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            bool result = await _serverService.SendMessageAsync(new SendMessageRequest { SendMessage = txtSendMessage.Text });
            if (result)
            {
                txtGetMessage.AppendText(txtSendMessage.Text + "\n");
                txtSendMessage.Text = "";
            }
            else
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            //Düzenleme yapılacak  16.07.2020 | Ali Yasin DOĞAN
            // _serverService.StopServer();
        }
    }
}