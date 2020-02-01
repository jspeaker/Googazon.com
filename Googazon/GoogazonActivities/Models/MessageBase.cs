using Googazon.Library.Extensions;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System;

namespace GoogazonActivities.Models
{
    public interface IEventMessage
    {
        EventData AsEventData();
        bool IsEventType(EventType eventType);
        string UniqueIdentifier();
    }

    public class MessageBaseImplementation : MessageBase {
        public MessageBaseImplementation(EventType eventType, string need) : base(eventType, need) { }
    }

    public abstract class MessageBase : IEventMessage
    {
        protected MessageBase(EventType eventType, string need)
        {
            Need = need;
            EventType = eventType.ToString();
        }

        public EventData AsEventData() => JsonConvert.SerializeObject(this).AsEventData();

        public bool IsEventType(EventType eventType) => eventType.ToString().Equals(EventType);

        public string UniqueIdentifier() => _id.ToString();

        [JsonProperty("eventType")]
        protected readonly string EventType;

        [JsonProperty("need")]
        protected readonly string Need;

        [JsonProperty("createdDateTime")]
        private readonly DateTime _createdDateTime = DateTime.Now.ToUniversalTime();

        [JsonProperty("id")]
        private readonly Guid _id = Guid.NewGuid();
    }
}