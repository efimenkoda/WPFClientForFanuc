using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDP
{
    public class Server
    {
        UdpClient recieve;
        IPEndPoint iPEndPoint = null;
        public Server(int port)
        {
            recieve = new UdpClient(port);
        }

        public void Recieve()
        {
            try
            {
                while (true)
                {
                    byte[] data = recieve.Receive(ref iPEndPoint);
                    string message = Encoding.UTF8.GetString(data);
                    Console.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message );
            }
            finally
            {
                recieve.Close();
            }
        }
    }
}
