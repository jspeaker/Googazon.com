using GoogazonActivities.Texts;

namespace GoogazonActivities.Messaging
{
    public class Need : EventMessageBodyField
    {
        public Need(EventMessageBody messageBody) : base(messageBody, new NeedFieldName()) { }
    }
}