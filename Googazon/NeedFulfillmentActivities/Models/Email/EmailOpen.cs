using NeedFulfillmentActivities.Facades;
using System;

namespace NeedFulfillmentActivities.Models.Email
{
    public class EmailOpen : IContactMethodOpen
    {
        private readonly IDateTime _dateTime;
        public EmailOpen() : this(new DateTimeFacade()) { }

        public EmailOpen(IDateTime dateTime) => _dateTime = dateTime;

        private readonly TimeSpan _openFrom = TimeSpan.FromHours(5);
        private readonly TimeSpan _closedAt = TimeSpan.FromHours(20);

        public bool IsOpen() => _dateTime.UtcNow().TimeOfDay >= _openFrom && _dateTime.UtcNow().TimeOfDay <= _closedAt;
        public string Hours()
        {
            return $"{_openFrom} until {_closedAt}";
        }
    }
}