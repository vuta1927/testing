using System;

namespace Demo.Reflection
{
    public interface ITypeFinder
    {
        Type[] Find(Func<Type, bool> predicate);

        Type[] Find<T>();

        Type[] FindAll();
    }
}