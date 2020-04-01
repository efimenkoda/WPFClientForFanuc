using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client
    {
        private UdpClient client;
        public Client(string host, int port)
        {
            client = new UdpClient(host, port);
        }
        public void Send()
        {
            try
            {
                while (true)
                {
                    string message = Console.ReadLine();
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    client.Send(data, data.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                client.Close();
            }
        }


    }
}
