using Microsoft.Azure.EventHubs;
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

        public bool IsEnriched() => ((IDictionary<string, object>) MessageObject).ContainsKey("Results");

        public ServiceBusMessage EnrichedInstance(dynamic enrichment)
        {
            ServiceBusMessage newMessageObject = new ServiceBusMessage(JsonConvert.SerializeObject(MessageObject));
            // TODO: SRP this with strategies - violated guard clause policy
            if (newMessageObject.IsEnriched())
            {
                ((List<dynamic>) newMessageObject.MessageObject.Results).Add(enrichment);
                return newMessageObject;
            }

            newMessageObject.MessageObject.Results = new List<dynamic> { enrichment };
            return newMessageObject;
        }

        public EventData AsEventData() => new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_lazyMessageObject.Value)));
    }
}