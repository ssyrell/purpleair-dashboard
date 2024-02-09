using System;
using System.IO;
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
    public static class SensorData
    {
        /// <summary>
        /// Fetches data for the specified sensor
        /// </summary>
        /// <param name="req">The request object</param>
        /// <param name="sensorId">The ID of the sensor to get data for.</param>
        /// <param name="averagingPeriod">The averaging period of the data to fetch. Options are 0 (real-time), 10 (ten minutes), or 60 (one hour)</param>
        /// <param name="rowCount">The number of rows of data to fetch</param>
        /// <param name="log">Logger.</param>
        /// <returns>Fetched data.</returns>
        [FunctionName("SensorData")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "sensorData/{sensorId}/{averagingPeriod:int}/{rowCount:int}")] HttpRequest req,
            string sensorId,
            int averagingPeriod,
            int rowCount,
            ILogger log)
        {
            log.LogInformation($"[SensorHistory] fetching {rowCount} rows of history for {sensorId} averaged by {averagingPeriod} minutes");

            TableServiceClient tableServiceClient = new TableServiceClient(Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_CONNECTION_STRING"));
            TableClient tableClient = tableServiceClient.GetTableClient(GetTableName(averagingPeriod));

            if (averagingPeriod == 0)
            {
                var query = tableClient.QueryAsync<RealTimeTableEntity>(x => x.PartitionKey == sensorId, maxPerPage: rowCount);
                return new OkObjectResult(await GetHistoryRowsAsync(query, rowCount, log));
            }
            else
            {
                var query = tableClient.QueryAsync<RealTimeTableEntity>(x => x.PartitionKey == sensorId, maxPerPage: rowCount);
                return new OkObjectResult(await GetHistoryRowsAsync(query, rowCount, log));
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

        private static async Task<List<T>> GetHistoryRowsAsync<T>(AsyncPageable<T> query, int rowCount, ILogger log)
        {
            List<T> rows = new();
            await foreach (var row in query)
            {
                log.LogInformation("[GetHistoryRowsAsync] adding row");
                rows.Add(row);
                if (rows.Count == rowCount)
                {
                    break;
                }
            }

            // Reverse data so that rows are oldest -> newest
            rows.Reverse();
            return rows;
        }
    }
}
