namespace SPACE_Shuttle_Launch.Models
{
    using System;
    using SPACE_Shuttle_Launch.Models.Contracts;
    using SPACE_Shuttle_Launch.Models.Enums;
    using SPACE_Shuttle_Launch.Utilities.Messages;

    public class DayForecast : IDayForecast
    {
        DateTime date;
        double temperature;
        double wind;
        double humidity;
        double precipitation;
        bool lightning;
        Clouds clouds;

        public DayForecast(DateTime date, double temperature, double wind, double humidity, double precipitation, bool lightning, Clouds clouds)
        {
            this.Date = date;
            this.Temperature = temperature;
            this.Wind = wind;
            this.Humidity = humidity;
            this.Precipitation = precipitation;
            this.Lightning = lightning;
            this.Clouds = clouds;
        }

        public DateTime Date
        {
            get => this.date;
            private set => date = value;
        }

        public double Temperature
        {
            get => this.temperature;
            private set => temperature = value;
        }

        public double Wind
        {
            get => this.wind;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.NegativeWind);
                }

                this.wind = value;
            }
        }

        public double Humidity
        {
            get => this.humidity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.NegativeHumidity);
                }

                this.humidity = value;
            }
        }

        public double Precipitation
        {
            get => this.precipitation;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.NegativePrecipitation);
                }

                this.precipitation = value;
            }
        }

        public bool Lightning
        {
            get => this.lightning;
            private set => lightning = value;
        }

        public Clouds Clouds
        {
            get => this.clouds;
            private set => clouds = value;
        }

        public bool IsAppropriateDayForLaunch()
        {
            if (this.Temperature <= 2 || this.Temperature >= 31)
                return false;

            if (this.Wind > 10)
                return false;

            if (this.Humidity >= 60)
                return false;

            if (this.Precipitation > 0)
                return false;

            if (this.Lightning)
                return false;

            if (this.Clouds == Clouds.Cumulus || this.Clouds == Clouds.Nimbus)
                return false;

            return true;
        }

        public double CalculateAppropriateScore()
        {
            if(!IsAppropriateDayForLaunch())
            {
                return double.MinValue;
            }

            double score = 0;

            score += 10 - this.Wind; //slower wind = better score

            score += 60 - this.Humidity; //less humidity = better score

            return score;
        }
    }
}