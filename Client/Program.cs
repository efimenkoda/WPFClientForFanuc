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
            Client client = new Client("127.0.0.1", 8000);
            client.Send();
        }
    }
}
