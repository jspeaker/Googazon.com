namespace GoogazonActivities.Models
{
    public class NullMessage : MessageBase
    {
        public NullMessage() : base(Models.EventType.None, string.Empty) { }
    }
}