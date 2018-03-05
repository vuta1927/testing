using System;
using Demo.Messaging.Events;
using Demo.Timing;

namespace Demo.Messaging
{
    /// <summary>
    /// This type of events can be used to notify for an exception.
    /// </summary>
    public class HandleEventException : Event
    {
        /// <summary>
        /// Exception object
        /// </summary>
        public Exception Exception { get; private set; }
        
        public HandleEventException(Exception exception)
        {
            Exception = exception;
        }
    }
}