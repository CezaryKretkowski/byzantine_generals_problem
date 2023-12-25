using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace byzantine_generals_problem
{
    public partial class MainWindow : Form
    {
        public static string IpAddressString { get; set; } = "127.0.0.1";
        private UdpClient udpClient;

        public MainWindow()
        {
            InitializeComponent();
            udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 7788));
        }

        private void Connection_Click(object sender, EventArgs e)
        {
            using (var connectionDialog = new ConnectionSettings())
            {
                connectionDialog.ShowDialog();
            }
        }
        private DataTable ToDataTeble(string result)
        {
            DataTable dt = new DataTable("Result");
            var index = result.IndexOf('[');
            result = result.Substring(index);
            var array = JArray.Parse(result).Children<JObject>();
            foreach (var item in array.First())
            {
                dt.Columns.Add(item.Key);
            }

            foreach (var item in array) { 
                var row = dt.NewRow();
                foreach (var pair in item) {
                    row[pair.Key] = pair.Value;
                }
                dt.Rows.Add(row);
            }
            return dt;

        }
        private async void Execute_Click(object sender, EventArgs e)
        {
            var input = SqlInput.Text.Trim();
            if (string.IsNullOrEmpty(input))
                return;

            var output = await SendQuery(input);
            ServerOuts.Text = output;
            if (input.ToLower().Contains("select")) {
                dataGridView1.DataSource = ToDataTeble(output);
            }
        }

        private async Task<String> SendQuery(string query)
        {
            var returnString = "";
            var endPointAddres = IpAddressString + ":7767";
            var endPoint = IPEndPoint.Parse(endPointAddres);
            await udpClient.SendAsync(Encoding.UTF8.GetBytes(query), query.Length, endPoint);
            var receiveResultat = await udpClient.ReceiveAsync();
            var result = Encoding.UTF8.GetString(receiveResultat.Buffer);
            if (!string.IsNullOrEmpty(result))
            {
                returnString += "Response form: " + receiveResultat.RemoteEndPoint + "\n";
                returnString += result;
            }
            else
            {
                returnString += "Time out!! Cluster not responding!!";
            }
            return returnString;
        }
    }
}