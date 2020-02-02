using GoogazonActivities.TopicNeedActivity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GoogazonActivities.Models;

namespace GoogazonFunctions.Functions.TopicNeed
{
    public static class ResourceTopicNeedFunction
    {
        [FunctionName("ResourceTopicNeed")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{resource}/{resourceIdentifier}/{topic}/{need}")] HttpRequest request,
            string resource, string resourceIdentifier, string topic, string need)
        {
            try
            {
                string connectionId = request.Headers["X-ConnectionId"].First();
                await new TopicNeedActivity(connectionId, resourceIdentifier, topic, new ResourceBasedNeed(resource, need)).ExpressNeed();
                return new OkResult();
            }
            catch (Exception)
            {
                return new InternalServerErrorResult();
            }
        }
    }
}
