using System;
using GoogazonActivities.Texts.ConfigurationKeys;

namespace GoogazonActivities.Configuration
{
    public interface IResultConfiguration
    {
        TimeSpan UniqueResultTimeout();
    }

    public class ResultConfiguration : IResultConfiguration
    {
        public TimeSpan UniqueResultTimeout()
        {
            if (!int.TryParse((string) Environment.GetEnvironmentVariable(new UniqueResultTimeoutMillisecondsKey()), out int timeout)) throw new Exception("Result timeout could not be parsed from configuration.");
            return new TimeSpan(0, 0, 0, 0, timeout);
        }
    }
}