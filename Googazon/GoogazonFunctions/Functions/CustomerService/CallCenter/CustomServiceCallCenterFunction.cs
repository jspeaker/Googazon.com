using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;

namespace GoogazonFunctions.Functions.CustomerService.CallCenter
{
    public static class CustomServiceCallCenterFunction
    {
        [FunctionName("CustomServiceCallCenter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customer/{customerId}/customerservice/callcenter")]
            HttpRequest request,
            string customerId)
        {
            try
            {
                dynamic valueAsync = await new CustomerServiceCallCenterActivity(customerId).ValueAsync();
                return new OkObjectResult(valueAsync);
            }
            catch (Exception)
            {
                return new InternalServerErrorResult();
            }
        }
    }
}
