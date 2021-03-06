using Googazon.Library.Configuration;
using Googazon.Library.Texts;
using Microsoft.Azure.EventHubs;

namespace Googazon.Library.Messaging
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
                        new EventHubsConnectionStringBuilder(new EventHubConfiguration().ConnectionString())
                        {
                            EntityPath = new RapidsKey(),
                            TransportType = TransportType.AmqpWebSockets
                        }.ToString());
                }
            }
        }
    }
}