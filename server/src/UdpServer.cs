using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;

namespace server.src
{
    public class UdpServer
    {
        UdpClient listener;
        List<EndPoint> addresses;
        bool isRunning;
        bool isLeader;
        bool isForwarded;
        bool isCandidate;
        Thread mainThread;
        IPEndPoint liderServer;
        bool heartbeat;
        int hostNumber;
        int votes;


        public void Stop()
        {
            lock (new object())
            {
                isRunning = false;
            }

            mainThread.Join();
        }
        public void Start()
        {
            mainThread.Start();
        }
        private async Task<string> GetFreeAddres() {
            int i = 0;

            while (i < 10) {
                i++;
                string ipAddres = "127.0.0." + i;
                using (Ping ping = new Ping())
                {
                    try
                    {

                        PingReply reply = await ping.SendPingAsync(ipAddres);
                        if (reply.Status != IPStatus.Success) {
                            return ipAddres;
                        }
                    }
                    catch (PingException ex)
                    {
                        Console.WriteLine($"Błąd podczas sprawdzania adresu {ipAddres}: {ex.Message}");
                    }
                }
            }
            return String.Empty;
        }

        public UdpServer(string ipAddress,int hostNumber)
        {
            addresses = new List<EndPoint>();
            mainThread = new Thread(Run);
            IPAddress address = IPAddress.Parse(ipAddress);
            var endPointClient = new IPEndPoint(address, 7766);
            listener = new UdpClient(endPointClient);
            isRunning = true;
            isLeader = false;

        }

        async void MessageHandler(UdpReceiveResult receiveResult, string message) {
            if (!message.Contains("Server"))
            {
                if (!isLeader)
                {
                    await listener.SendAsync(Encoding.UTF8.GetBytes(message), message.Length, liderServer);
                }
            }
            else {
                if (!addresses.Contains(receiveResult.RemoteEndPoint))
                    addresses.Add(receiveResult.RemoteEndPoint);
                if (receiveResult.RemoteEndPoint.Equals(liderServer)) {
                    heartbeat = true;
                    string hearbeatMessage = "Server recive heartbeat";
                    await listener.SendAsync(Encoding.UTF8.GetBytes(hearbeatMessage), hearbeatMessage.Length, liderServer);
                }
                if (message.Contains("Vote")&&isCandidate) {
                    votes++;
                    if (votes > hostNumber / 2)
                    {
                        isCandidate = false;
                        isLeader = true;
                        await listener.SendAsync(Encoding.UTF8.GetBytes("Server im a leadre"), "Server im a leadre".Length, new IPEndPoint(IPAddress.Broadcast, 7766));
                    }
                }
            }
        }
        
        private async void Run()
        {
   
            Console.WriteLine("Server listen...");
            while (isRunning)
            {

                Task<UdpReceiveResult> receiveTask = listener.ReceiveAsync();
                Task timeout= Task.Delay(2000);
                Task completed = await Task.WhenAny(receiveTask,timeout);
                if (completed == receiveTask)
                {
                    byte[] reciveMessage = receiveTask.Result.Buffer;
                    var message = Encoding.UTF8.GetString(reciveMessage);
                    Console.WriteLine($"Otrzymano wiadomość od {receiveTask.Result.RemoteEndPoint}: {message}");
                    MessageHandler(receiveTask.Result,message);
                }
                else {
                    Console.WriteLine("Timeout");
                    heartbeat = false;
                    if (isLeader != true) {
                        isCandidate = true;
                        var ms = "server is candidate";
                        IPEndPoint brodcast = new  IPEndPoint(IPAddress.Broadcast,7766);
                        await listener.SendAsync(Encoding.UTF8.GetBytes(ms),ms.Length,brodcast);
                    }
                }
            }
        }
    }
}
