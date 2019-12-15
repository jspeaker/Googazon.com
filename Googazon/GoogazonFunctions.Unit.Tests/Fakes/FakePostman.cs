using System.Threading.Tasks;
using GoogazonFunctions.Messaging.Strategies;

namespace GoogazonFunctions.Unit.Tests.Fakes
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