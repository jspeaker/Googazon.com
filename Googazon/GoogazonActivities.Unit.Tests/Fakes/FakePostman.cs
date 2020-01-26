using GoogazonActivities.Messaging.Strategies;
using System.Threading.Tasks;

namespace GoogazonActivities.Unit.Tests.Fakes
{
    public class FakePostman : IServiceBusPostman
    {
        private readonly bool _shouldExecute;
        private readonly IServiceBusPostman _nextPostman;
        public int CallCount;

        public FakePostman() : this(true) { }

        public FakePostman(bool shouldExecute) : this(shouldExecute, new ServiceBusUselessPostman()) { }

        public FakePostman(bool shouldExecute, IServiceBusPostman nextPostman)
        {
            _shouldExecute = shouldExecute;
            _nextPostman = nextPostman;
        }

        public Task SendAsync()
        {
            if (_shouldExecute) CallCount++;

            _nextPostman.SendAsync();
            return Task.CompletedTask;
        }
    }
}