using Googazon.Library.Exceptions;
using Googazon.Library.PrimitiveConcepts;
using GoogazonActivities.Texts;
using GoogazonActivities.Texts.ConfigurationKeys;
using System;

namespace GoogazonActivities.Configuration
{
    public interface IResultConfiguration
    {
        TimeSpan UniqueResultTimeout();
        TimeSpan PollingFrequency();
    }

    public class ResultConfiguration : IResultConfiguration
    {
        public TimeSpan UniqueResultTimeout()
        {
            return ConfigMillisecondsAsTimeSpan(new UniqueResultTimeoutMillisecondsKey());
        }

        public TimeSpan PollingFrequency()
        {
            return ConfigMillisecondsAsTimeSpan(new UniqueResultPollingFrequencyKey());
        }

        private static TimeSpan ConfigMillisecondsAsTimeSpan(Text key)
        {
            string configValue = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrWhiteSpace(configValue)) throw new ConfigurationItemNotFoundException(key);
            if (!int.TryParse(configValue, out int returnValue)) throw new ConfigurationItemParsingException(key);

            return TimeSpan.FromMilliseconds(returnValue);
        }
    }
}