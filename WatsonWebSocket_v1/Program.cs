using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WatsonWebsocket;

namespace Test.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket.TestStart();
            while (true)
            {
                Console.WriteLine(".");
                Thread.Sleep(1000);
            }
        }
    }
}