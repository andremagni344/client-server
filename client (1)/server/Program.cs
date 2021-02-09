using System;
using System.IO;
using System.Net.Sockets;
using static System.Console;
using System.Threading;
using System.Text;
using System.Net;

namespace server
{
    class Program
    {
       
        static TcpListener server = new TcpListener( 23);
        static NetworkStream client = null;
        static void Main(string[] args)
        {

            
            Console.WriteLine("listener...");
            server.Start();
            TcpClient newConn = server.AcceptTcpClient();
            while (true)
            {
                client = newConn.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ricevuto = new byte[100];
                int k = client.Read(ricevuto, 0, 100);
                Console.Write("client: ");
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(ricevuto[i]));
                Console.WriteLine();

                Console.Write("Enter the string to be transmitted at client: ");
                string str = Console.ReadLine();
                if (str == "esc") break;
                byte[] sender = asen.GetBytes(str);
                Console.WriteLine("Transmitting.....");
                client.Write(sender, 0, sender.Length);

               
            }
            client.Close();
        }
    }
}
