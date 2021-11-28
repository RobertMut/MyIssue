using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Model;
using MyIssue.Web.Services;
using NUnit.Framework;

namespace MyIssue.UnitTests.Web
{
    public class UserTypesServiceTests
    {
        private IUserTypesService _service;
        private IServerConnector _serverConnector;
        private TokenAuth _auth;
        [SetUp]
        public void SetUp()
        {
            _auth = new TokenAuth
            {
                Login = "login",
                Token = "verylongtokenstring"
            };
            _serverConnector = new Mocks.Server().Mock.Object;
            _service = new UserTypesService(_serverConnector);
        }
        [Test]
        public void GetTest()
        {
            var result = _service.GetUserTypes(_auth).Result;
            Assert.IsInstanceOf(typeof(string), result);
            Assert.True(result.ToLower().Contains("200"));
            Assert.True(result.ToLower().Contains("getuser"));

        }
    }
}
