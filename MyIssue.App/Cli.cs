using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace MyIssue.App
{
    class Cli : IDisposable
    {   //get the shit done
        private bool _disposed = false;
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected static IPAddress bindAddress = IPAddress.Any;
        protected static int port = Int32.Parse(IO.decryptedData[2]) ;
        protected static IPEndPoint localHost = new IPEndPoint(bindAddress, port);
        //protected static IPEndPoint remoteServer;
        protected static string serverName = IO.decryptedData[1];
        protected static IPAddress ResolvedDns(string serverName)
        {
            IPAddress ip;
            //TODO: use foreach address
            bool validateIPAddress = IPAddress.TryParse(serverName, out ip);
            if (validateIPAddress)
            {
                ip = IPAddress.Parse(serverName);
                return ip;
            }
            else
            {
                ip = Dns.GetHostEntry(serverName).AddressList[0];

                return ip;
            }
        }
        public static string HtmlTemplate(string link)
        {
            string data = String.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(link);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader readStream = null;
                        if (String.IsNullOrWhiteSpace(response.CharacterSet)) readStream = new StreamReader(stream);
                        else readStream = new StreamReader(stream, Encoding.GetEncoding(response.CharacterSet));

                        data = readStream.ReadToEnd();

                        response.Close();
                        readStream.Close();
                    }



                }
            }
            return data;
        }
        public static byte[] byteMessage(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _safeHandle?.Dispose();
            }
            _disposed = true;
        }
    }
}
