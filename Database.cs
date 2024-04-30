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

        public static void Add(string key, string value)
        {
            clientDict.Add(key, value);
        }

        public static void Remove(string key, string value)
        {
            clientDict.Remove(key);
        }

        public static string Get(string key)
        {
            if (isAvailable(key) == true)
                return clientDict[key];
            return null;

        }

        public static bool isAvailable(string key)
        {
            return clientDict.ContainsKey(key);
        }
    }
}
