using MyIssue.Core.Entities.Database;
using MyIssue.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyIssue.Infrastructure.Database
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected MyIssueDatabase _context;
        public Repository(MyIssueDatabase context)
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
