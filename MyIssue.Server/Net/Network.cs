using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

using MyIssue.Server.Tools;

namespace MyIssue.Server.Net
{
    public class Network : INetwork
    {
        private IProcessClient _processClient;
        private IStringTools _tools; 
        public void Listener(string ipAddres, int port)
        {
            _processClient = new ProcessClient();
            Parameters.ConnBuffer = new byte[Parameters.BufferSize];
            Parameters.EndPoint = SetEndPoint(ipAddres, port);
            try
            {
                Parameters.ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Parameters.ListenSocket.Bind(Parameters.EndPoint);
                Parameters.ListenSocket.Listen(100);
                CancellationTokenSource ct;
                while (true)
                {
                    Console.WriteLine("Accepting...");
                    Socket sock = Parameters.ListenSocket.AcceptAsync().Result;
                    Task.Run(async () =>
                    {
                        ct = new CancellationTokenSource();
                        _processClient.ConnectedTask(sock, ct.Token);

                    }
                    );
                }
            }
            catch (ArgumentNullException ane)
            {
                ExceptionHandler.HandleMyException(ane);
                
            } catch (SocketException se)
            {
                ExceptionHandler.HandleMyException(se);
            }

        }
        public async Task<string> Receive(Socket sock, CancellationToken ct)
        {
            _tools = new StringTools();
            using (NetworkStream netStream = new NetworkStream(sock))
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
                byte[] receiveBuffer = new byte[Parameters.BufferSize];
                netStream.ReadTimeout = Parameters.Timeout;
                bool terminator = false;
                string input = string.Empty, workString = string.Empty;
                int x = 0;

                try
                {
                    cts.CancelAfter(600000);
                    while (!terminator || !sock.Connected)
                    {
                        ct.ThrowIfCancellationRequested();
                        x = await netStream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length, cts.Token);
                        input += _tools.StringMessage(receiveBuffer, x);
                        int f = input.IndexOf("\r\n<EOF>\r\n");
                        if (x > 0 && !f.Equals(-1)) terminator = true;
                    }
                }
                catch (TaskCanceledException tce)
                {
                    cts.Cancel();
                    netStream.Close();
                    terminator = true;
                    sock.Close();
                    ExceptionHandler.HandleMyException(tce);

                }

                input = input.Remove(input.Length - 9, 9);
                return input;
            }
        }
        public void Write(Socket sock, string dataToSend, CancellationToken ct)
        {
            _tools = new StringTools();
            ct.ThrowIfCancellationRequested();
            using (NetworkStream netStream = new NetworkStream(sock))
            {
                byte[] writeBuffer = new byte[Parameters.BufferSize];
                try
                {
                    writeBuffer = _tools.ByteMessage(dataToSend);
                    netStream.WriteAsync(writeBuffer, 0, writeBuffer.Length);
                }
                catch (ArgumentNullException ane)
                {
                    ExceptionHandler.HandleMyException(ane);
                    netStream.Close();
                    sock.Close();
                }


            }

        }
        private IPEndPoint SetEndPoint(string ipAddr, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddr);
            return new IPEndPoint(ip, port);
        }
    }
}
