using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyIssue.Infrastructure.Database
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        void Delete(T entity);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void SaveChanges();
    }
}