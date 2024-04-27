using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_WebSocket_Server
{
    public class Database
    {
        public static Dictionary<string, string> clientDict = new Dictionary<string, string>();
        // key: name of client, 
        // value: session id of client
    }
}
