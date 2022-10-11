using System;
using System.Text.Json.Serialization;
using Azure;
using Azure.Data.Tables;


namespace SteveSyrell.PurpleAirDashboard.Api.Models
{
    public record SensorDataTableEntity : ITableEntity
    {
        public string PartitionKey 
        { 
            get => this.SensorId; 
            set => this.SensorId = value;
        }

        public string RowKey 
        { 
            get => DateTimeOffset.Parse(this.DateTime).ToString("yyyyMMddHHmmss");
            set => this.DateTime = value;
        }
        public ETag ETag { get; set; } = default!;

        public DateTimeOffset? Timestamp { get; set; } = default!;

        public string SensorId {get; set;}
        public string DateTime {get; set;}
        public string Geo {get; set;}

        [JsonPropertyName("Mem")]
        public int Mem {get; set;}

        [JsonPropertyName("memfrag")]
        public int MemFrag {get; set;}

        [JsonPropertyName("memfb")]
        public int MemFB {get; set;}

        [JsonPropertyName("memcs")]
        public int MemCS {get; set;}

        public int Id {get; set;}

        [JsonPropertyName("lat")]
        public double Latitude {get; set;}

        [JsonPropertyName("lon")]
        public double Longitude {get; set;}

        public double Adc {get; set;}

        [JsonPropertyName("loggingrate")]
        public int LoggingRate {get; set;}

        [JsonPropertyName("place")]
        public string Place {get; set;}

        [JsonPropertyName("version")]
        public string Version {get; set;}

        [JsonPropertyName("uptime")]
        public long Uptime {get; set;}

        [JsonPropertyName("rssi")]
        public int Rssi {get; set;}

        [JsonPropertyName("period")]
        public int Period {get; set;}

        [JsonPropertyName("httpsuccess")]
        public int RequestSuccessCount {get; set;}

        [JsonPropertyName("httpsends")]
        public int RequestSendCount {get; set;}

        [JsonPropertyName("hardwareversion")]
        public string HardwareVersion {get; set;}

        [JsonPropertyName("hardwarediscovered")]
        public string HardwareDiscovered {get; set;}

        [JsonPropertyName("current_temp_f")]
        public double CurrentTempFahrenheit {get; set;}

        [JsonPropertyName("current_humidity")]
        public double CurrentHumidity {get; set;}

        [JsonPropertyName("current_dewpoint_f")]
        public double CurrentDewpointFahrenheit {get; set;}

        [JsonPropertyName("pressure")]
        public double Pressure {get; set;}

        [JsonPropertyName("current_temp_f_680")]
        public double CurrentTempFahrenheit680 {get; set;}

        [JsonPropertyName("current_humidity_680")]
        public double CurrentHumidity680 {get; set;}

        [JsonPropertyName("current_dewpoint_f_680")]
        public double CurrentDewpointFahrenheit680 {get; set;}

        [JsonPropertyName("pressure_680")]
        public double Pressure680 {get; set;}

        [JsonPropertyName("gas_680")]
        public double Gas680 {get; set;}

        [JsonPropertyName("p25aqic")]
        public string ChannelA_Pm25AqiColor {get; set;}

        [JsonPropertyName("pm2.5_aqi")]
        public int ChannelA_Pm25Aqi {get; set;}

        [JsonPropertyName("pm1_0_cf_1")]
        public double ChannelA_Pm10CF1 {get; set;}

        [JsonPropertyName("pm2_5_cf_1")]
        public double ChannelA_Pm25CF1 {get; set;}

        [JsonPropertyName("pm10_0_cf_1")]
        public double ChannelA_Pm100CF1 {get; set;}

        [JsonPropertyName("p_0_3_um")]
        public double ChannelA_P03Um {get; set;}

        [JsonPropertyName("p_0_5_um")]
        public double ChannelA_P05Um {get; set;}

        [JsonPropertyName("p_1_0_um")]
        public double ChannelA_P10Um {get; set;}

        [JsonPropertyName("p_2_5_um")]
        public double ChannelA_P25Um {get; set;}

        [JsonPropertyName("p_5_0_um")]
        public double ChannelA_P50Um {get; set;}

        [JsonPropertyName("p_10_0_um")]
        public double ChannelA_P100Um {get; set;}

        [JsonPropertyName("pm1_0_atm")]
        public double ChannelA_Pm10Atm {get; set;}

        [JsonPropertyName("pm2_5_atm")]
        public double ChannelA_Pm25Atm {get; set;}

        [JsonPropertyName("pm10_0_atm")]
        public double ChannelA_Pm100Atm {get; set;}

        [JsonPropertyName("p25aqic_b")]
        public string ChannelB_Pm25AqiColor {get; set;}

        [JsonPropertyName("pm2.5_aqi_b")]
        public int ChannelB_Pm25Aqi {get; set;}

        [JsonPropertyName("pm1_0_cf_1_b")]
        public double ChannelB_Pm10CF1 {get; set;}

        [JsonPropertyName("pm2_5_cf_1_b")]
        public double ChannelB_Pm25CF1 {get; set;}

        [JsonPropertyName("pm10_0_cf_1_b")]
        public double ChannelB_Pm100CF1 {get; set;}

        [JsonPropertyName("p_0_3_um_b")]
        public double ChannelB_P03Um {get; set;}

        [JsonPropertyName("p_0_5_um_b")]
        public double ChannelB_P05Um {get; set;}

        [JsonPropertyName("p_1_0_um_b")]
        public double ChannelB_P10Um {get; set;}

        [JsonPropertyName("p_2_5_um_b")]
        public double ChannelB_P25Um {get; set;}

        [JsonPropertyName("p_5_0_um_b")]
        public double ChannelB_P50Um {get; set;}

        [JsonPropertyName("p_10_0_um_b")]
        public double ChannelB_P100Um {get; set;}

        [JsonPropertyName("pm1_0_atm_b")]
        public double ChannelB_Pm10Atm {get; set;}

        [JsonPropertyName("pm2_5_atm_b")]
        public double ChannelB_Pm25Atm {get; set;}

        [JsonPropertyName("pm10_0_atm_b")]
        public double ChannelB_Pm100Atm {get; set;}

        [JsonPropertyName("pa_latency")]
        public int PaLatency {get; set;}

        [JsonPropertyName("response")]
        public int ChannelA_Response {get; set;}

        [JsonPropertyName("response_date")]
        public long ChannelA_ResponseDate {get; set;}

        [JsonPropertyName("latency")]
        public int ChannelA_Latency {get; set;}

        [JsonPropertyName("key1_response")]
        public int ChannelA_Key1Response {get; set;}

        [JsonPropertyName("key1_response_date")]
        public long ChannelA_Key1ResponseDate {get; set;}

        [JsonPropertyName("key1_count")]
        public long ChannelA_Key1Count {get; set;}

        [JsonPropertyName("ts_latency")]
        public int ChannelA_TsLatency {get; set;}

        [JsonPropertyName("key2_response")]
        public int ChannelA_Key2Response {get; set;}

        [JsonPropertyName("key2_response_date")]
        public long ChannelA_Key2ResponseDate {get; set;}

        [JsonPropertyName("key2_count")]
        public long ChannelA_Key2Count {get; set;}

        [JsonPropertyName("ts_s_latency")]
        public int ChannelA_TssLatency {get; set;}

        [JsonPropertyName("response_b")]
        public int ChannelB_Response {get; set;}

        [JsonPropertyName("response_date_b")]
        public long ChannelB_ResponseDate {get; set;}

        [JsonPropertyName("latency_b")]
        public int ChannelB_Latency {get; set;}

        [JsonPropertyName("key1_response_b")]
        public int ChannelB_Key1Response {get; set;}

        [JsonPropertyName("key1_response_date_b")]
        public long ChannelB_Key1ResponseDate {get; set;}

        [JsonPropertyName("key1_count_b")]
        public long ChannelB_Key1Count {get; set;}

        [JsonPropertyName("ts_latency_b")]
        public int ChannelB_TsLatency {get; set;}

        [JsonPropertyName("key2_response_b")]
        public int ChannelB_Key2Response {get; set;}

        [JsonPropertyName("key2_response_date_b")]
        public long ChannelB_Key2ResponseDate {get; set;}

        [JsonPropertyName("key2_count_b")]
        public long ChannelB_Key2Count {get; set;}

        [JsonPropertyName("ts_s_latency_b")]
        public int ChannelB_TssLatency {get; set;}

        [JsonPropertyName("wlstate")]
        public string WLanState {get; set;}

        [JsonPropertyName("status_0")]
        public int Status0 {get; set;}

        [JsonPropertyName("status_1")]
        public int Status1 {get; set;}

        [JsonPropertyName("status_2")]
        public int Status2 {get; set;}

        [JsonPropertyName("status_3")]
        public int Status3 {get; set;}

        [JsonPropertyName("status_4")]
        public int Status4 {get; set;}

        [JsonPropertyName("status_5")]
        public int Status5 {get; set;}

        [JsonPropertyName("status_6")]
        public int Status6 {get; set;}

        [JsonPropertyName("status_7")]
        public int Status7 {get; set;}

        [JsonPropertyName("status_8")]
        public int Status8 {get; set;}

        [JsonPropertyName("status_9")]
        public int Status9 {get; set;}

        [JsonPropertyName("status_10")]
        public int Status10 {get; set;}

        [JsonPropertyName("ssid")]
        public string SSID {get; set;}
    }
}