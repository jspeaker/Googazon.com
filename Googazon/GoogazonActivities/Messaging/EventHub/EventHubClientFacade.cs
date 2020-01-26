using Microsoft.Azure.EventHubs;
using System.Threading.Tasks;

namespace GoogazonActivities.Messaging.EventHub
{
    public interface IEventHubClient
    {
        Task SendAsync(EventData eventData);
    }

    public class EventHubClientFacade : IEventHubClient
    {
        private readonly EventHubClient _eventHubClient;

        public EventHubClientFacade() : this(Googazon.Library.Messaging.EventHub.Client) { }

        private EventHubClientFacade(EventHubClient eventHubClient) => _eventHubClient = eventHubClient;

        public async Task SendAsync(EventData eventData) => await _eventHubClient.SendAsync(eventData);
    }
}