using System;
using System.Collections.Generic;

namespace MyIssue.Core.Interfaces
{
    public interface IAggregateClasses
    {
        IEnumerable<Type> GetAllClassTypes(string assembly, string nspace);
        Type GetClassByName(string name);
        List<Type> GetAggregatedClasses();
        void Insert(Type t);
        void Remove(int index);
        void RemoveByName(string name);
    }
}