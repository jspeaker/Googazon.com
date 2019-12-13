using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System;
using System.Text;

namespace GoogazonFunctions.Models
{
    public interface IEventMessage
    {
        EventData EventData();
        bool IsEventType(EventType eventType);
        string UniqueIdentifier();
    }

    public abstract class MessageBase : IEventMessage
    {
        protected MessageBase(EventType eventType) => EventType = eventType;

        public EventData EventData() => new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this)));

        public bool IsEventType(EventType eventType) => eventType.Equals(EventType);

        public string UniqueIdentifier() => _id.ToString();

        [JsonProperty("eventType")]
        protected readonly EventType EventType;

        [JsonProperty("createdDateTime")]
        private readonly DateTime _createdDateTime = DateTime.Now.ToUniversalTime();

        [JsonProperty("id")]
        private readonly Guid _id = Guid.NewGuid();
    }
}