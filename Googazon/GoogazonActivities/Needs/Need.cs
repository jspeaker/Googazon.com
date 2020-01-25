using System.Threading.Tasks;
using GoogazonActivities.Messaging.EventHub;
using GoogazonActivities.Models;
using Microsoft.Azure.EventHubs;

namespace GoogazonActivities.Needs
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
            await _eventHubClient.SendAsync(_eventMessage.AsEventData());
            return _eventMessage.AsEventData();
        }
    }
}