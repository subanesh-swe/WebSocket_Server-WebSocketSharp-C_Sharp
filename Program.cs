using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocket_Server_WebSocketSharp_C_Sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string serverUrl = "ws://192.168.116.149:7890";
            //const string serverUrl = "ws://192.168.0.102:7890";
            WebSocketServer wssv = new WebSocketServer(serverUrl);

            wssv.AddWebSocketService<Home>("/");
            //wssv.AddWebSocketService<Echo>("/Echo");
            //wssv.AddWebSocketService<EchoAll>("/EchoAll");

            wssv.Start();
            if (wssv.IsListening)
            {
                //Console.WriteLine("Listening on port {0}, and providing WebSocket services:", wssv.Port);
                Console.WriteLine("WS server started on {0}", serverUrl);
                Console.WriteLine("WebSocket services:");
                foreach (var path in wssv.WebSocketServices.Paths)
                    Console.WriteLine("- {0}{1}", serverUrl, path);
            }

            Console.WriteLine("\nPress Enter key to stop the server...");
            Console.ReadLine();
            // // Console.ReadKey();
            wssv.Stop();
        }
    }
}