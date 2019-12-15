using System.Threading.Tasks;

namespace GoogazonFunctions.Messaging.Strategies
{
    public interface IServiceBusPostman
    {
        Task SendAsync();
    }
}