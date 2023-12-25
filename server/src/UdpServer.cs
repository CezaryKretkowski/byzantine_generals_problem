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
using System.Globalization;
using server.src.common;

namespace server.src
{
    public class UdpServer
    {


        public UdpClient Listener { get; init; }
        private UdpClient ClientListener { get; init; }
        private UdpClient DataBaseListener { get; init; }
        public List<EndPoint> Addresses { get; init; }
        private bool IsRunning { get; set; }
        public bool IsLeader { get; set; }
        
        public bool IsForwarded { get; set; }
        public bool IsCandidate { get; set; }
        private readonly Thread _mainThread;
        private readonly Thread _clientListenerThread;
        private readonly Thread _dataBaseListenerThread;
        public int HostNumber { get; init; }
        private readonly HeartBeatController _heartBeatController;
        private readonly VoteController _voteController;
        private readonly ClientMessageController _clientMessageController;
        public int Vote { get; set; }
        public string ServerName { get; init; }
        private IPEndPoint ServerEndPoint { get; init; }
        public IPEndPoint LeaderEndPoint { get; set; }
        public IPEndPoint LeaderEndPointClientListener { get; set; }
        public IPEndPoint LeaderEndPointDataBaseListener { get; set; }
        public DataBaseClient DataBaseClient { get; init; }

        //public string LeaderAddress { get; set; } = string.Empty;
        //public string LeaderAddress { get; set; } = string.Empty;



        public void Stop()
        {
            lock (new object())
            {
                IsRunning = false;
            }

            _mainThread.Join();
            _clientListenerThread.Join();
            _dataBaseListenerThread.Join();
        }
        public void Start()
        {
            _mainThread.Start();
            _clientListenerThread.Start();
            _dataBaseListenerThread.Start();
        }


        public UdpServer(string ipAddress,int hostNumber,string serverName,string connectionString)
        {
            _mainThread = new Thread(Run);
            _clientListenerThread = new Thread(RunClientListener);
            _dataBaseListenerThread = new Thread(RunDataBaseListener);
            var address = IPAddress.Parse(ipAddress);
            var ipEndPoint = new IPEndPoint(address, 7766);
            ServerEndPoint = ipEndPoint;
            ServerName = serverName;
            HostNumber = hostNumber;
            Listener = new UdpClient(ipEndPoint);
            var ipEndPoint2 = new IPEndPoint(address, 7767);
            ClientListener = new UdpClient(ipEndPoint2);
            var ipEndPoint3 = new IPEndPoint(address, 7765);
            DataBaseListener = new UdpClient(ipEndPoint3);
            IsRunning = true;
            IsLeader = false;
            IsForwarded = true;
            Addresses = new List<EndPoint>();
            _heartBeatController = new HeartBeatController(this);
            _voteController = new VoteController(this);
            _clientMessageController = new ClientMessageController(this);
            Vote = 0;
            DataBaseClient = new DataBaseClient(connectionString);

        }

        private void ClientMessageHandler(UdpReceiveResult receiveResult, string message)
        {
            _clientMessageController.Receive(receiveResult,message);
        }
        
        private void ServerMessageHandler(UdpReceiveResult receiveResult, string message)
        {
            if (message.ToLower().Contains("heartbeat"))
            {
                _heartBeatController.Receive(receiveResult,message);
            }

            if (message.ToLower().Contains("vote"))
            {
                _voteController.Receive(receiveResult,message);
            }

        }

        private void ReplicationMessageHandler(UdpReceiveResult receiveResult, string message)
        {
            var myEndpoint = new IPEndPoint(ServerEndPoint.Address,7765);
            if (!IsLeader && !myEndpoint.Equals(receiveResult.RemoteEndPoint))
            {
                DataBaseClient.ExecuteQuery(message);
            }
        }

        private  void MessageHandler(UdpReceiveResult receiveResult, string message)
        {

            if (message.ToLower().Contains("server"))
            {
                ServerMessageHandler(receiveResult, message);
            }
            
        }

        private void WhenReceive(Task<UdpReceiveResult> receiveTask)
        {
            byte[] reciveMessage = receiveTask.Result.Buffer;
            var message = Encoding.UTF8.GetString(reciveMessage);
            if (!ServerEndPoint.Equals(receiveTask.Result.RemoteEndPoint))
            {

                Console.WriteLine( $"{ServerName} Otrzymano wiadomość od {receiveTask.Result.RemoteEndPoint}: {message} time {DateTime.Now.ToString("h:mm:ss.fff tt")}");
                MessageHandler(receiveTask.Result, message);
            }
        }

        private  void WhenTimeOut()
        {
            Console.WriteLine($"{ServerName} Timeout time {DateTime.Now.ToString("h:mm:ss.fff tt")}");

            if (IsLeader != true)
            {
                if(IsCandidate == false)
                    Vote = 1;
                IsCandidate = true;
                Console.WriteLine($"{ServerName} is Candidate time {DateTime.Now.ToString("h:mm:ss.fff tt")}");
                
                _voteController.SendVote();
            }
            
        }
        
        private async void Run()
        {
            Console.WriteLine("Server listen...");//
            while (IsRunning)
            {
                var receiveTask = Listener.ReceiveAsync();
                var timeout= Task.Delay(1000);
                var completed = await Task.WhenAny(receiveTask,timeout);
                if (completed == receiveTask)
                {

                   WhenReceive(receiveTask);
                   Thread.Sleep(250);
                }
                else {
                    WhenTimeOut();
                }
                if(IsLeader)
                    HeartBeatController.SendHeartBeat(this);
            }
        }

        private async void RunClientListener()
        {
            Console.WriteLine("Server client listen...");
            while (IsRunning)
            {
                var receiveTask = await ClientListener.ReceiveAsync();
                byte[] reciveMessage = receiveTask.Buffer;
                var message = Encoding.UTF8.GetString(reciveMessage);
                Console.WriteLine( $"{ServerName} Otrzymano wiadomość od {receiveTask.RemoteEndPoint}: {message} time {DateTime.Now.ToString("h:mm:ss.fff tt")}");
                if(!string.IsNullOrEmpty(message))
                    ClientMessageHandler(receiveTask,message);
     
            }
        }
        private async void RunDataBaseListener()
        {
            Console.WriteLine("Server client listen...");
            while (IsRunning)
            {
                var receiveTask = await DataBaseListener.ReceiveAsync();
                byte[] reciveMessage = receiveTask.Buffer;
                var message = Encoding.UTF8.GetString(reciveMessage);
                Console.WriteLine( $"{ServerName} Otrzymano wiadomość od {receiveTask.RemoteEndPoint}: {message} time {DateTime.Now.ToString("h:mm:ss.fff tt")}");
                if(!string.IsNullOrEmpty(message))
                    ReplicationMessageHandler(receiveTask,message);
     
            }
        }
    }
}
