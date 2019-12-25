using Googazon.Library.Exceptions;
using Googazon.Library.Texts;
using System;

namespace Googazon.Library.Configuration
{
    public interface IEventHubConfiguration
    {
        string ConnectionString();
    }

    public class EventHubConfiguration : IEventHubConfiguration
    {
        public string ConnectionString()
        {
            string connectionString = Environment.GetEnvironmentVariable(new EventHubConnectionStringKey());
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ConfigurationItemNotFoundException(new EventHubConnectionStringKey());

            return connectionString;
        }
    }
}