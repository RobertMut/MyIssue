using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyIssue.Core.DataTransferObjects.Request;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Identity.API.Controllers;
using MyIssue.Identity.API.Infrastructure;
using MyIssue.Identity.API.Model;
using MyIssue.Identity.API.Services;
using MyIssue.UnitTests.Mocks;
using Newtonsoft.Json;
using NUnit.Framework;
using UserType = MyIssue.Identity.API.Model.UserType;

namespace MyIssue.UnitTests.Identity.API
{
    public class UsersControllerTests
    {
        private DBContext<IdentityContext> _context;
        private UserController _controller;
        private IAuthService _auth;
        private IConfiguration _configuration;

        public UsersControllerTests()
        {

            _context = new DBContext<IdentityContext>();
            _context.Context.Users.Add(new User
            {
                UserLogin = "test",
                Password = "1273",
                UserType = 2
            });
            _context.Context.UserTypes.Add(new UserType
            {
                Id = 1,
                Name = "Admin"
            });
            _context.Context.UserTypes.Add(new UserType
            {
                Id = 2,
                Name = "Normal"
            });
            _context.Context.SaveChanges();
        }

        [SetUp]
        public void Setup()
        {
            var inMemory = new Dictionary<string, string>(){
                { "Token:Secret", "USED TO SIGN AND VERIFY JWT TOKEN"},
                { "Issuer", "issuer"},
                { "Audience", "audience"}
            };

            _configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemory).Build();
            _auth = new AuthService(_context.Context, _configuration);
            _controller = new UserController(_context.Context);
        }

        [Test]
        public async Task GetAllTest()
        {
            var users = await _controller.GetUsersData();
            Assert.NotNull(users);
            Assert.True((users as OkObjectResult).StatusCode == 200);

        }
        [Test]
        public async Task GetUserTest()
        {
            var user = await _controller.GetUser("test");
            Assert.NotNull(user);
            Assert.True((user as OkObjectResult).StatusCode == 200);
        }
        [Test]
        public async Task PostUserTest()
        {
            var res = await _controller.PostUser(new UserReturn
            {
                Username = "test2",
                Password = "1243",
                Type = "Normal"
            });
            Assert.NotNull(_context.Context.Users.First(x => x.UserLogin == "test2"));
            Assert.IsInstanceOf(typeof(ActionResult<User>), res);
        }
    }
}
