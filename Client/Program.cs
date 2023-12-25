using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;

internal class Program
{
    private readonly UdpClient _udpClient;
    private bool _isRunning;
    private IPEndPoint _ipEndPoint;
    private bool isExeciute;
    
    public Program(string ipAddress)
    {
        ipAddress += ":7767";
        var endpoint = new IPEndPoint(IPAddress.Any, 7768);
        _ipEndPoint = IPEndPoint.Parse(ipAddress);
        _udpClient = new UdpClient(endpoint);
        _isRunning = true;
    }

    private async void GetQuery()
    {
        var query = "";
        if (!isExeciute)
        {
            
            Console.Write("SQL>");
            do
            {
                query += Console.ReadLine();
            } while (!query.Last().Equals(';') && !query.Contains("exit"));
        }

        if (query.Contains("exit()"))
            _isRunning = false;
        else
        {
            var result = await SendAndReceive(query);
            Console.WriteLine(result);
            isExeciute = false;
            
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
        

    }
    public async Task<string> SendAndReceive(string query) {

        var returnString="";
        isExeciute = true;
        await _udpClient.SendAsync(Encoding.UTF8.GetBytes(query), query.Length, _ipEndPoint);
        var receiveTask = await _udpClient.ReceiveAsync();
        var result = Encoding.UTF8.GetString(receiveTask.Buffer);
        if (!string.IsNullOrEmpty(result))
        {
            
            returnString+="Response form: "+receiveTask.RemoteEndPoint+"\n";
            
            var index = result.IndexOf("Result:", StringComparison.Ordinal);
            result = result.Substring(index);
            returnString+=result;
            
        }
        else
        {
            returnString+="Time out!! Cluster not responding!!";
            
        }
        
        return returnString;
        
    }
}