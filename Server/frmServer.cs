using Entities.Concrete.Request;
using Server.Helpers;
using Services.Abstract;
using Services.Concrete;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class frmServer : Form
    {
        #region Services

        private readonly IServerService _serverService = new ServerManager();

        #endregion Services

        #region frmServer

        public frmServer()
        {
            InitializeComponent();
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            ControlHelpers.ComboBoxFill(comboBoxActiveIPAdress);
        }

        #endregion frmServer

        #region Buttons

        private async void btnSend_Click(object sender, EventArgs e)
        {
            bool result = await _serverService.SendMessageAsync(new ServerSendMessageRequest { SendMessage = txtSendMessage.Text });
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

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            ServerStartListeningRequest baseRequest = new ServerStartListeningRequest()
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

        #endregion Buttons
    }
}