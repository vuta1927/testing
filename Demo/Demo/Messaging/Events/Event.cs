using System;
using Demo.Timing;

namespace Demo.Messaging.Events
{
    public class Event : IEvent
    {
        public Guid Id { get; }
        public DateTime CreationDate { get; }

        public Event()
        {
            Id = Guid.NewGuid();
            CreationDate = Clock.Now;
        }
    }
}