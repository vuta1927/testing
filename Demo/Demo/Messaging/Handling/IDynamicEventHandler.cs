using System.Threading.Tasks;

namespace Demo.Messaging.Handling
{
    public interface IDynamicEventHandler
    {
        Task HandleAsync(dynamic eventData);
    }
}