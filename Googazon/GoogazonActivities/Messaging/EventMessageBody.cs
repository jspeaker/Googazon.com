using Googazon.Library.PrimitiveConcepts;
using GoogazonActivities.Models;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System.Text;

namespace GoogazonActivities.Messaging
{
    public class EventMessageBody : Text
    {
        public EventMessageBody(EventData eventData) : base(Encoding.UTF8.GetString(eventData.Body.Array ?? new byte[0], eventData.Body.Offset, eventData.Body.Count)) { }

        public ServiceBusMessage AsServiceBusMessage()
        {
            try
            {
                return new ServiceBusMessage(this, new Topic(this), new Need(this));
            }
            catch
            {
                return new NullServiceBusMessage();
            }
        }

        public bool IsEventType(EventType eventType) => AsEventMessage().IsEventType(eventType);

        private IEventMessage AsEventMessage()
        {
            try
            {
                return JsonConvert.DeserializeObject<MessageBaseImplementation>(this);
            }
            catch
            {
                return new NullMessage();
            }
        }
    }
}