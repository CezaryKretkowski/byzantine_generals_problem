using server.src;

public class Program
{
    private static void Main(string[] args)
    {
       
        UdpServer server = new UdpServer("127.0.0.1",5);
        UdpServer udpServer = new UdpServer("127.0.0.2", 5);
        server.Start();
        Thread.Sleep(3000);
        udpServer.Start();
        Thread.Sleep(3000);
        server.Stop();
        udpServer.Stop();

    }
}