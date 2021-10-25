using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MyIssue.API.Controllers;
using MyIssue.API.Model;
using MyIssue.UnitTests.Mocks;
using NUnit.Framework;
using Task = System.Threading.Tasks.Task;

namespace MyIssue.UnitTests.API
{
    class UserTypesControllerTests
    {
        private DBContext _context;
        private UserTypesController _userTypes;
        [SetUp]
        public void SetUp()
        {
            _context = new DBContext();
            _context.Context.UserTypes.Add(new UserType
            {
                Id = 1,
                Name = "Test"
            });
            _userTypes = new UserTypesController(_context.Context);
        }

        [Test]
        public async Task ReturnTest()
        {
            var response = await _userTypes.GetUserTypes();
            Assert.IsNotNull(response as OkObjectResult);

        }
    }
}
