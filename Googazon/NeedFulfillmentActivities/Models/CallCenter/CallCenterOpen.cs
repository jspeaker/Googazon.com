using NeedFulfillmentActivities.Facades;
using System;

namespace NeedFulfillmentActivities.Models.CallCenter
{
    public class CallCenterOpen : IContactMethodOpen
    {
        private readonly IDateTime _dateTime;
        public CallCenterOpen() : this(new DateTimeFacade()) { }

        public CallCenterOpen(IDateTime dateTime) => _dateTime = dateTime;

        private readonly TimeSpan _openFrom = TimeSpan.FromHours(6);
        private readonly TimeSpan _closedAt = TimeSpan.FromHours(18);

        public bool IsOpen() => _dateTime.UtcNow().TimeOfDay >= _openFrom && _dateTime.UtcNow().TimeOfDay <= _closedAt;
        public string Hours()
        {
            return $"{_openFrom} until {_closedAt}";
        }
    }
}