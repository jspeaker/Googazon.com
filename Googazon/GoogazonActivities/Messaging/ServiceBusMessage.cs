using System.Threading.Tasks;
using GoogazonActivities.Messaging.Strategies;

namespace GoogazonActivities.Messaging
{
    public class ServiceBusMessage
    {
        private readonly IServiceBusPostman _serviceBusPostman;

        public ServiceBusMessage(string messageBody, string topic, string need) : this(new ServiceBusPostmen(topic, need, messageBody)) { }

        public ServiceBusMessage(IServiceBusPostman postman) => _serviceBusPostman = postman;

        public async Task SendAsync() => await _serviceBusPostman.SendAsync();
    }
}