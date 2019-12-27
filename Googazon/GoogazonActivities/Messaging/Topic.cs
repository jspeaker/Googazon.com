using GoogazonActivities.Texts;

namespace GoogazonActivities.Messaging
{
    public class Topic : EventMessageBodyField
    {
        public Topic(EventMessageBody messageBody) : base(messageBody, new EventTypeFieldName()) { }
    }
}