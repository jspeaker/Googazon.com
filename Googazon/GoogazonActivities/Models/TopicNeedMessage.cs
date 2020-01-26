using GoogazonActivities.TopicNeedActivity;
using Newtonsoft.Json;

namespace GoogazonActivities.Models
{
    public interface ITopicNeedMessage
    {
        string ClientIdentifier();
    }

    public class TopicNeedMessage : MessageBase, ITopicNeedMessage
    {
        [JsonConstructor]
        public TopicNeedMessage(string connectionId, string resourceIdentifier, string topic, string need) : this(topic, need)
        {
            _connectionId = connectionId;
            _resourceIdentifier = resourceIdentifier;
        }

        private TopicNeedMessage(string topic, string need) : base(new EventTypeText(topic).AsEnum(), need) { }

        [JsonProperty("resourceIdentifier")]
        private readonly string _resourceIdentifier;

        [JsonProperty("connectionId")]
        private readonly string _connectionId;

        public string ClientIdentifier() => _connectionId;
    }
}