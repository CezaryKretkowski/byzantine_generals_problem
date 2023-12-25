using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Unicode;

internal class Program
{
    private readonly UdpClient _udpClient;
    private bool _isRunning;
    private IPEndPoint _ipEndPoint;
    
    public Program(string ipAddress)
    {
        ipAddress += ":7767";
        var endpoint = new IPEndPoint(IPAddress.Any, 7768);
        _ipEndPoint = IPEndPoint.Parse(ipAddress);
        _udpClient = new UdpClient(endpoint);
        _isRunning = true;
    }

    private void GetQuery()
    {
        var query = "";
        Console.Write("SQL>");
        do
        {
            query += Console.ReadLine();
        } while (!query.Last().Equals(';')&&!query.Contains("exit"));

        if (query.Contains("exit()"))
            _isRunning = false;
        else
        {
            SendAndReceive(query);
            
        }
    }

    public void MainLoop()
    {
        Console.CancelKeyPress += delegate(object? sender, ConsoleCancelEventArgs e)
        {
            _isRunning = false;
            e.Cancel = true;
            
        };

        while (_isRunning)
        {
            GetQuery();
        }
    }

    private static void Main(string[] args)
    {
        if(args.Length < 1)
            throw new Exception("Cannot init sql client ip address is null");

        if (string.IsNullOrEmpty(args[0]))
            throw new Exception("Cannot init sql client ip address is empty");
        
        var udpServer = new Program(args[0]);
        udpServer.MainLoop();
        //udpServer.SendAndReceive();

    }
    public async Task<bool> SendAndReceive(string query) {

        var receive = false;
        await _udpClient.SendAsync(Encoding.UTF8.GetBytes(query), query.Length, _ipEndPoint);
        var receiveTask = await _udpClient.ReceiveAsync();
        var result = Encoding.UTF8.GetString(receiveTask.Buffer);
        if (!string.IsNullOrEmpty(result))
        {
            Console.WriteLine();
            Console.WriteLine("Response form: "+receiveTask.RemoteEndPoint);
            
            var index = result.IndexOf("Result:", StringComparison.Ordinal);
            result = result.Substring(index);
            Console.WriteLine(result);
            receive = true;
        }
        else
        {
            Console.WriteLine("Time out!! Cluster not responding!!");
        }
        
        return receive;
        
    }
}