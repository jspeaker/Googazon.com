namespace GoogazonFunctions.Texts
{
    public class EnrichedMessagesQueueName
    {
        private readonly string _value = "enrichedmessages";
        public static implicit operator string(EnrichedMessagesQueueName instance) => instance._value;
    }
}