using System.Net;
using System.Net.Sockets;
using System.Text;
using server.src.common;

namespace server.src;

public class HeartBeatController
{
    private readonly UdpServer _client;
    

 

    public HeartBeatController(UdpServer client)
    {
        _client = client;
        
    }

    public static async void SendHeartBeat(UdpServer client)
    {
        var message = "server heartBeat request";
        var broadcast = new IPEndPoint(IPAddress.Broadcast,7766);
        await client.Listener.SendAsync(Encoding.UTF8.GetBytes(message),message.Length,broadcast);
    }

    public async void Receive(UdpReceiveResult receiveResult, string message)
    {
        StringBuilder responseMessage = new StringBuilder("server HeartBeat response");
        
        if (message.ToLower().Contains("request")&&!_client.IsLeader)
        {
            _client.IsLeader = false;
            _client.IsCandidate = false;
            _client.IsForwarded = true;
            _client.LeaderEndPoint = receiveResult.RemoteEndPoint;
            var endPoint = new IPEndPoint(receiveResult.RemoteEndPoint.Address,7767); 
            _client.LeaderEndPointClientListener = endPoint;
            var dataBaseEndpoint = new IPEndPoint(receiveResult.RemoteEndPoint.Address,7765);  
            
            var stringMessage = responseMessage.ToString();
            await _client.Listener.SendAsync(Encoding.UTF8.GetBytes(stringMessage), stringMessage.Length,
                receiveResult.RemoteEndPoint);
        }
        
    }
}