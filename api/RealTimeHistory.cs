using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SteveSyrell.PurpleAirDashboard.Api.Models;
using Azure.Data.Tables;

namespace SteveSyrell.PurpleAirDashboard.Api
{
    public static class RealTimeHistory
    {
        [FunctionName("RealTimeHistory")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "history/realtime/{sensorId}/{historyInMinutes:int}")] HttpRequest req,
            string sensorId, int historyInMinutes, ILogger log)
        {
            log.LogInformation("[RealTimeHistory] Processing history request");

            log.LogInformation($"[RealTimeHistory] Fetching {historyInMinutes} minutes of history for sensor {sensorId}");
            TableServiceClient tableServiceClient = new TableServiceClient(Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_CONNECTION_STRING"));
            TableClient tableClient = tableServiceClient.GetTableClient(Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_REAL_TIME_TABLE_NAME"));

            var query = tableClient.QueryAsync<SensorDataTableEntity>(x => x.PartitionKey == sensorId && x.Timestamp >= DateTimeOffset.UtcNow.AddMinutes(historyInMinutes * -1));
            var results = new List<SensorDataTableEntity>();
            await foreach (var match in query)
            {
                results.Add(match);
            }

            log.LogInformation($"[RealTimeHistory] Found {results.Count} matching records");

            return new OkObjectResult(results);
        }
    }
}
