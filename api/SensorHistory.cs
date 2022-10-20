using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SteveSyrell.PurpleAirDashboard.Api.Models;

namespace SteveSyrell.PurpleAirDashboard.Api
{
    public static class SensorHistory
    {
        /// <summary>
        /// Fetches history for the specified sensor
        /// </summary>
        /// <param name="req">The request object</param>
        /// <param name="sensorId">The ID of the sensor to get history for.</param>
        /// <param name="averagingPeriod">The averaging period of the history to fetch. Options are 0 (real-time), 10 (ten minutes), or 60 (one hour)</param>
        /// <param name="rowCount">The number of rows of history to fetch</param>
        /// <param name="log">Logger.</param>
        /// <returns>Fetched history.</returns>
        [FunctionName("SensorHistory")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "history/{sensorId}/{averagingPeriod:int}/{rowCount:int}")] HttpRequest req,
            string sensorId, int averagingPeriod, int rowCount, ILogger log)
        {
            log.LogInformation($"[SensorHistory] fetching {rowCount} rows of history for {sensorId} averaged by {averagingPeriod} minutes");

            TableServiceClient tableServiceClient = new TableServiceClient(Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_CONNECTION_STRING"));
            TableClient tableClient = tableServiceClient.GetTableClient(GetTableName(averagingPeriod));

            if (averagingPeriod == 0)
            {
                var query = tableClient.QueryAsync<RealTimeTableEntity>(x => x.PartitionKey == sensorId);
                return new OkObjectResult(await GetHistoryRowsAsync(query, rowCount));
            }
            else
            {
                var query = tableClient.QueryAsync<AverageTableEntity>(x => x.PartitionKey == sensorId);
                return new OkObjectResult(await GetHistoryRowsAsync(query, rowCount));
            }
        }

        private static string GetTableName(int averagingPeriod)
        {
            switch (averagingPeriod)
            {
                case 0:
                    return Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_REAL_TIME_TABLE_NAME");
                case 10:
                    return Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_TEN_MINUTE_AVERAGES_TABLE_NAME");
                case 60:
                    return Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_HOURLY_AVERAGES_TABLE_NAME");
                default:
                    throw new ArgumentOutOfRangeException(nameof(averagingPeriod), $"{averagingPeriod} is not a supported averaging period");
            }
        }

        private static async Task<List<T>> GetHistoryRowsAsync<T>(AsyncPageable<T> query, int rowCount)
        {
            List<T> rows = new();
            await foreach (var row in query)
            {
                rows.Add(row);
                if (rows.Count == rowCount)
                {
                    break;
                }
            }

            return rows;
        }
    }
}
