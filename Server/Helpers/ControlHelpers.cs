using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Server.Helpers
{
    public static class ControlHelpers
    {
        /// <summary>
        /// Combobox Doldur
        /// </summary>
        public static void ComboBoxFill(ComboBox comboBox)
        {
            //Aktif IP adresleri bulunup combobox a ekleniyor.
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

            comboBox.DataSource = new BindingSource(comboFill, null);
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
        }
    }
}