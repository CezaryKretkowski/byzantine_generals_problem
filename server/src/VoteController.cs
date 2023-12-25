using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server.src;

public class VoteController
{
    private readonly UdpServer _client;

    public VoteController(UdpServer client)
    {
        _client = client;
    }

    public async void SendVote()
    {
        var message = "server vote request";
        var broadcast = new IPEndPoint(IPAddress.Broadcast,7766);
        await _client.Listener.SendAsync(Encoding.UTF8.GetBytes(message),message.Length,broadcast);
    }

    public async void Receive(UdpReceiveResult receiveResult, string message)
    {
        if (message.ToLower().Contains("request"))
        {
            _client.IsCandidate = false;
            _client.Vote = 1;
            _client.IsForwarded = true;
            var responseMessage = "server vote response";
            await _client.Listener.SendAsync(Encoding.UTF8.GetBytes(responseMessage),responseMessage.Length,receiveResult.RemoteEndPoint);
        }
        if(message.ToLower().Contains("response"))
        {
            if (_client.IsCandidate)
            {
                _client.Vote++;
                var requireVotes = (float)_client.HostNumber / 2;
                
                if (_client.Vote > requireVotes)
                {
                    _client.IsLeader = true;
                    _client.IsCandidate = false;
                    _client.IsForwarded = false;
                    Console.WriteLine(_client.ServerName + " Is Leader vote result "+ _client.Vote +"/"+_client.HostNumber+" require votes:"+requireVotes);
                    HeartBeatController.SendHeartBeat(_client);

                }
            }
        }
    }

        
    
}