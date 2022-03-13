using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nefarius.ViGEm.Client.Targets;
using NetCoreServer;
using WebSocketListener_vtortola.GamePad;

//using GamepadHandler;

namespace WsChatServer
{
    class ChatSession : WsSession
    {

        //private SortedDictionary<Guid, IXbox360Controller> controllers = new SortedDictionary<Guid, IXbox360Controller>();

        public ChatSession(WsServer server) : base(server) { }


        public override void OnWsConnected(HttpRequest request)
        {

            GamepadHandeler gamepad = new GamepadHandeler(Id);

            Console.WriteLine($"Chat WebSocket session with Id {Id} connected!");

            // Send invite message
            string message = "Hello from WebSocket chat! Please send a message or '!' to disconnect the client!";
            SendTextAsync(message);

        }

        public override void OnWsDisconnected()
        {
            Console.WriteLine($"Chat WebSocket session with Id {Id} disconnected!");

            GamepadHandeler.Disconnect(Id);

        }

        public override void OnWsReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            Console.WriteLine("Incoming: " + message);

            GamepadHandeler.Action(Id, message);


            //Connection.testAs();

            // Multicast message to all connected sessions
            //((WsServer)Server).MulticastText(message);

            

            // If the buffer starts with '!' the disconnect the current session
            if (message == "!")
                Close(1000);
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat WebSocket session caught an error with code {error}");
        }
    }

    class ChatServer : WsServer
    {
        public ChatServer(IPAddress address, int port) : base(address, port) { }

        protected override TcpSession CreateSession() { return new ChatSession(this); }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chat WebSocket server caught an error with code {error}");
        }
    }

    class Connection
    {
       

        public static void testAs()
        {
            Console.WriteLine("TEST SUPER");
        }




        public static ChatServer ServerStart()
        {
            // WebSocket server port
            int port = 10500;
            // if (args.Length > 0)
            //port = int.Parse(args[0]);
            // WebSocket server content path
            string www = "../../../../../www/ws";
            //if (args.Length > 1)
            //www = args[1];

            Console.WriteLine($"WebSocket server port: {port}");
            Console.WriteLine($"WebSocket server static content path: {www}");
            Console.WriteLine($"WebSocket server website: http://localhost:{port}/chat/index.html");

            Console.WriteLine();

            // Create a new WebSocket server
            var server = new ChatServer(IPAddress.Any, port);
            //server.AddStaticContent(www, "/chat");

            // Start the server
            Console.Write("Server starting...");
            server.Start();
            Console.WriteLine("Done!");

            Console.WriteLine("Press Enter to stop the server or '!' to restart the server...");

            // Perform text input
            for (; ; )
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                // Restart the server
                if (line == "!")
                {
                    Console.Write("Server restarting...");
                    server.Restart();
                    Console.WriteLine("Done!");
                }

                // Multicast admin message to all sessions
                line = "(admin) " + line;
                server.MulticastText(line);
            }

            // Stop the server
            Console.Write("Server stopping...");
            server.Stop();
            Console.WriteLine("Done!");

            return server;
        }
    }
}