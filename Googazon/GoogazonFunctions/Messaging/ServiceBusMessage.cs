using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace GoogazonFunctions.Messaging
{
    public class ServiceBusMessage
    {
        private readonly string _messageBody;
        private readonly ITopicClient _topicClient;
        private readonly IQueueClient _queueClient;

        public ServiceBusMessage(string messageBody, string topic) : 
            this(messageBody, ServiceBusTopic.Client(topic), EnrichedMessagesQueue.Client()) { }

        public ServiceBusMessage(string messageBody, ITopicClient topicClient, IQueueClient queueClient)
        {
            _messageBody = messageBody;
            _topicClient = topicClient;
            _queueClient = queueClient;
        }

        public async Task SendAsync()
        {
            Message message = new Message(Encoding.UTF8.GetBytes(_messageBody));
            await _topicClient.SendAsync(message);

            if (!((IDictionary<string, object>) JsonConvert.DeserializeObject<ExpandoObject>(_messageBody)).ContainsKey("Results")) return;

            await _queueClient.SendAsync(message);
        }
    }
}