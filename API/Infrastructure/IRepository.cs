using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyIssue.Main.API.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void SaveChanges();
    }
}