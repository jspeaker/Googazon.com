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
    public static class CustomerTopicNeedFunction
    {
        [FunctionName("CustomerTopicNeed")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customer/{customerId}/{topic}/{need}")] HttpRequest request,
            string customerId, string topic, string need)
        {
            try
            {
                await new TopicNeedActivity(customerId, topic, need).ExpressNeed();
                return new OkResult();
            }
            catch (Exception)
            {
                return new InternalServerErrorResult();
            }
        }
    }
}
