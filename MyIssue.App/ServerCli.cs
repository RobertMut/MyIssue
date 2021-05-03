using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace MyIssue.App
{
    class ServerCli : Cli
    {
        private bool _disposed = false;
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        public void TcpClient()
        {
            try
            {
                using (Socket netwSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) { 
                netwSocket.Bind(localHost);
                netwSocket.ReceiveTimeout = 5000;
                IPEndPoint serv = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 49153);

                try
                {
                    netwSocket.Connect(serv);
                        using (NetworkStream networkStream = new NetworkStream(netwSocket))
                        {
                            TcpSendAndReceive(networkStream);
                            networkStream.Flush();
                        }
                    netwSocket.Disconnect(true);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());

                }

                }


            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());

            }
        }

        public static void TcpSendAndReceive(NetworkStream ns)
        {
            byte[] sendData;
            //byte[] response = new byte[2048];
            string messageHolder;
            try
            {
                messageHolder = "test \n<EndOfMessage>";
                sendData = new byte[Encoding.UTF8.GetByteCount(messageHolder)];
                sendData = Encoding.UTF8.GetBytes(messageHolder);
                ns.Write(sendData, 0, sendData.Length);
                Debug.WriteLine("Data sent!");
                /*ns.Read(response);
                string received = Encoding.UTF8.GetString(response, 0, response.Length);
                Debug.WriteLine(response.Length + "      " + received.Length);
                Debug.WriteLine(received);*/

                ns.Flush();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        protected override void Dispose(bool disposing)
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
            base.Dispose(disposing);
        }
    }
}
