using NeedFulfillmentActivities.Facades;
using NeedFulfillmentActivities.Models.BusinessHours;
using NeedFulfillmentActivities.Texts;

namespace NeedFulfillmentActivities.Models.CallCenter
{
    public class CallCenterOpen : ContactMethodOpen
    {
        public CallCenterOpen() : base(new DateTimeFacade(), new OpenHours(new CallCenterHoursKey())) { }
    }
}