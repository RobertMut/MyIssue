using MyIssue.Server.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server.Comm
{
    public class Communicate : ICommunicate
    {
        public string Receive(Socket sock, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            using (NetworkStream netStream = new NetworkStream(sock))
            {
                byte[] receiveBuffer = new byte[Parameters.BufferSize];
                netStream.ReadTimeout = Parameters.Timeout;
                try
                {
                    Tools t = new StringProcessing();
                    bool terminator = false;
                    string input = string.Empty;
                    int x = 0;
                    while (!terminator || !sock.Connected)
                    {
                        try
                        {
                            x = netStream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length).Result;
                            //Console.WriteLine("received: {0}", x);
                            input += t.StringMessage(receiveBuffer, x);
                            int f = input.IndexOf("\r\n<EOF>\r\n");
                            //Console.WriteLine("{0} - {1}", x, input);
                            if (x > 0 && !f.Equals(-1)) terminator = true;
                            //ct.ThrowIfCancellationRequested();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                    }
                    //Console.WriteLine(input);
                    input = input.Remove(input.Length - 9, 9);
                    //Console.WriteLine(input);
                    return input;
                }
                catch (Exception e)
                {
                    Write(sock, e.Message, ct);
                    netStream.Close();
                    sock.Close();
                    ClientCounter.Clients--;
                    return String.Empty;


                }
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
    }
}
