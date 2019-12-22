using System;

namespace NeedFulfillmentActivities.Facades
{
    public interface IDateTime
    {
        DateTime UtcNow();
    }

    public class DateTimeFacade : IDateTime
    {
        public DateTime UtcNow() => DateTime.UtcNow;
    }
}