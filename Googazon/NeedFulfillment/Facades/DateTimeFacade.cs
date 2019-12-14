using System;

namespace NeedFulfillment.Facades
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