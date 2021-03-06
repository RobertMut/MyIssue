using MyIssue.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyIssue.Core.Aggregates
{
    public class AggregateClasses : IAggregateClasses
    {
        private List<Type> types;
        public AggregateClasses() => types = new List<Type>();
        public AggregateClasses(IEnumerable<Type> typeEnumerable) => types = typeEnumerable.ToList();
        public Type GetClassByName(string name) => types.Find(t => t.Name.Equals(name));

        public void Insert(Type t) => types.Add(t);
        public void RemoveByName(string name) => types.RemoveAt(IndexOfName(name));
        public void Remove(int index) => types.RemoveAt(index);
        private int IndexOfName(string name) => (from i in types
                                                 where i.Name == name
                                                 select types.IndexOf(i)).FirstOrDefault();
        public List<Type> GetAggregatedClasses() => types;
       
    }
}
