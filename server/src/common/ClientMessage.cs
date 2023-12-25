using System.Net;

namespace server.src.common;

public class ClientMessage
{
    public string Message { get; set; } =  String.Empty;
    public IPEndPoint? ClientEndPoint { get; set; } 
}