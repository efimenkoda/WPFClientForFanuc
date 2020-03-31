using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            
            UdpClient client = new UdpClient("127.0.0.1", 8000);
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
