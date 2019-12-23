using NeedFulfillmentActivities.Facades;
using System;

namespace NeedFulfillmentActivities.Models.BrickAndMortar
{
    public class BrickAndMortarOpen : IContactMethodOpen
    {
        private readonly IDateTime _dateTime;
        public BrickAndMortarOpen() : this(new DateTimeFacade()) { }

        public BrickAndMortarOpen(IDateTime dateTime) => _dateTime = dateTime;

        private readonly TimeSpan _weekdayOpenFrom = TimeSpan.FromHours(10);
        private readonly TimeSpan _weekdayClosedAt = TimeSpan.FromHours(16);
        private readonly TimeSpan _weekendOpenFrom = TimeSpan.FromHours(11);
        private readonly TimeSpan _weekendClosedAt = TimeSpan.FromHours(14);

        public bool IsOpen()
        {
            DateTime now = _dateTime.UtcNow();
            if (now.DayOfWeek == DayOfWeek.Sunday) return false;
            if (now.DayOfWeek == DayOfWeek.Saturday) return now.TimeOfDay >= _weekendOpenFrom && now.TimeOfDay <= _weekendClosedAt;
            return now.DayOfWeek != DayOfWeek.Sunday && now.TimeOfDay >= _weekdayOpenFrom && now.TimeOfDay <= _weekdayClosedAt;
        }

        public string Hours()
        {
            DateTime now = _dateTime.UtcNow();
            if (now.DayOfWeek == DayOfWeek.Sunday) return "Closed on Sunday.";
            if (now.DayOfWeek == DayOfWeek.Saturday) return $"{_weekendOpenFrom} until {_weekendClosedAt}";
            return $"{_weekdayOpenFrom} until {_weekdayClosedAt}";
        }
    }
}