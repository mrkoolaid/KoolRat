using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KoolServer
{
    class Program
    {
        public static string data = string.Empty;

        static void Main(string[] args)
        {
            listen();
        }

        static void listen()
        {
            byte[] bytes;
            
            IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
            IPAddress ip = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ip, 962);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            try
            {
                Console.WriteLine("Listening for connections...");

                listener.Bind(endPoint);
                listener.Listen(1);

                while (null == null)
                {
                    Socket sockHandler = listener.Accept();
                    data = string.Empty;

                    while (true)
                    {
                        bytes = new byte[1024];
                        int incomingBytes = sockHandler.Receive(bytes);
                        data = Encoding.ASCII.GetString(bytes);

                        if(data.IndexOf(Environment.NewLine) > -1)
                        {
                            char[] trims = { '\r', '\0', '\n' };
                            data = data.Trim(trims);
                            switch(data)
                            {
                                case "notepad":
                                    Process.Start("notepad");
                                    break;
                            }
                            break;
                        }
                    }

                    Console.WriteLine(data);
                }
            }
            catch (SocketException sex)
            {
                Console.WriteLine(sex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Write("Press any key to continue..");
            Console.ReadKey();
        }
    }
}
