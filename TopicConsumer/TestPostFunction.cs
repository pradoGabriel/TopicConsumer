using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Web.Http;

namespace TopicConsumer
{
    public class TestPostFunction
    {
        [FunctionName("TestPost")]
        public static IActionResult POST([HttpTrigger(AuthorizationLevel.Anonymous, "put", "post", Route = null)] HttpRequestMessage req, ILogger log)
        {
            try
            {
                log.LogInformation($"Requisição recebida");

                return new OkResult();
            }
            catch (Exception ex)
            {
                log.LogInformation($"Esso na requisição: {ex}");
                return new InternalServerErrorResult();
            }
        }
    }
}
