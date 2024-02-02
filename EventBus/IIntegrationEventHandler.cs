using System.Threading.Tasks;

namespace EventBus
{
    public interface IIntegrationEventHandler
    {
        Task Handle(string eventName, string eventData);
    }
}
