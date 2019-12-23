using NeedFulfillmentActivities.Facades;
using NeedFulfillmentActivities.Models.BusinessHours;
using NeedFulfillmentActivities.Texts;

namespace NeedFulfillmentActivities.Models.BrickAndMortar
{
    public class BrickAndMortarOpen : ContactMethodOpen
    {
        public BrickAndMortarOpen() : base(new DateTimeFacade(), new OpenHours(new BrickAndMortarHoursKey())) { }
    }
}