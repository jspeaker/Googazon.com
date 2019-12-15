using Googazon.Library.Messaging;
using GoogazonFunctions.Models;
using Microsoft.Azure.EventHubs;
using System.Threading.Tasks;

namespace GoogazonFunctions.Needs
{
    public interface INeed
    {
        Task<EventData> SendAsync();
    }

    public class Need : INeed
    {
        private readonly IEventMessage _eventMessage;
        private readonly IEventHubClient _eventHubClient;

        public Need(IEventMessage eventMessage) : this(eventMessage, new EventHubClientFacade()) { }

        public Need(IEventMessage eventMessage, IEventHubClient eventHubClient)
        {
            _eventMessage = eventMessage;
            _eventHubClient = eventHubClient;
        }

        public async Task<EventData> SendAsync()
        {
            await _eventHubClient.SendAsync(_eventMessage.EventData());
            return _eventMessage.EventData();
        }
    }

    public interface IEventHubClient
    {
        Task SendAsync(EventData eventData);
    }

    public class EventHubClientFacade : IEventHubClient
    {
        private readonly EventHubClient _eventHubClient;

        public EventHubClientFacade() : this(EventHub.Client) { }

        public EventHubClientFacade(EventHubClient eventHubClient) => _eventHubClient = eventHubClient;

        public async Task SendAsync(EventData eventData) => await _eventHubClient.SendAsync(eventData);
    }
}