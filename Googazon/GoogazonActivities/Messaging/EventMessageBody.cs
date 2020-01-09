using Googazon.Library.PrimitiveConcepts;
using Microsoft.Azure.EventHubs;
using System.Text;

namespace GoogazonActivities.Messaging
{
    public class EventMessageBody : Text
    {
        public EventMessageBody(EventData eventData) : base(Encoding.UTF8.GetString(eventData.Body.Array ?? new byte[0], eventData.Body.Offset, eventData.Body.Count)) { }
    }
}