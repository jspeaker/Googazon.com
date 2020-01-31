using Googazon.Library.Extensions;
using GoogazonActivities.Texts.ServiceBus;
using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;

namespace GoogazonActivities.Messaging.Strategies
{
    public class ServiceBusTopicPostman : IServiceBusPostman
    {
        private readonly ITopicClient _topicClient;
        private readonly Message _message;
        private readonly IServiceBusPostman _nextPostman;

        public ServiceBusTopicPostman(string topic, string need, string messageBody, IServiceBusPostman nextPostman) : 
            this(ServiceBusTopic.Client(topic), messageBody.AsServiceBusMessage(new []
            {
                new UserProperty(new NeedUserPropertyName(), need)
            }), nextPostman) { }

        private ServiceBusTopicPostman(ITopicClient topicClient, Message message, IServiceBusPostman nextPostman)
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