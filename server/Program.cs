using server.src;

public class Program
{
    public record Config(int hostNumber,string connectionString,string serverName,string ipAddress);

    private Config _config;
    public UdpServer Instance { get; init; }

    public void Configuration()
    {
        try
        {
            using (var reader = new StreamReader("config.json"))
            {
                string json = reader.ReadToEnd();
                var config = System.Text.Json.JsonSerializer.Deserialize<Config>(json);
                if (config != null)
                    _config = config;
                else
                {
                    throw new Exception("Cannot init server: failed to initialize configuration");
                }
            }
                
        }catch (Exception ex)
        {
            throw new Exception("Cannot init server: "+ex.Message);
        }
    }

    public Program()
    {
        Configuration();
        
        if (_config != null)
        {
            var serverName = _config.serverName;
            if(string.IsNullOrEmpty(_config.ipAddress))
                throw new Exception("Cannot init server: Ip Address is empty");
            if(string.IsNullOrEmpty(_config.connectionString))
                throw new Exception("Cannot init server: connection string is empty");
            if (string.IsNullOrEmpty(_config.serverName))
                serverName = "server instance";
            Instance = new UdpServer(_config.ipAddress, _config.hostNumber, serverName,_config.connectionString);
        }
        else
        {
            throw new Exception("Cannot init server: _configuration is null");
        }
    }

    public void Start()
    {
        Instance.Start();
    }

    public void Stop()
    {
        Instance.Stop();
    }

    private static void Main(string[] args)
    {
        bool isr = true;
        //var server = new Program();
        #region Testing
        string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Instance1;User ID=user;Password=admin;TrustServerCertificate=True;";
        UdpServer server1 = new UdpServer("127.0.0.1",5,"Server 1",connectionString);
        connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Instance2;User ID=user;Password=admin;TrustServerCertificate=True;";
        UdpServer server2 = new UdpServer("127.0.0.2", 5,"Server 2",connectionString);
        connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Instance3;User ID=user;Password=admin;TrustServerCertificate=True;";
        UdpServer server3 = new UdpServer("127.0.0.3", 5,"Server 3",connectionString);
        server1.Start();
        server3.Start();
        server2.Start();
        #endregion
        
        Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e)
        {
            isr = false;
            //server.Stop();
            #region Testing
            server1.Stop();
            server2.Stop();
            server3.Stop();
            #endregion
            e.Cancel = true;
        };
        while (isr){}
        
    }
}