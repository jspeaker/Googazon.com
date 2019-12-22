using System.Threading.Tasks;

namespace GoogazonActivities.Messaging.Strategies
{
    public class ServiceBusPostmen : IServiceBusPostman
    {
        private readonly IServiceBusPostman _postman;

        public ServiceBusPostmen(string topic, string need, string messageBody) : this(new ServiceBusTopicPostman(topic, need, messageBody, new ServiceBusQueuePostman(messageBody, new ServiceBusUselessPostman()))) { }

        private ServiceBusPostmen(IServiceBusPostman postman) => _postman = postman;

        public async Task SendAsync() => await _postman.SendAsync();
    }
}