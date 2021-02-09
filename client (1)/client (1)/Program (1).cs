using System;
using System.IO;
using System.Net.Sockets;
using static System.Console;
using System.Threading;
using System.Text;

namespace client
{
    class Program
    {
        static TcpClient client = new TcpClient();
        static NetworkStream server = null;
        static void Main(string[] args)
        {

            client.Connect("127.0.0.1", 23);
            Console.WriteLine("Connected");
            while (true)
            {
                Console.Write("Enter the string to be transmitted : ");

                string str = Console.ReadLine();
                if (str == "esc") break;
                server = client.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] sender = asen.GetBytes(str);
                Console.WriteLine("Transmitting.....");

                server.Write(sender, 0, sender.Length);

                byte[] ricevuto = new byte[100];
                int k = server.Read(ricevuto, 0, 100);

                Console.Write("server: ");
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(ricevuto[i]));
                Console.WriteLine();
            }

            client.Close();
            
        }
    }
}
