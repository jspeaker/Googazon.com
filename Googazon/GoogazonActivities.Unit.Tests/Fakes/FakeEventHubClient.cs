using System.Threading.Tasks;
using GoogazonActivities.Messaging.EventHub;
using Microsoft.Azure.EventHubs;

namespace GoogazonActivities.Unit.Tests.Fakes
{
    public class FakeEventHubClient : IEventHubClient
    {
        public int CallCount;

        public Task SendAsync(EventData eventData)
        {
            CallCount++;
            return Task.CompletedTask;
        }
    }
}