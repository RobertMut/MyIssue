using MyIssue.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyIssue.Core.Aggregates
{
    public class AggregateClasses : IAggregateClasses
    {
        private List<Type> types;
        public AggregateClasses() => types = new List<Type>();
        public IEnumerable<Type> GetAllClassTypes(string assembly, string nspace) { 
        return (from t in Assembly.Load(assembly).GetTypes()
                where t.IsClass && t.Namespace == nspace
                select t).ToList();
        } 
        public Type GetClassByName(string name) => types.Find(t => t.Name.Equals(name));

        public void Insert(Type t) => types.Add(t);
        public void RemoveByName(string name) => types.RemoveAt(IndexOfName(name));
        public void Remove(int index) => types.RemoveAt(index);
        private int IndexOfName(string name) => (from i in types
                                                 where i.Name == name
                                                 select types.IndexOf(i)).FirstOrDefault();
        public List<Type> GetAggregatedClasses() => types;
        public void SetTypes(IEnumerable<Type> typeEnumerable) => types = typeEnumerable.ToList();
    }
}
