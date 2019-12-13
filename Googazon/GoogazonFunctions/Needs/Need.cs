using GoogazonFunctions.Models;
using Microsoft.Azure.EventHubs;
using System.Threading.Tasks;

namespace GoogazonFunctions.Needs
{
    public class Need : INeed
    {
        private readonly IEventMessage _eventMessage;
        private readonly EventHubClient _eventHubClient;

        public Need(IEventMessage eventMessage) : this(eventMessage,
            new EventHubsConnectionStringBuilder("Endpoint=sb://googazon-rapids.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=JO4d/Bp05LEZWuITU52ZaUpO0u2nq5Bi1R2WmrsQaZw=")
            {
                EntityPath = "rapids",
                TransportType = TransportType.AmqpWebSockets
            }.ToString())
        { }

        private Need(IEventMessage eventMessage, string connectionString) : this(eventMessage, EventHubClient.CreateFromConnectionString(connectionString)) { }

        private Need(IEventMessage eventMessage, EventHubClient eventHubClient)
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
}