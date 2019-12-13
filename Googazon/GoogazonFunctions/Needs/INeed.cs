using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;

namespace GoogazonFunctions.Needs
{
    public interface INeed
    {
        Task<EventData> SendAsync();
    }
}