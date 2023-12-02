using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Unicode;

internal class Program
{
    public UdpClient udpClient;
    Program() {
        udpClient = new UdpClient();
    }
    private static void Main(string[] args)
    {
        Program udpServer = new Program();
        udpServer.SendAndRecive();
        

    }
    public async void SendAndRecive() {
        string message = "Hello, World!";
        IPAddress serverAdress = IPAddress.Parse("127.0.0.1");
        await udpClient.SendAsync(Encoding.UTF8.GetBytes(message), message.Length, new IPEndPoint(serverAdress, 7766));
        var result =  await udpClient.ReceiveAsync();
        Console.WriteLine(Encoding.UTF8.GetString(result.Buffer));
    }
}