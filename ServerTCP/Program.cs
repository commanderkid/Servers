using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerTCP
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1"; //  ip адресс для соединения.
            const int port = 8080; // место для соединения

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            // набор параметров для протокола
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(5); // Очередь клиентов

            while(true)
            {
                var listener = tcpSocket.Accept(); // Cокет для подключения клиента
                var buffer = new byte[256]; // Хранилище данных
                var size = 0;
                var data = new StringBuilder();

                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (listener.Available > 0);

                Console.WriteLine(data);

                listener.Send(Encoding.UTF8.GetBytes("Успех"));
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }
    }
}
