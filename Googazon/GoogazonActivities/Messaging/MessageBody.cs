using System.Text;
using Googazon.Library.PrimitiveConcepts;
using Microsoft.Azure.EventHubs;

namespace GoogazonActivities.Messaging
{
    public class EventMessageBody : Text
    {
        public EventMessageBody(EventData eventData) : base(Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count)) { }
    }
}