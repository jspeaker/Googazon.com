using System.Threading.Tasks;

namespace GoogazonActivities.Messaging.Strategies
{
    public interface IServiceBusPostman
    {
        Task SendAsync();
    }
}