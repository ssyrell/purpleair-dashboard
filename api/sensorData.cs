using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Data.Tables;
using Azure;


namespace SteveSyrell.PurpleAirDashboard.Api
{
    public static class SensorData
    {
        [FunctionName("sensorData")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            TableServiceClient tableServiceClient = new TableServiceClient(Environment.GetEnvironmentVariable("TableStorageConnectionString"));
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: "history");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var dataEntry = new SensorDataEntry
            {
                PartitionKey = "12345",
                RowKey = DateTimeOffset.UtcNow.ToString("yyyyMMdd-HHmmss"),
                Json = requestBody
            };

            await tableClient.AddEntityAsync<SensorDataEntry>(dataEntry);

            return new OkObjectResult("Sensor Data Added");
        }
    }

    public record SensorDataEntry : ITableEntity
    {
        public string RowKey { get; set; } = default!;

        public string PartitionKey { get; set; } = default!;

        public string Json { get; set; }

        public ETag ETag { get; set; } = default!;

        public DateTimeOffset? Timestamp { get; set; } = default!;
    }
}
