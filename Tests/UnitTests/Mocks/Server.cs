using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyIssue.Core.Exceptions;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Server;

namespace MyIssue.UnitTests.Mocks
{
    public class Server
    {
        private Mock<IServerConnector> _connector;

        public Mock<IServerConnector> Mock
        {
            get => _connector;
        }
        public Server()
        {
            _connector = new Mock<IServerConnector>();
            _connector.Setup(x =>
                    x.SendData(It.Is<IEnumerable<byte[]>>(x =>
                        CheckIfContainsLogout(x.Last())
                    )))
                .Returns((IEnumerable<byte[]> commandsToSend) =>
                {
                    string second = StringStatic.StringMessage(commandsToSend.ElementAt(2), commandsToSend.ElementAt(2).Length);
                    string third = StringStatic.StringMessage(commandsToSend.ElementAt(3), commandsToSend.ElementAt(3).Length);
                    if (second.ToLower().Contains("get")) return $"200 - {second} - {third}";
                    return $"203 - {second} - {third}";
                });
        }
        private bool CheckIfContainsLogout(byte[] last)
        {
            return StringStatic.StringMessage(last, last.Length).ToLower().Contains("logout");
        }
    }
}
