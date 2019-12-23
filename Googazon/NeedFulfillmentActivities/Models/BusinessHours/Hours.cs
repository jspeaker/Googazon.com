using Newtonsoft.Json;
using System;

namespace NeedFulfillmentActivities.Models.BusinessHours
{
    public interface IHours
    {
        bool IsOpen(DayOfWeek dayOfWeek, TimeSpan time);
    }

    public class Hours : IHours
    {
        [JsonProperty("DayOfWeek")]
        private readonly DayOfWeek _dayOfWeek;

        [JsonProperty("OpenTime")]
        private readonly TimeSpan _openTime;

        [JsonProperty("CloseTime")]
        private readonly TimeSpan _closeTime;

        public bool IsOpen(DayOfWeek dayOfWeek, TimeSpan time) => _dayOfWeek == dayOfWeek && !ClosedAllDay() && time >= _openTime && time <= _closeTime;

        private bool ClosedAllDay() => _openTime == _closeTime;
    }
}