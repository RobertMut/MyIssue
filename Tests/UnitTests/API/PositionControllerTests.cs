using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MyIssue.API.Infrastructure;
using MyIssue.Main.API.Controllers;
using MyIssue.Main.API.Infrastructure;
using MyIssue.Main.API.Model;
using MyIssue.UnitTests.Mocks;
using NUnit.Framework;
using Unity.Injection;
using Task = System.Threading.Tasks.Task;

namespace MyIssue.UnitTests.API
{
    public class PositionControllerTests
    {
        private DBContext<MyIssueContext> _context;
        private PositionsController _positionsController;
        [SetUp]
        public void SetUp()
        {
            _context = new DBContext<MyIssueContext>();
            _context.Context.Positions.Add(new Position
            {
                PositionId = 1,
                PositionName = "test",
            });
            _positionsController = new PositionsController(_context.Context);
        }

        [Test]
        public async Task ReturnTest()
        {
            var response = await _positionsController.GetPositions();
            Assert.IsNotNull(response as OkObjectResult);

        }
    }
}
