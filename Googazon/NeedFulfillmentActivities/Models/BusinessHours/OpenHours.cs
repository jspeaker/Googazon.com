using Googazon.Library.PrimitiveConcepts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeedFulfillmentActivities.Models.BusinessHours
{
    public interface IOpenHours
    {
        bool IsOpen(DateTime dateTime);
    }

    public class OpenHours : IOpenHours
    {
        [JsonProperty("OpenHours")]
        private readonly List<Hours> _hours;

        public OpenHours(Text businessHoursKey) : this(JsonConvert.DeserializeObject<List<Hours>>(Environment.GetEnvironmentVariable(businessHoursKey))) { }

        private OpenHours(List<Hours> hours) => _hours = hours;

        public bool IsOpen(DateTime dateTime) => _hours.Any(h => h.IsOpen(dateTime.DayOfWeek, dateTime.TimeOfDay));
    }
}