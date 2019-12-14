using System;
using GoogazonFunctions.Texts;
using Microsoft.Azure.EventHubs;

namespace GoogazonFunctions.Messaging
{
    public class EventHub
    {
        private static readonly EventHubClient Instance = null;
        private static readonly object LockObject = new object();

        public static EventHubClient Client
        {
            get
            {
                // ReSharper disable once InconsistentlySynchronizedField
                if (Instance != null) return Instance;

                lock (LockObject)
                {
                    if (Instance != null) return Instance;

                    return EventHubClient.CreateFromConnectionString(
                        new EventHubsConnectionStringBuilder(Environment.GetEnvironmentVariable(new EventHubConnectionStringKey()))
                        {
                            EntityPath = new RapidsKey(),
                            TransportType = TransportType.AmqpWebSockets
                        }.ToString());
                }
            }
        }
    }
}