using GoogazonFunctions.Models;
using Newtonsoft.Json;

namespace GoogazonFunctions.Functions.CustomerService
{
    public class CustomerServiceMessage : MessageBase
    {
        private const EventType DefaultEventType = EventType.CustomerServiceNeed;

        [JsonConstructor]
        public CustomerServiceMessage(string customerId) : this(DefaultEventType) => _customerId = customerId;

        private CustomerServiceMessage(EventType eventType) : base(eventType) { }

        [JsonProperty("customerId")]
        private readonly string _customerId;
    }
}