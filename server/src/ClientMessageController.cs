using System.Net;
using System.Net.Sockets;
using System.Text;
using Type = server.src.common.Type;

namespace server.src;

public class ClientMessageController
{
    private readonly UdpServer _client;

    public ClientMessageController(UdpServer client)
    {
        _client= client;
    }

    public void Receive(UdpReceiveResult receiveResult,string message)
    {
        if (_client.IsLeader)
        {
            var ipEndPoint = receiveResult.RemoteEndPoint;
            
            if (message.ToLower().Contains("client endpoint:"))
            {
                var index = message.IndexOf(':')+1;
                var ipAddressWithPort = message.Substring(index);
                ipEndPoint = IPEndPoint.Parse(ipAddressWithPort);
                index = message.IndexOf(" client endpoint:", StringComparison.Ordinal);
                message = message.Substring(0, index);
            }

            var queryResult = _client.DataBaseClient.ExecuteQuery(message);
            var messageResponse =  queryResult.ToString();
            
            _client.Listener.SendAsync(Encoding.UTF8.GetBytes(messageResponse), messageResponse.Length,ipEndPoint);
            if (queryResult.Type == Type.Success && !message.ToLower().Contains("select"))
            {
                var broadcastEndPoint = new IPEndPoint(IPAddress.Broadcast, 7765);
                _client.Listener.SendAsync(Encoding.UTF8.GetBytes(message), message.Length,broadcastEndPoint);
            }


        }
        else
        {
            message += " client endpoint:"+receiveResult.RemoteEndPoint;
            _client.Listener.SendAsync(Encoding.UTF8.GetBytes(message), message.Length,
                _client.LeaderEndPointClientListener);
        }
    }
}