using Demo.Dependency;
using Demo.Messaging.Events;

namespace Demo.Messaging.Handling
{
    /// <summary>
    /// Marker interface that makes it easier to discover handlers via reflection.
    /// </summary>
    public interface IEventHandler : ITransientDependency { }

    public interface IEventHandler<T> : IEventHandler, IHandler<T>
        where T : IEvent
    {
    }
}