using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.IO;

namespace MyIssue.App
{
    class SMTPCli : Cli
    {
        private bool _disposed = false;
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        public void TelnetClient()
            {
            IPEndPoint remoteServer = new IPEndPoint(Cli.ResolvedDns(serverName), port);
            using (Socket netwSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    netwSocket.Bind(localHost);
                    netwSocket.ReceiveTimeout = 5000;
                        try
                        {

                            netwSocket.Connect(remoteServer);
                            using (NetworkStream networkStream = new NetworkStream(netwSocket))
                            {
                                SendAndReceive(networkStream);
                                networkStream.Close();
                            }
                            netwSocket.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Client connection exception -> " + e.Message);
                            if (netwSocket != null)
                            {
                                netwSocket.Close();
                            }
                        }
                    
                }

                catch (SocketException se)
                {
                    if (netwSocket != null)
                    {
                        netwSocket.Shutdown(SocketShutdown.Both);
                    }
                    Console.WriteLine("Client bind exception -> " + se.Message);
                }
            }
            }
            public void SSLClient()
            {
            IPEndPoint remoteServer = new IPEndPoint(Cli.ResolvedDns(serverName), port);
            using (Socket netwSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    netwSocket.Bind(localHost);
                    netwSocket.ReceiveTimeout = 5000;
                    try
                    {
                        netwSocket.Connect(remoteServer);
                        using (NetworkStream networkStream = new NetworkStream(netwSocket))
                        using (SslStream sslStream = new SslStream(networkStream, false, new RemoteCertificateValidationCallback(CertificateValidation)))
                        {
                            SMTPCli.SendAndReceive(sslStream);
                            sslStream.Close();
                            networkStream.Close();
                        }
                        netwSocket.Close();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Client connection exception -> {0}", e.Message);
                    }
                }
                catch (SocketException se)
                {
                    Debug.WriteLine("Client bind exception -> " + se.Message);
                }
            }
        }
            
            
            public static void SendAndReceive(NetworkStream ns)
            {
                byte[] tcpSendData;
                byte[] receiveData = new byte[2048];
                string response;
                string[] commands;
                using (MessageConstructor messageConstructor = new MessageConstructor())
                {

                    if (!IO.encryptedData[11].Equals("Empty"))
                    {
                        commands = messageConstructor.HTMLMessageBuilder(MainWindow.input);
                    } else
                    {
                        commands = messageConstructor.MessageBuilder(MainWindow.input);
                    }

                }
                
                try
                {
                    ns.Read(receiveData, 0, receiveData.Length);
                    response = Encoding.UTF8.GetString(receiveData, 0, receiveData.Length);
                    Debug.WriteLine("Received {0}!\n" +
                    "Response message -> {1}", receiveData.Length, response);

                    for (int i = 0; i < commands.Length; i++)
                    {
                        tcpSendData = new byte[Encoding.UTF8.GetByteCount(commands[i])];
                        Debug.WriteLine("Sending message -> {0}", commands[i]);
                    tcpSendData = byteMessage(commands[i]);
                        ns.Write(tcpSendData, 0, tcpSendData.Length);
                        ns.Read(receiveData, 0, receiveData.Length);
                        response = Encoding.UTF8.GetString(receiveData, 0, receiveData.Length);
                        Debug.WriteLine("Received {0}!\n" +
                            "Response message -> {1}", receiveData.Length, response);
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine("Server communication exception -> {0}", e.Message);
                }

            }
            public static void SendAndReceive(SslStream stream)
            {
                byte[] receiveData = new byte[2048];
                byte[] sendData;
                string response;
                string[] commands;
                using (MessageConstructor messageConstructor = new MessageConstructor())
                {
                try
                {
                    commands = messageConstructor.HTMLMessageBuilder(MainWindow.input);
                }
                catch (FileNotFoundException fnfe)
                {
                    Debug.WriteLine("File not found exception -> {0}", fnfe);
                    commands = messageConstructor.MessageBuilder(MainWindow.input);
                }


            }
                try
                   {
                    stream.AuthenticateAsClient(serverName);

                    try
                    {
                        stream.Read(receiveData, 0, receiveData.Length);
                        response = Encoding.UTF8.GetString(receiveData, 0, receiveData.Length);
                        for (int i = 0; i < commands.Length; i++)
                        {
                            sendData = new byte[Encoding.UTF8.GetByteCount(commands[i])];
                            stream.Write(byteMessage(commands[i]));
                            stream.Read(receiveData, 0, receiveData.Length);
                            response = Encoding.UTF8.GetString(receiveData, 0, receiveData.Length);
                            Debug.WriteLine("Response from server -> {0}",response);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Server communication exception -> {0}", e.Message);
                    }


                }
                catch (AuthenticationException ae)
                {
                    Debug.WriteLine("Authentication exception -> {0}", ae.Message);
                }


            }

            private static bool CertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                if (sslPolicyErrors == SslPolicyErrors.None)
                {
                    return true;
                }
                else
                {
                    Debug.WriteLine("Certification exception -> {0}", sslPolicyErrors.ToString());
                    return false;
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