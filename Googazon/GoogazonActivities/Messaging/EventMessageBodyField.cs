using Googazon.Library.PrimitiveConcepts;
using Newtonsoft.Json.Linq;

namespace GoogazonActivities.Messaging
{
    public abstract class EventMessageBodyField
    {
        private readonly EventMessageBody _messageBody;
        private readonly Text _fieldName;

        protected EventMessageBodyField(EventMessageBody messageBody, Text fieldName)
        {
            _messageBody = messageBody;
            _fieldName = fieldName;
        }

        public static implicit operator string(EventMessageBodyField instance) => JObject.Parse(instance._messageBody)[instance._fieldName].Value<string>();
    }
}