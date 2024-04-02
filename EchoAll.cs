using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using WebSocketSharp;

namespace C_Sharp_WebSocket_Server
{
    public class EchoAll : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Received message from EchoAll client: " + e.Data);
            Sessions.Broadcast(e.Data);
        }
    }
}
