using System;
using System.Text.Json.Serialization;
using Azure;
using Azure.Data.Tables;


namespace SteveSyrell.PurpleAirDashboard.Api.Models
{
    public record RealTimeTableEntity : ITableEntity
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public ETag ETag { get; set; } = default!;

        public DateTimeOffset? Timestamp { get; set; } = default!;

        public string SensorId { get; set; }

        public string DateTime { get; set; }

        public string Geo { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Place { get; set; }

        public double CurrentTempFahrenheit { get; set; }

        public double CurrentHumidity { get; set; }

        public double CurrentDewpointFahrenheit { get; set; }

        public double Pressure { get; set; }

        public double CurrentTempFahrenheit680 { get; set; }

        public double CurrentHumidity680 { get; set; }

        public double CurrentDewpointFahrenheit680 { get; set; }

        public double Pressure680 { get; set; }

        public double Gas680 { get; set; }

        public string ChannelA_Pm25AqiColor { get; set; }

        public int ChannelA_Pm25Aqi { get; set; }

        public double ChannelA_Pm10CF1 { get; set; }

        public double ChannelA_Pm25CF1 { get; set; }

        public double ChannelA_Pm100CF1 { get; set; }

        public double ChannelA_P03Um { get; set; }

        public double ChannelA_P05Um { get; set; }

        public double ChannelA_P10Um { get; set; }

        public double ChannelA_P25Um { get; set; }

        public double ChannelA_P50Um { get; set; }

        public double ChannelA_P100Um { get; set; }

        public double ChannelA_Pm10Atm { get; set; }

        public double ChannelA_Pm25Atm { get; set; }

        public double ChannelA_Pm100Atm { get; set; }

        public string ChannelB_Pm25AqiColor { get; set; }

        public int ChannelB_Pm25Aqi { get; set; }

        public double ChannelB_Pm10CF1 { get; set; }

        public double ChannelB_Pm25CF1 { get; set; }

        public double ChannelB_Pm100CF1 { get; set; }

        public double ChannelB_P03Um { get; set; }

        public double ChannelB_P05Um { get; set; }

        public double ChannelB_P10Um { get; set; }

        public double ChannelB_P25Um { get; set; }

        public double ChannelB_P50Um { get; set; }

        public double ChannelB_P100Um { get; set; }

        public double ChannelB_Pm10Atm { get; set; }

        public double ChannelB_Pm25Atm { get; set; }

        public double ChannelB_Pm100Atm { get; set; }
    }
}