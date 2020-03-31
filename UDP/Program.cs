using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDP
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient recieve = new UdpClient(8000);
            IPEndPoint iPEndPoint = null;

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
                Console.WriteLine(e.Message);
            }
            finally
            {
                recieve.Close();
            }
        }
    }
}
