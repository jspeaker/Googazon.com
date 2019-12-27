using Microsoft.Azure.EventHubs;
using NeedFulfillmentActivities.Texts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace NeedFulfillmentActivities.Messaging
{
    public class ServiceBusMessage
    {
        private readonly Lazy<dynamic> _lazyMessageObject;

        private dynamic MessageObject => _lazyMessageObject.Value;

        public ServiceBusMessage(string serviceBusMessage) : this(new Lazy<dynamic>(() => JsonConvert.DeserializeObject<ExpandoObject>(serviceBusMessage))) { }

        public ServiceBusMessage(Lazy<dynamic> lazyMessageObject) => _lazyMessageObject = lazyMessageObject;

        public static implicit operator string(ServiceBusMessage instance) => JsonConvert.SerializeObject(instance.MessageObject);

        public bool IsEnriched() => ((IDictionary<string, object>) MessageObject).ContainsKey(new ResultsKey());

        public ServiceBusMessage EnrichedInstance(dynamic enrichment)
        {
            ServiceBusMessage newInstance = new ServiceBusMessage(JsonConvert.SerializeObject(MessageObject));
            if (newInstance.IsEnriched()) return WithAdditionalResult(newInstance, enrichment);

            newInstance.MessageObject.Results = new List<dynamic> { enrichment };
            return newInstance;
        }

        public EventData AsEventData() => new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_lazyMessageObject.Value)));

        private static ServiceBusMessage WithAdditionalResult(ServiceBusMessage serviceBusMessage, dynamic additionalResult)
        {
            ((List<dynamic>) serviceBusMessage.MessageObject.Results).Add(additionalResult);
            return serviceBusMessage;
        }
    }
}