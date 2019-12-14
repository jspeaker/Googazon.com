using System.Threading.Tasks;
using GoogazonFunctions.Needs;
using Microsoft.Azure.EventHubs;

namespace GoogazonFunctions.Unit.Tests.Fakes
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