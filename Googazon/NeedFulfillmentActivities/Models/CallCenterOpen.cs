using System;
using NeedFulfillmentActivities.Facades;

namespace NeedFulfillmentActivities.Models
{
    public interface ICallCenterOpen
    {
        bool IsOpen();
    }

    public class CallCenterOpen : ICallCenterOpen
    {
        private readonly IDateTime _dateTime;
        public CallCenterOpen() : this(new DateTimeFacade()) { }

        public CallCenterOpen(IDateTime dateTime) => _dateTime = dateTime;

        public bool IsOpen() => _dateTime.UtcNow().TimeOfDay >= TimeSpan.FromHours(6) && _dateTime.UtcNow().TimeOfDay <= TimeSpan.FromHours(18);
    }
}