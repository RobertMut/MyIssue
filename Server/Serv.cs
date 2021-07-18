using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public class Serv
    {


        public virtual async Task Listen() { }

        private bool IsConnected(Socket socket, CancellationToken ct)
        {
            
            bool poll = socket.Poll(1000, SelectMode.SelectRead);
            bool aval = socket.Available == 0;
            if (poll && aval)
                return false;
            else
                return true;
            //run after connected task
            //kill using cancelation token if no connection after 10s
        }




        }
    } 
