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
    public class AveragedHistory
    {
        [FunctionName("AveragedHistory")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "history/averaged/{averagingPeriod:int}/{sensorId}/{historyInMinutes:int}")] HttpRequest req,
            int averagingPeriod, string sensorId, int historyInMinutes, ILogger log)
        {
            if (averagingPeriod != 10 && averagingPeriod != 60)
            {
                log.LogError($"[AveragedHistory] Received invalid averaging period of {averagingPeriod}");
                return new BadRequestResult();
            }

            log.LogInformation("[AveragedHistory] Processing history request");

            log.LogInformation($"[AveragedHistory] Fetching {historyInMinutes} minutes of {averagingPeriod}-minute-averaged history for sensor {sensorId}");
            TableServiceClient tableServiceClient = new TableServiceClient(Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_CONNECTION_STRING"));

            var tableNameSetting = averagingPeriod == 10 ? "STORAGE_ACCOUNT_TEN_MINUTE_AVERAGES_TABLE_NAME" : "STORAGE_ACCOUNT_HOURLY_AVERAGES_TABLE_NAME";
            TableClient tableClient = tableServiceClient.GetTableClient(Environment.GetEnvironmentVariable(tableNameSetting));

            var query = tableClient.QueryAsync<AverageTableEntity>(x => x.PartitionKey == sensorId && x.Timestamp >= DateTimeOffset.UtcNow.AddMinutes(historyInMinutes * -1));
            var results = new List<AverageTableEntity>();
            await foreach (var match in query)
            {
                results.Add(match);
            }

            log.LogInformation($"[AveragedHistory] Found {results.Count} matching records");

            return new OkObjectResult(results);
        }
    }
}