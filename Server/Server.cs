using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        private const int logonPort = 4242;

        static Socket logonSocket;
        static IPAddress iPAddress = IPAddress.Parse(Packet.GetIP4Address());
        public static List<ClientData> clients;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting server on " + iPAddress);

            logonSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iPLogon = new IPEndPoint(iPAddress, logonPort);
            logonSocket.Bind(iPLogon);

            Thread logonThread = new Thread(LogonThread);
            logonThread.Start();
        }

        private static void LogonThread()
        {
            for (;;)
            {
                logonSocket.Listen(0);
                // http://stackoverflow.com/questions/14252182/how-to-use-tcp-client-listener-in-multithreaded-c
            }
        }
    }
}
