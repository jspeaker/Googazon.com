using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System.Threading.Tasks;

namespace GoogazonFunctions.Functions.SignalR
{
    public static class SignalRFunctions
    {
        [FunctionName("Negotiate")]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest request,
            [SignalRConnectionInfo(HubName = "googazon", ConnectionStringSetting = "AzureSignalRConnectionString")] SignalRConnectionInfo connectionInfo) =>
            connectionInfo;

        [FunctionName("CustomerServiceContactResult")]
        public static async Task Run(
            [ServiceBusTrigger("result", Connection = "ServiceBusConnectionString")] string message,
            [SignalR(HubName = "googazon")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "customerServiceNeed",
                    Arguments = new object[] { message }
                });
        }
    }

}
