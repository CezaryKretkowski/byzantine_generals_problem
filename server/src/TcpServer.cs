using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server.src
{
    public class TcpServer
    {
        TcpListener listener;
        bool isRunning;
        Thread mainThread;
        List<Thread> workerThreads;
        public void Stop() {
            lock (new object()) { 
                isRunning = false;
            }
            foreach(Thread k in workerThreads) { 
                k.Join();
            }
            workerThreads.Clear();
            mainThread.Join();
        }
        public void Start() {
            mainThread.Start();
        }
        public TcpServer() {
            mainThread = new Thread(Run);
            workerThreads = new List<Thread>();
            var endPoint = new IPEndPoint(IPAddress.Any,7766);
            listener = new TcpListener(endPoint);
            isRunning = true;
        }
        private void ClientHandler([AllowNull] object handler) {
            if (handler == null)
                return;

            TcpClient client = (TcpClient)handler;
            Console.WriteLine("Client Connected assync in thread");
            client.Close();
        }
        private async void Run() {
            listener.Start();
            Console.WriteLine("Server listen...");
            while (isRunning) {
                TcpClient handler = await listener.AcceptTcpClientAsync();
                var thread = new Thread(new ParameterizedThreadStart(ClientHandler));
                workerThreads.Add(thread);
                thread.Start(handler);
            }
        }
    }
}
