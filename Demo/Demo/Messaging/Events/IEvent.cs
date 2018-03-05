using System;

namespace Demo.Messaging.Events
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime CreationDate { get; }
    }
}