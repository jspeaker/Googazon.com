using GoogazonActivities.Models;
using Newtonsoft.Json;

namespace GoogazonActivities.CustomerService
{
    public class CustomerServiceMessage : MessageBase
    {
        private const EventType DefaultEventType = Models.EventType.CustomerService;

        [JsonConstructor]
        public CustomerServiceMessage(string customerId, string need) : this(DefaultEventType, need) => _customerId = customerId;

        private CustomerServiceMessage(EventType eventType, string need) : base(eventType, need) { }

        [JsonProperty("customerId")]
        private readonly string _customerId;
    }
}