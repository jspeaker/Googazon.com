using NeedFulfillmentActivities.Facades;
using NeedFulfillmentActivities.Models.BusinessHours;
using NeedFulfillmentActivities.Texts;

namespace NeedFulfillmentActivities.Models.Email
{
    public class EmailOpen : ContactMethodOpen
    {
        public EmailOpen() : base(new DateTimeFacade(), new OpenHours(new EmailHoursKey())) { }
    }
}