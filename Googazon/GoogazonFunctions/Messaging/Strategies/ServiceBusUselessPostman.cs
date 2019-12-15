using System.Threading.Tasks;

namespace GoogazonFunctions.Messaging.Strategies
{
    public class ServiceBusUselessPostman : IServiceBusPostman
    {
        public Task SendAsync() => Task.CompletedTask;
    }
}