using GoogazonActivities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GoogazonFunctions.Functions.SignalR
{
    public static class SignalRFunctions
    {
        [FunctionName("Negotiate")]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest request,
            [SignalRConnectionInfo(HubName = "googazon", ConnectionStringSetting = "AzureSignalRConnectionString")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName("CustomerServiceContactResult")]
        public static async Task Run(
            [ServiceBusTrigger("result", Connection = "ServiceBusConnectionString")] string message,
            [SignalR(HubName = "googazon")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            TopicNeedMessage messageObject = JsonConvert.DeserializeObject<TopicNeedMessage>(message);

            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    ConnectionId = messageObject.ClientIdentifier(),
                    Target = "customerServiceNeed",
                    Arguments = new object[] { message }
                });
        }
    }

}
