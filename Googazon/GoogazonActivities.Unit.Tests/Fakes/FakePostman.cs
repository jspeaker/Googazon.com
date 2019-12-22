using System.Threading.Tasks;
using GoogazonActivities.Messaging.Strategies;

namespace GoogazonActivities.Unit.Tests.Fakes
{
    public class FakePostman : IServiceBusPostman
    {
        public int CallCount;

        public Task SendAsync()
        {
            CallCount++;
            return Task.CompletedTask;
        }
    }
}