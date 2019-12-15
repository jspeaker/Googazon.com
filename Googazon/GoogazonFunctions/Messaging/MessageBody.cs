using Microsoft.Azure.EventHubs;
using System.Text;

namespace GoogazonFunctions.Messaging
{
    public class EventMessageBody
    {
        private readonly EventData _eventData;

        public EventMessageBody(EventData eventData) => _eventData = eventData;

        public static implicit operator string(EventMessageBody instance) => Encoding.UTF8.GetString(instance._eventData.Body.Array, instance._eventData.Body.Offset, instance._eventData.Body.Count);
    }
}