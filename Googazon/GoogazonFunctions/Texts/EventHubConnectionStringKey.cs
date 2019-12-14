namespace GoogazonFunctions.Texts
{
    public class EventHubConnectionStringKey
    {
        private const string Value = "EventHubConnectionString";
        public static implicit operator string(EventHubConnectionStringKey instance) => Value;
    }
}