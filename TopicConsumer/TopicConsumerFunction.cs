using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TopicConsumer.DTO;

namespace TopicConsumer
{
    public class TopicConsumerFunction
    {
        #region Properties

        private readonly IHttpClientFactory _clientFactory;

        #endregion

        #region Constructor
        public TopicConsumerFunction(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        #endregion


        #region Function
        [FunctionName("TopicConsumerFunction")]
        public async Task RunAsync([ServiceBusTrigger("topic-db-sync", "subscription-db-sync", Connection = "ServiceBusConnectionString")] string message, ILogger log)
        {
            try
            {
                log.LogInformation($"Mensagem recebida: {message}");

                var alteracoes = JsonSerializer.Deserialize<List<ChangeTrackingDto>>(message);
                var processamento = await SimularProcessamento(alteracoes);

                var client = _clientFactory.CreateClient("ApiTest");
                var itemJson = new StringContent(
                    JsonSerializer.Serialize(processamento),
                    Encoding.UTF8,
                    "application/json");

                using var response =
                    await client.PostAsync("TestPost", itemJson);

                if (response.IsSuccessStatusCode)
                    log.LogInformation($"REQUEST HTTP OK");
                else
                    log.LogInformation($"REQUEST ERROR: {response.StatusCode}");

            }
            catch (Exception ex)
            {
                log.LogInformation($"Esso na requisição: {ex}");
            }
        }

        #endregion

        #region Private Methods
        private async Task<List<ChangeTrackingDto>> SimularProcessamento(List<ChangeTrackingDto> alteracoes)
        {
            await Task.Delay(5000);
            return alteracoes;
        }
        #endregion
    }
}
