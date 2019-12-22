using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Threading.Tasks;

namespace GoogazonFunctions.Messaging.Strategies
{
    public class ServiceBusTopicPostman : IServiceBusPostman
    {
        private readonly ITopicClient _topicClient;
        private readonly Message _message;
        private readonly IServiceBusPostman _nextPostman;

        public ServiceBusTopicPostman(string topic, string messageBody, IServiceBusPostman nextPostman) : this(ServiceBusTopic.Client(topic), new Message(Encoding.UTF8.GetBytes(messageBody)), nextPostman) { }

        public ServiceBusTopicPostman(ITopicClient topicClient, Message message, IServiceBusPostman nextPostman)
        {
            _topicClient = topicClient;
            _message = message;
            _nextPostman = nextPostman;
        }

        public async Task SendAsync()
        {
            await _topicClient.SendAsync(_message);
            await _nextPostman.SendAsync();
        }
    }
}