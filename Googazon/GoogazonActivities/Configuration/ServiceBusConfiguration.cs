using Googazon.Library.Exceptions;
using GoogazonActivities.Texts.ConfigurationKeys;
using System;

namespace GoogazonActivities.Configuration
{
    public interface IServiceBusConfiguration
    {
        string ConnectionString();
    }

    public class ServiceBusConfiguration : IServiceBusConfiguration
    {
        public string ConnectionString()
        {
            string connectionString = Environment.GetEnvironmentVariable(new ServiceBusConnectionStringKey());
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ConfigurationItemNotFoundException(new ServiceBusConnectionStringKey());

            return connectionString;
        }
    }
}