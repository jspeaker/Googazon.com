using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GoogazonFunctions.Functions
{
    public static class CustomServiceCallCenterFunction
    {
        [FunctionName("CustomServiceCallCenter")]
        public static async  Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customer/{customerId}/customerservice/callcenter")] 
            HttpRequest req,
            string customerId,
            ILogger log)
        {
            try
            {
                dynamic valueAsync = await new CustomerServiceCallCenterActivity(customerId).ValueAsync();
                return new OkObjectResult(valueAsync);
            }
            catch (Exception e)
            {
                log.LogError(e, e.Message);
                return new InternalServerErrorResult();
            }
        }
    }

    public class CustomerServiceCallCenterActivity : CustomerServiceActivity<CustomerServiceMessage>
    {
        public CustomerServiceCallCenterActivity(string customerId) : base(customerId) { }
    }

    public abstract class CustomerServiceActivity<TMessage> : INeedActivity
    {
        private readonly string _customerId;

        protected CustomerServiceActivity(string customerId)
        {
            _customerId = customerId;
        }

        public async Task<dynamic> ValueAsync()
        {
            EventData eventData = await new Need().SendAsync();

            return new {State = "closed"};
        }
    }

    public interface INeedActivity
    {
        Task<dynamic> ValueAsync();
    }

    public interface INeed
    {
        Task<EventData> SendAsync();
    }

    public class Need : INeed
    {
        private readonly EventHubClient _eventHubClient;

        public Need() : this(new EventHubsConnectionStringBuilder("Endpoint=sb://googazon-rapids.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=JO4d/Bp05LEZWuITU52ZaUpO0u2nq5Bi1R2WmrsQaZw=")
        {
            EntityPath = "rapids",
            TransportType = TransportType.AmqpWebSockets
        }.ToString()) { }

        private Need(string connectionString) : this(EventHubClient.CreateFromConnectionString(connectionString)) { }

        private Need(EventHubClient eventHubClient)
        {
            _eventHubClient = eventHubClient;
        }

        public Task<EventData> SendAsync()
        {
            _eventHubClient.SendAsync(new EventData(ArraySegment<byte>.Empty));
            return Task.FromResult(new EventData(ArraySegment<byte>.Empty));
        }
    }

    public class CustomerServiceMessage : MessageBase
    {
        private const EventType DefaultEventType = EventType.CustomerServiceNeed;

        [JsonConstructor]
        public CustomerServiceMessage() : this(DefaultEventType) { }

        private CustomerServiceMessage(EventType eventType) : base(eventType) { }
    }

    public enum EventType
    {
        CustomerServiceNeed,
        OfferNeed
    }

    public interface IEventMessage
    {
        EventData EventData();
        bool IsEventType(EventType eventType);
    }

    public abstract class MessageBase : IEventMessage
    {
        protected MessageBase(EventType eventType) => EventType = eventType;

        public EventData EventData() => new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this)));

        public bool IsEventType(EventType eventType) => eventType.Equals(EventType);

        [JsonProperty("eventType")]
        protected readonly EventType EventType;

        [JsonProperty("createdDateTime")]
        private readonly DateTime _createdDateTime = DateTime.Now.ToUniversalTime();

        [JsonProperty("id")]
        private readonly Guid _id = Guid.NewGuid();
    }
}
