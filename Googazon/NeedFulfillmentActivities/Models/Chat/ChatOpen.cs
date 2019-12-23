using NeedFulfillmentActivities.Facades;
using NeedFulfillmentActivities.Models.BusinessHours;
using NeedFulfillmentActivities.Texts;

namespace NeedFulfillmentActivities.Models.Chat
{
    public class ChatOpen : ContactMethodOpen
    {
        public ChatOpen() : base(new DateTimeFacade(), new OpenHours(new ChatHoursKey())) { }
    }
}