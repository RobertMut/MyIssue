using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyIssue.API.Infrastructure;

namespace MyIssue.UnitTests.Mocks
{
    public class DBContext
    {
        private MyIssueContext _context;

        public DBContext(MyIssueContext context)
        {
            _context = context;
        }

        public DBContext()
        {
            var options = new DbContextOptionsBuilder<MyIssueContext>().UseInMemoryDatabase("MyIssueDB").Options;
            _context = new MyIssueContext(options);
        }

        public MyIssueContext Context
        {
            get => _context;
        }
    }
}
