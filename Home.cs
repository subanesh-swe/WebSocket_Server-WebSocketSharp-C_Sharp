﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using WebSocketSharp.Server;
using WebSocketSharp;

namespace WebSocket_Server_WebSocketSharp_C_Sharp
{
    public class Home : WebSocketBehavior
    {
        private string _name;
        private string _id;
        private static int _number = 0;
        private string _prefix;

        public Home()
        {
            _prefix = "anon#";
        }

        public string Prefix
        {
            get
            {
                return _prefix;
            }

            set
            {
                _prefix = !value.IsNullOrEmpty() ? value : "anon#";
            }
        }

        protected NameValueCollection QueryString
        {
            get
            {
                if (Context == null)
                {
                    var msg = "The session has not started yet.";

                    throw new InvalidOperationException(msg);
                }

                return Context.QueryString;
            }
        }

        private string getName()
        {
            var name = QueryString["name"];

            return !name.IsNullOrEmpty() ? name : _prefix + getNumber();
        }

        private static int getNumber()
        {
            return Interlocked.Increment(ref _number);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            // to get string data
            // var strMsg = e.Data;
            // to get byte data
            var rawMsg = e.RawData;
            // do something with this data (from client)

            var fmt = "Server[Internal Reply]: {0}: {1}";
            string strMsg = "";
            for (int x = 0; x < rawMsg.Length; x++)
            {
                //Console.WriteLine(rayMsg[x]);
                strMsg += ((int) rawMsg[x]).ToString() + " ";
            }

            var msg = String.Format(fmt, _name, strMsg);

            Console.WriteLine("Home:\n\tReceived message:\n\tClient: " + _name + "\n\tMessage: " + strMsg + "\n\tReply: " + msg);
        }

        protected override void OnOpen()
        {
            var fmt = "ID: {0} has logged in!, name: {1}";
            var msg = String.Format(fmt, _id, _name);

            Console.WriteLine("Home:\n\t" + msg);

            var reply = "Server[Internal Reply]:" + msg;
            Sessions.Broadcast(reply);
        }
        
        protected override void OnClose(CloseEventArgs e)
        {
            if (_name == null)
                return;

            var fmt = "ID: {0} has logged out!, name: {1}";
            var msg = String.Format(fmt, _id, _name);

            Console.WriteLine("Home:\n\t" + msg);

            var reply = "Server[Internal Reply]:" + msg;
            Sessions.Broadcast(reply);
        }
    }
}
