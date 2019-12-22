using System.Threading.Tasks;

namespace GoogazonActivities.Messaging.Strategies
{
    public class ServiceBusUselessPostman : IServiceBusPostman
    {
        public Task SendAsync() => Task.CompletedTask;
    }
}