using Microsoft.Azure.EventHubs;

namespace GoogazonActivities.Models
{
    public interface IEventMessage
    {
        EventData AsEventData();
        bool IsEventType(EventType eventType);
        string UniqueIdentifier();
    }
}