using System.Collections.Generic;

namespace Googazon.Library.Extensions
{
    public class UserProperty
    {
        private readonly string _key;
        private readonly object _value;

        public UserProperty(string key, object value)
        {
            _key = key;
            _value = value;
        }

        public KeyValuePair<string, object> Value() => new KeyValuePair<string, object>(_key, _value);
    }
}