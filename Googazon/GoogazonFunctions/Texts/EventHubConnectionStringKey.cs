namespace GoogazonFunctions.Texts
{
    public class EventHubConnectionStringKey
    {
        private readonly string _value = "EventHubConnectionString";
        public static implicit operator string(EventHubConnectionStringKey instance) => instance._value;
    }
}