using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientTCP
{
    class Program
    {
        private static IPAddress ip = IPAddress.Parse("127.0.0.1");
        private static int port = 8080;

        public static AddressFamily AddressFamili { get; private set; }

        static void Main(string[] args)
        {
            var tcpEndPoint = new IPEndPoint(ip, port);
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Введите сообщение:");
            var message = Console.ReadLine();
            var data = Encoding.UTF8.GetBytes(message);
            tcpSocket.Connect(tcpEndPoint);
            tcpSocket.Send(data);

            var buffer = new byte[256];
            var size = 0;
            var answer = new StringBuilder();

            do
            {
                size = tcpSocket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            }
            while (tcpSocket.Available > 0);

            Console.WriteLine(answer.ToString());
            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();
            Console.ReadLine();
        }
    }
}
