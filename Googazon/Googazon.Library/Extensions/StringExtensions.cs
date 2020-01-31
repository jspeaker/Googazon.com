using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Googazon.Library.Extensions
{
    public static class StringExtensions
    {
        public static Message AsServiceBusMessage(this string value) => new Message(Encoding.UTF8.GetBytes(value));

        public static Message AsServiceBusMessage(this string value, IEnumerable<UserProperty> userProperties)
        {
            Message serviceBusMessage = value.AsServiceBusMessage();
            foreach (UserProperty userProperty in userProperties)
            {
                serviceBusMessage.UserProperties.Add(userProperty.Value());
            }
            return serviceBusMessage;
        }

        public static T AsType<T>(this string value) where T : class => JsonConvert.DeserializeObject<T>(value);

        public static byte[] AsBytes(this string value) => Encoding.UTF8.GetBytes(value);
    }
    
}