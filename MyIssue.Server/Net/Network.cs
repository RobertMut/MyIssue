﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server.Net
{
    public class Network : INetwork
    {
        private IProcessClient _processClient;
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
                    //Console.WriteLine(sock.Connected.ToString());
                    Task.Run(async () =>
                    {
                        ct = new CancellationTokenSource();
                        _processClient.ConnectedTask(sock, ct.Token);

                    }
                    );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                
            }
            finally
            {
                ClientCounter.Clients--;
            }

        }
        public async Task<string> Receive(Socket sock, CancellationToken ct)
        {
            using (NetworkStream netStream = new NetworkStream(sock))
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
                byte[] receiveBuffer = new byte[Parameters.BufferSize];
                netStream.ReadTimeout = Parameters.Timeout;
                Tools t = new StringProcessing();
                bool terminator = false;
                string input = string.Empty, workString = string.Empty;
                int x = 0;

                try
                {
                    cts.CancelAfter(60000);
                    while (!terminator || !sock.Connected)
                    {
                        ct.ThrowIfCancellationRequested();
                        x = await netStream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length, cts.Token);
                        input += t.StringMessage(receiveBuffer, x);
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
                    Console.WriteLine(tce.Message);

                }

                input = input.Remove(input.Length - 9, 9);
                return input;
            }
        }
        public void Write(Socket sock, string dataToSend, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            using (NetworkStream netStream = new NetworkStream(sock))
            {
                byte[] writeBuffer = new byte[Parameters.BufferSize];
                try
                {
                    Tools t = new StringProcessing();
                    writeBuffer = t.ByteMessage(dataToSend);
                    netStream.WriteAsync(writeBuffer, 0, writeBuffer.Length);
                }
                catch (Exception e)
                {
                    e.ToString();
                    netStream.Close();
                    sock.Close();
                    ClientCounter.Clients--;
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