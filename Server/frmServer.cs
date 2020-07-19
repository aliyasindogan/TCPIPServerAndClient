using DevExpress.XtraEditors;
using Entities.Concrete.Request;
using Entities.Constans;
using Server.Helpers;
using Services.Abstract;
using Services.Concrete;
using System;
using System.Drawing;
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

        #region Operations

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
                MessageBox.Show(Messages.AnErrorOccurred);
            }
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                IpAddress = comboBoxActiveIPAdress.SelectedValue.ToString(),
                Port = Convert.ToInt32(txtPort.Text),
            };

            if (_serverService.StartListening(baseRequest))
            {
                lblConnectionState.Text = DateTime.Now.ToString() + " " + Messages.ListeningStarted;
                btnStartServer.BackColor = Color.GreenYellow;

                Task.Run(() =>
                {
                    _serverService.StartReadAsync();
                });
                ServerManager.WriteOnScreenEvent += this.WriteOnScreenEvent;
                ServerManager.FirstConnectionEvent += this.FirstConnectionEvent;
                comboBoxActiveIPAdress.Enabled = false;
                txtPort.Enabled = false;
            }
        }

        private void FirstConnectionEvent(string message)
        {
            this.Invoke(new Action(() =>
            {
                if (listBoxConnected.Items.Count == 0)
                {
                    listBoxConnected.Items.Add(message);
                }
            }));
        }

        private void WriteOnScreenEvent(string message)
        {
            this.Invoke(new Action(() =>
            {
                if (!message.Contains("Exception"))
                {
                    txtGetMessage.AppendText(message + "\n");
                }
            }));
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            bool state = _serverService.StopServer();
            if (state)
            {
                lblConnectionState.Text = Messages.ServerStopped;
                btnStopServer.BackColor = Color.Red;
            }
            else
            {
                btnStopServer.BackColor = Color.GreenYellow;
                lblConnectionState.Text = Messages.ServerCouldNotBeStopped;
            }
        }

        #endregion Operations
    }
}