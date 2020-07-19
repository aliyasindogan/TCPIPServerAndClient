using Client.Models;
using Entities.Concrete.Request;
using Entities.Constans;
using Services.Abstract;
using Services.Concrete;
using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frmClient : Form
    {
        #region Services

        private IClientService _clientService = new ClientManager();

        #endregion Services

        #region frmClient

        public frmClient()
        {
            InitializeComponent();
        }

        private void frmClient_Load(object sender, EventArgs e)
        {
            this.Text = "Client: " + LoginViewModel.UserName;
        }

        #endregion frmClient

        private void btnConnection_Click(object sender, EventArgs e)
        {
            try
            {
                BaseRequest baseRequest = new BaseRequest()
                {
                    IpAddress = maskedTextBoxIPAddress.Text,
                    Port = Convert.ToInt32(txtPort.Text),
                };
                Task.Run(() =>
                {
                    _clientService.ConnectionAndStartReadyAsync(baseRequest);
                    this.Invoke(new Action(() =>
                    {
                        lblConnectionState.Text = DateTime.Now.ToString() + " " + Messages.ConnectionEstablished;
                        btnConnection.BackColor = Color.GreenYellow;
                        btnDisconnect.BackColor = default(Color);/* SystemColors.Control;*/
                        txtPort.Enabled = false;
                        maskedTextBoxIPAddress.Enabled = false;
                        ConnnectionControl();
                    }));
                });

                ClientManager.WriteOnScreenEvent += ClientManager_WriteOnScreenEvent;
            }
            catch (Exception)
            {
                lblConnectionState.Text = Messages.ErrorWhileConnectingToServer;
            }
        }

        private void ClientManager_WriteOnScreenEvent(string message)
        {
            message = "Server: " + message;
            this.Invoke(new Action(() =>
            {
                if (!message.Contains("Exception"))
                {
                    txtGetMessage.AppendText(message + "\n");
                }
            }));
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            bool result = await _clientService.SendMessageAsync(new ClientSendMessageRequest
            {
                UserName = LoginViewModel.UserName,
                SendMessage = txtSendMessage.Text
            });

            if (result)
            {
                txtGetMessage.AppendText(txtSendMessage.Text + "\n");
                txtSendMessage.Text = "";
            }
            else
            {
                MessageBox.Show(Messages.AnErrorOccurred);
            }
        }

        private async void ConnnectionControl()
        {
            bool result = await _clientService.SendMessageAsync(new ClientSendMessageRequest
            {
                UserName = LoginViewModel.UserName,
                SendMessage = Messages.FirstConnection
            });
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                _clientService.Disconnect();
                this.Invoke(new Action(() =>
                {
                    btnDisconnect.BackColor = Color.Red;
                    btnConnection.BackColor = default(Color);

                    lblConnectionState.Text = Messages.Disconnection;
                }));
            });
        }
    }
}