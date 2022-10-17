using System;
using Azure;
using Azure.Data.Tables;

namespace SteveSyrell.PurpleAirDashboard.Api.Models
{
    public abstract record EntityBase : ITableEntity
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public ETag ETag { get; set; } = default!;

        public DateTimeOffset? Timestamp { get; set; } = default!;
    }
}