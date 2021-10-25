using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyIssue.Core.Model.Request;
using MyIssue.Core.Model.Return;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Model;
using MyIssue.Web.Services;
using NUnit.Framework;

namespace MyIssue.UnitTests.Web
{
    public class EmployeesServiceTests
    {
        private IEmployeesService _service;
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
            _service = new EmployeesService(_serverConnector);
        }

        [Test]
        public void GetTest()
        {
            var result = _service.GetEmployees(null, _auth).Result;
            Assert.IsInstanceOf(typeof(string), result);
            Assert.True(result.ToLower().Contains("200"));
            Assert.True(result.ToLower().Contains("getemployee"));

        }

        [Test]
        public void CreateEmployeeTest()
        {
            var employee = new EmployeeReturn
            {
                Login = "samplelogin",
                Name = "name",
                Surname = "surname",
                No = "1234",
                Position = "test"
            };
            var result = _service.CreateEmployee(employee, _auth).Result;
            Assert.IsInstanceOf(typeof(string), result);
            Assert.True(result.ToLower().Contains("203"));
            Assert.True(result.ToLower().Contains("addemployee"));
            Assert.True(result.Contains(employee.Name) && result.Contains(employee.Surname) && result.Contains(employee.No));
        }
    }
}
