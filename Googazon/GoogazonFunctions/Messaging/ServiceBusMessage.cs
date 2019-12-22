using GoogazonFunctions.Messaging.Strategies;
using System.Threading.Tasks;

namespace GoogazonFunctions.Messaging
{
    public class ServiceBusMessage
    {
        private readonly IServiceBusPostman _serviceBusPostman;

        public ServiceBusMessage(string messageBody, string topic) : this(new ServiceBusPostmen(topic, messageBody)) { }

        public ServiceBusMessage(IServiceBusPostman postman) => _serviceBusPostman = postman;

        public async Task SendAsync() => await _serviceBusPostman.SendAsync();
    }
}