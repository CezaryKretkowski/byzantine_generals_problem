using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace byzantine_generals_problem
{
    public partial class ConnectionSettings : Form
    {
        private readonly string pattern = @"^((25[0-5]|2[0-4]\d|[0-1]?\d{1,2})\.){3}(25[0-5]|2[0-4]\d|[0-1]?\d{1,2})$";
        public ConnectionSettings()
        {
            InitializeComponent();
            this.IPAddress.Text = MainWindow.IpAddressString;
        }
        public ConnectionSettings(string ipAddress)
        {
            InitializeComponent();
            this.IPAddress.Text = ipAddress;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var address = IPAddress.Text.Trim();
            bool isIPv4Match = Regex.IsMatch(address, pattern);
            if (!isIPv4Match)
            {
                MessageBox.Show("Nie poprawny address!!");
            }
            else
            {
                MainWindow.IpAddressString = address;
                this.Close();
            }

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
