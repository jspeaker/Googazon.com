using Googazon.Library.Exceptions;
using GoogazonActivities.Texts.ConfigurationKeys;
using System;

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
            if (!int.TryParse(Environment.GetEnvironmentVariable(new UniqueResultTimeoutMillisecondsKey()), out int timeout)) throw new ConfigurationItemNotFoundException(new UniqueResultTimeoutMillisecondsKey());
            
            return TimeSpan.FromMilliseconds(timeout);
        }
    }
}