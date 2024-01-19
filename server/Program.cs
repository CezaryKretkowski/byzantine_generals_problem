using server.src;

public class Program
{
    public record Config(int hostNumber,string connectionString,string serverName,string ipAddress);

    private Config _config;
    public UdpServer Instance { get; init; }

    public void Configuration(string path)
    {
        try
        {
            using (var reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                var config = System.Text.Json.JsonSerializer.Deserialize<Config>(json);
                if (config != null)
                    _config = config;
                else
                {
                    throw new Exception("Cannot init server: failed to initialize configuration");
                }
                Console.WriteLine(config);
            }
                
        }catch (Exception ex)
        {
            throw new Exception("Cannot init server: "+ex.Message);
        }
    }

    public Program(string path)
    {
        Configuration(path);
        
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
        var server = new Program(args[0]);
        /*#region Testing
        string connectionString = "Data Source=Instance1.db;";
        UdpServer server1 = new UdpServer("127.0.0.1",5,"Server 1",connectionString);
        connectionString = "Data Source=Instance2.db;";
        UdpServer server2 = new UdpServer("127.0.0.2", 5,"Server 2",connectionString);
        connectionString = "Data Source=Instance3.db;";
        UdpServer server3 = new UdpServer("127.0.0.3", 5,"Server 3",connectionString);
        server1.Start();
        server3.Start();
        server2.Start();
        #endregion
        */
        Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e)
        {
            isr = false;
            server.Stop();
            #region Testing
          //  server1.Stop();
           // server2.Stop();
            //server3.Stop();
            #endregion
            e.Cancel = true;
        };
        server.Start();
        while (isr){}
        
    }
}