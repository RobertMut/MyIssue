using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyIssue.Core.Model.Request;
using MyIssue.Core.Model.Return;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Model;
using MyIssue.Web.Services;
using NUnit.Framework;

namespace MyIssue.UnitTests.Web
{
    public class UsersServiceTests
    {
        private IUsersService _service;
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
            _service = new UsersService(_serverConnector);
        }

        [Test]
        public void GetTest()
        {
            var result = _service.GetUsers(null, _auth).Result;
            Assert.IsInstanceOf(typeof(string), result);
            Assert.True(result.ToLower().Contains("200"));
            Assert.True(result.ToLower().Contains("getuser"));

        }

        [Test]
        public void GetByNameTest()
        {
            var result = _service.GetUsers("superlogin", _auth).Result;
            Assert.IsInstanceOf(typeof(string), result);
            Assert.True(result.ToLower().Contains("200"));
            Assert.True(result.ToLower().Contains("getuser"));
            Assert.True(result.Contains("superlogin"));
        }

        [Test]
        public void ChangePasswordTest()
        {
            var password = new Password
            {
                UserLogin = "superlogin",
                OldPassword = "superpass",
                NewPassword = "supernewpass"
            };
            var result = _service.ChangePassword(password, _auth).Result;
            Assert.IsInstanceOf(typeof(string), result);
            Assert.True(result.ToLower().Contains("203"));
            Assert.True(result.ToLower().Contains("changepassword"));
            Assert.True(result.Contains("superpass") && result.Contains("supernewpass"));
        }

        [Test]
        public void CreateUserTest()
        {
            var user = new UserReturn
            {
                Username = "testuser",
                Password = "1234",
                Type = "normal"
            };
            var result = _service.CreateUser(user, _auth).Result;
            Assert.IsInstanceOf(typeof(string), result);
            Assert.True(result.ToLower().Contains("203"));
            Assert.True(result.ToLower().Contains("adduser"));
            Assert.True(result.Contains(user.Password) && result.Contains(user.Type));
        }
    }
}
