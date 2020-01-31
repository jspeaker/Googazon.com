using Googazon.Library.Extensions;
using GoogazonActivities.Texts;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace GoogazonActivities.Messaging.Strategies
{
    public class ServiceBusQueuePostman : IServiceBusPostman
    {
        private readonly IQueueClient _queueClient;
        private readonly string _messageBody;
        private readonly Message _message;
        private readonly IServiceBusPostman _nextPostman;

        public ServiceBusQueuePostman(string messageBody, IServiceBusPostman nextPostman) : this(EnrichedMessagesQueue.Client, messageBody, messageBody.AsServiceBusMessage(), nextPostman)  { }

        private ServiceBusQueuePostman(IQueueClient queueClient, string messageBody, Message message, IServiceBusPostman nextPostman)
        {
            _queueClient = queueClient;
            _messageBody = messageBody;
            _message = message;
            _nextPostman = nextPostman;
        }

        public async Task SendAsync()
        {
            if (!((IDictionary<string, object>) JsonConvert.DeserializeObject<ExpandoObject>(_messageBody)).ContainsKey(new ResultsFieldName()))
            {
                await _nextPostman.SendAsync();
                return;
            }
            
            await _queueClient.SendAsync(_message);
        }
    }
}