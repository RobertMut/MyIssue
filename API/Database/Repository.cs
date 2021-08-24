using MyIssue.Core.Interfaces;
using MyIssue.Infrastructure.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyIssue.Infrastructure.Database
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected MyIssueContext _context;
        public Repository(MyIssueContext context)
        {
            _context = context;
        }
        public virtual T Add(T entity)
        {
            return _context.Set<T>().Add(entity);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsQueryable().Where(predicate).ToList();
        }
        public virtual T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
