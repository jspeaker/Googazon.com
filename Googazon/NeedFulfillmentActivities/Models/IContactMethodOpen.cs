using NeedFulfillmentActivities.Facades;
using NeedFulfillmentActivities.Models.BusinessHours;

namespace NeedFulfillmentActivities.Models
{
    public interface IContactMethodOpen
    {
        bool IsOpen();
        OpenHours OpenHours();
    }

    public abstract class ContactMethodOpen : IContactMethodOpen
    {
        private readonly IDateTime _dateTime;
        private readonly IOpenHours _openHours;

        protected ContactMethodOpen(IDateTime dateTime, IOpenHours openHours)
        {
            _dateTime = dateTime;
            _openHours = openHours;
        }

        public bool IsOpen() => _openHours.IsOpen(_dateTime.UtcNow());

        public OpenHours OpenHours() => (OpenHours) _openHours;
    }
}