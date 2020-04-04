using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class TelnetClient : ITelnetClient
    {
        private Socket client;
        public void connect(string ip, int port)
        {
            client = new Socket(AddressFamily.InterNetwork,
                  SocketType.Stream,
                  ProtocolType.Tcp);

            Console.WriteLine("Establishing Connection to {0}", ip);
            client.Connect(ip, port);
            Console.WriteLine("Connection established");
        }

        public void disconnect()
        {
            client.Disconnect(true);
        }

        public string read()
        {
            string data = null;
            byte[] bytes = null;

            //while (true)
            //{
                bytes = new byte[1024];
                int bytesRec = client.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

            // if (data.IndexOf("<EOF>") > -1)
            // {
            return data;
               // }
            //}
        }

        public void write(string command)
        {
            byte[] msg = Encoding.UTF8.GetBytes(command);
            int i = client.Send(msg);
        }
    }
}
