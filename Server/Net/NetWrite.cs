﻿using MyIssue.Core.Entities;
using MyIssue.Core.String;
using System.Net.Sockets;
using System.Threading;
using MyIssue.Core.Exceptions;
using System;
using MyIssue.Infrastructure.Files;

namespace MyIssue.Server.Net
{
    public class NetWrite
    {
        public static void Write(Socket sock, string dataToSend, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            using (NetworkStream netStream = new NetworkStream(sock))
            {
                byte[] writeBuffer = new byte[Parameters.BufferSize];
                try
                {
                    writeBuffer = StringStatic.ByteMessage(dataToSend);
                    netStream.WriteAsync(writeBuffer, 0, writeBuffer.Length);
                }
                catch (ArgumentNullException ane)
                {
                    SerilogLogger.ServerLogException(ane);
                    netStream.Close();
                    sock.Close();
                }
            }
        }
    }
}
