using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebSocketSharp.Server;
using WebSocketSharp;

namespace WebSocket_Server_WebSocketSharp_C_Sharp
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Received message from Echo client: " + e.Data + "rawData: ");
            Send(e.Data);
        }
    }
}
