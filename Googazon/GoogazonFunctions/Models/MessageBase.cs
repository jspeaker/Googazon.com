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
        string Topic();
    }

    public class CandidateMessage : MessageBase {
        public CandidateMessage(EventType eventType, string need) : base(eventType, need) { }
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

        public string Topic() => _need;

        [JsonProperty("eventType")]
        protected readonly string EventType;

        [JsonProperty("need")]
        protected readonly string _need;

        [JsonProperty("createdDateTime")]
        private readonly DateTime _createdDateTime = DateTime.Now.ToUniversalTime();

        [JsonProperty("id")]
        private readonly Guid _id = Guid.NewGuid();
    }
}