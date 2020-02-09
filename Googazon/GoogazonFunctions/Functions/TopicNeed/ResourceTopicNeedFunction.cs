using GoogazonActivities.Models;
using GoogazonActivities.TopicNeedActivity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading.Tasks;
using System.Web.Http;

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
                await new TopicNeedActivity(new ConnectionId(request.Headers), resourceIdentifier, topic, new ResourceBasedNeed(resource, need)).ExpressNeed();
                return new OkResult();
            }
            catch (Exception)
            {
                return new InternalServerErrorResult();
            }
        }
    }
}
