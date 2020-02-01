using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace Googazon.Library.Extensions
{
    public static class DynamicExtensions
    {
        public static string Serialized(this ExpandoObject value) => JsonConvert.SerializeObject(value);
        public static EventData AsEventData(this ExpandoObject value) => new EventData(value.Serialized().AsBytes());
        public static bool HasProperty(this ExpandoObject value, string propertyName) => ((IDictionary<string, object>)value).ContainsKey(propertyName);
    }
}