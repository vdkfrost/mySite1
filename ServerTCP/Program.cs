using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Azure.WebJobs;

namespace ServerTCP
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        static List<KeyValuePair<string, Thread>> connections = new List<KeyValuePair<string, Thread>>();
        static string address = "52.178.190.191";
        //static string address = "192.168.1.6";
        static int port = 8005;
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var host = new JobHost();
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }

        static void CreateConnection(TcpClient client)
        {
            Thread acceptor = new Thread(new ThreadStart(delegate { CreateAcceptor(client); }));
            Thread connectionChecker = new Thread(new ThreadStart(delegate { CreateConnectionChecker(client); }));
            acceptor.Start();
            connectionChecker.Start();
        }

        static void CreateAcceptor(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            while (true)
                while (stream.DataAvailable)
                {
                    byte[] size = new byte[2];
                    stream.Read(size, 0, 2);
                    byte[] data = new byte[size[0] << 8 | size[1]];
                    stream.Read(data, 0, data.Length);
                    stream.Flush();
                    StringBuilder builder = new StringBuilder();
                    builder.Append(Encoding.UTF8.GetString(data));
                    Console.WriteLine(client.Client.RemoteEndPoint.ToString() + ": " + builder.ToString());

                    SendMessage("Packet received", stream);
                }
        }

        static void CreateConnectionChecker(TcpClient client)
        {
            while (true)
                try
                {
                    client.Client.Send(new byte[1]);
                    Thread.Sleep(10000);
                }
                catch
                {
                    Console.WriteLine(client.Client.RemoteEndPoint.ToString() + " disconnected");
                    DropConnection(client.Client.RemoteEndPoint.ToString());
                    break;
                }
        }

        static void SendMessage(string message, NetworkStream stream)
        {
            List<byte> answer = Encoding.UTF8.GetBytes(message).ToList();
            answer.Insert(0, 1);
            answer.Insert(1, (byte)((ushort)answer.Count >> 8 & 0xff));
            answer.Insert(2, (byte)((ushort)answer.Count & 0xff));
            stream.Write(answer.ToArray(), 0, answer.Count);
        }

        static void DropConnection(string ip)
        {
            for (int i = 0; i < connections.Count; i++)
                if (connections[i].Key == ip)
                {
                    connections[i].Value.Interrupt();
                    connections.RemoveAt(i);
                    break;
                }
        }
    }
}
