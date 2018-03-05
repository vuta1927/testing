using System;
using MediatR;

namespace Demo.Messaging.Events
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }
        DateTime CreationDate { get; }
    }
}