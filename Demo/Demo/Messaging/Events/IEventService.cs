using System.Threading.Tasks;

namespace Demo.Messaging.Events
{
    public interface IEventService
    {
        Task SaveEventAsync(IEvent @event);
        Task MarkEventAsPublishedAsync(IEvent @event);
    }
}