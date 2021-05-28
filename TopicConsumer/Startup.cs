using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: WebJobsStartup(typeof(TopicConsumer.Startup))]
namespace TopicConsumer
{

    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            try
            {
                builder.Services.AddHttpClient("ApiTest", c =>
                {
                    c.BaseAddress = new Uri("http://localhost:7071/api/");
                });
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error in Startup.cs:", e);
            }
        }
    }
}
