namespace SteveSyrell.PurpleAirDashboard.Api.Models
{
    public record AverageTableEntity : EntityBase
    {
        public double TempFahrenheit { get; set; }

        public double Humidity { get; set; }

        public double DewpointFahrenheit { get; set; }

        public double Pressure { get; set; }

        public double TempFahrenheit680 { get; set; }

        public double Humidity680 { get; set; }

        public double DewpointFahrenheit680 { get; set; }

        public double Pressure680 { get; set; }

        public double Gas680 { get; set; }

        public int Pm25Aqi { get; set; }

        public double Pm10CF1 { get; set; }

        public double Pm25CF1 { get; set; }

        public double Pm100CF1 { get; set; }

        public double P03Um { get; set; }

        public double P05Um { get; set; }

        public double P10Um { get; set; }

        public double P25Um { get; set; }

        public double P50Um { get; set; }

        public double P100Um { get; set; }

        public double Pm10Atm { get; set; }

        public double Pm25Atm { get; set; }

        public double Pm100Atm { get; set; }
    }
}