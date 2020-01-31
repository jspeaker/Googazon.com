using Microsoft.Azure.ServiceBus;
using System.Text;

namespace Googazon.Library.Extensions
{
    public static class ServiceBusMessageExtensions
    {
        public static string BodyAsString(this Message value) => Encoding.UTF8.GetString(value.Body);
    }
}