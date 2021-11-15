using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MyIssue.API.Infrastructure;

namespace MyIssue.UnitTests.Mocks
{
    public class DBContext<T> where T: DbContext
    {
        private T _context;

        public DBContext(T context)
        {
            _context = context;
        }

        public DBContext()
        {
            var options = new DbContextOptionsBuilder<T>().UseInMemoryDatabase("DB").Options;
            _context = (T)Activator.CreateInstance(typeof(T), options);
        }

        public T Context
        {
            get => _context;
        }
    }
}
