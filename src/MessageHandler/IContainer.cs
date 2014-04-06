using System;
using System.Collections.Generic;

namespace MessageHandler
{
    public interface IContainer
    {
        void Register<T>(Lifecycle lifecycle);
        void Register<T>(Lifecycle lifecycle, Func<T> factory );
        void Register(Type type, Lifecycle lifecycle);
        void Register(Type type, Lifecycle lifecycle, Func<object> factory);

        T Resolve<T>();
        object Resolve(Type type);

        IEnumerable<T> ResolveAll<T>();
        IEnumerable<object> ResolveAll(Type type);
    }
}