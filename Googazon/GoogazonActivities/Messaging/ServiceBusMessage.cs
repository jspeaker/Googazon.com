using GoogazonActivities.Messaging.Strategies;
using System.Threading.Tasks;

namespace GoogazonActivities.Messaging
{
    public class ServiceBusMessage
    {
        private readonly IServiceBusPostman _serviceBusPostman;

        public ServiceBusMessage(string messageBody, string topic, string need) : this(new ServiceBusPostmen(topic, need, messageBody)) { }

        protected ServiceBusMessage(IServiceBusPostman postman) => _serviceBusPostman = postman;

        public async Task SendAsync() => await _serviceBusPostman.SendAsync();
    }

    public class NullServiceBusMessage : ServiceBusMessage
    {
        public NullServiceBusMessage() : base(new ServiceBusUselessPostman()) { }
    }
}