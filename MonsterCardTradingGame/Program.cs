using MTCG.repository;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MonsterCardTradingGame
{
    class Program
    {
        private static int Port = 10001;
        public static EndpointController endpointController = new EndpointController();
        static void Main(string[] args)
        {
            TcpListener tcpListener = null;
            try
            {
                // loopback -> localhost 
                tcpListener = new TcpListener(IPAddress.Loopback, Port);
                //can maximal 5 client accepted
                tcpListener.Start(5);
                while (true)
                {
                    Console.WriteLine("Server Start");
                    Database.GetInstance();
                    Console.WriteLine("Listening on port: " + Port);

                    var socket = tcpListener.AcceptTcpClient();
                    TcpHandler tcpHandler = new TcpHandler(socket);
                    Thread thread = new Thread(tcpHandler.process);
                    thread.Start();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
            }
            
        }
    }
}
