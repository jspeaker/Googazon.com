using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System;
using System.Text;

namespace GoogazonActivities.Models
{
    public interface IEventMessage
    {
        EventData EventData();
        bool IsEventType(EventType eventType);
        string UniqueIdentifier();
        string Topic();
        string Need();
    }

    public class MessageBaseImplementation : MessageBase {
        public MessageBaseImplementation(EventType eventType, string need) : base(eventType, need) { }
    }

    public abstract class MessageBase : IEventMessage
    {
        protected MessageBase(EventType eventType, string need)
        {
            _need = need;
            EventType = eventType.ToString();
        }

        public EventData EventData() => new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this)));

        public bool IsEventType(EventType eventType) => eventType.ToString().Equals(EventType);

        public string UniqueIdentifier() => _id.ToString();

        public string Topic() => EventType;

        public string Need() => _need;

        [JsonProperty("eventType")]
        protected readonly string EventType;

        [JsonProperty("need")]
        // ReSharper disable once InconsistentNaming
        protected readonly string _need;

        [JsonProperty("createdDateTime")]
        private readonly DateTime _createdDateTime = DateTime.Now.ToUniversalTime();

        [JsonProperty("id")]
        private readonly Guid _id = Guid.NewGuid();
    }
}