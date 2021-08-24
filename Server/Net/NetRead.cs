using MyIssue.Core.Entities;
using MyIssue.Core.String;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MyIssue.Infrastructure.Files;
using Parameters = MyIssue.Server.Entities.Parameters;

namespace MyIssue.Server.Net
{
    class NetRead
    {
        public static async Task<string> Receive(Socket sock, CancellationToken ct)
        {
            using (NetworkStream netStream = new NetworkStream(sock))
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
                byte[] receiveBuffer = new byte[Parameters.BufferSize];
                netStream.ReadTimeout = Parameters.Timeout;
                bool terminator = false;
                string input = string.Empty;
                int x = 0;

                try
                {
                    cts.CancelAfter(Parameters.Timeout);
                    while (!terminator)
                    {
                        ct.ThrowIfCancellationRequested();
                        x = await netStream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length, cts.Token);
                        input += StringStatic.StringMessage(receiveBuffer, x);
                        if (x > 0 && !input.IndexOf("\r\n<EOF>\r\n").Equals(-1)) terminator = true;
                    }
                }
                catch (TaskCanceledException tce)
                {
                    cts.Cancel();
                    netStream.Close();
                    terminator = true;
                    sock.Close();
                    SerilogLogger.ServerLogException(tce);

                }
                return input.Remove(input.Length - 9, 9);
            }
        }
    }
}
