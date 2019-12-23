using GoogazonActivities.Models;
using Newtonsoft.Json;

namespace GoogazonActivities.TopicNeedActivity
{
    public class TopicNeedMessage : MessageBase
    {
        [JsonConstructor]
        public TopicNeedMessage(string customerId, string topic, string need) : this(topic, need) => _customerId = customerId;

        private TopicNeedMessage(string topic, string need) : base(new EventTypeText(topic).AsEnum(), need) { }

        [JsonProperty("customerId")]
        private readonly string _customerId;
    }
}