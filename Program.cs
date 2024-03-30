using C_Sharp_WebSocket_Server;
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

            WebSocketServiceHost host;
            var getHostSuccess = wssv.WebSocketServices.TryGetServiceHost("/", out host);
            if (getHostSuccess != true)
            {
                Console.WriteLine("Error getting Host...");
            }

            // // to send msg to all clients
            //host.Sessions.Broadcast(msg);

            // //to send msg to particulat client, id is needed
            //host.Sessions.SendTo(string data, string id);
            //host.Sessions.SendTo(msg, Database.Get(name));

            // // to send msg to one client
            //var msg = "hello there";
            //string name = "SUBANESHs-ESP8266-STA-2";
            //if (Database.isAvailable(name))
            //{
            //    host.Sessions.SendTo(msg, Database.Get(name));
            //    Console.WriteLine(name + " -> Msg:["+ msg +"] Sended...");
            //} 
            //else
            //{
            //    Console.WriteLine(name + " -> Not Connected...");
            //}

            // // to send msg to all clients in database

            //var msg = "hello there";
            byte[] rawMsg = { (int)'L', (int)'A', (int)'K', (int)2, (int)1, (int)'D', (int)8, (int)217 }; // 76 65 75 2 1 68 8 217
            string strMsg = "";
            for (int x = 0; x < rawMsg.Length; x++)
            {
                //Console.WriteLine(rayMsg[x]);
                strMsg += ((int)rawMsg[x]).ToString() + " ";
            }
            while (getHostSuccess)
            {
                try
                {
                    Dictionary<string, string> DatabaseCopy = new Dictionary<string, string>(Database.clientDict);
                    foreach (KeyValuePair<string, string> entry in DatabaseCopy)
                    {
                        // // do something with entry.Value or entry.Key
                        var name = entry.Key;
                        var id = entry.Value;
                        if (Database.isAvailable(name))// && host.Sessions.PingTo(id))
                        {
                            host.Sessions.SendTo(rawMsg, Database.Get(name));
                            Console.WriteLine(name + " -> Msg:[" + strMsg + "] Sended...");
                        }
                        else
                        {
                            Console.WriteLine(name + " -> Not Connected...");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Main Program: \n\tCatch Exception:" + ex.ToString());
                }
                System.Threading.Thread.Sleep(2000);
            }

            Console.WriteLine("\nPress Enter key to stop the server...");
            Console.ReadLine();
            // // Console.ReadKey();
            wssv.Stop();
        }
    }
}