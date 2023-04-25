namespace SPACE_Shuttle_Launch.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SPACE_Shuttle_Launch.Models.Contracts;

    public class WeatherForecast : IWeatherForecast
    {
        List<IDayForecast> timeline;

        public WeatherForecast()
        {
            this.timeline = new List<IDayForecast>();
        }

        public ICollection<IDayForecast> Timeline => this.timeline;

        public double AverageTemperature
        {
            get => this.Timeline.Average(x => x.Temperature);
        }

        public double MinTemperature
        {
            get => this.Timeline.Min(x => x.Temperature);
        }

        public double MaxTemperature
        {
            get => this.Timeline.Max(x => x.Temperature);
        }

        public double MedianTemperature
        {
            get
            {
                var arr = this.Timeline.OrderBy(x => x.Temperature).Select(x => x.Temperature).ToArray();
                if (arr.Length % 2 == 0)
                {
                    return (arr[arr.Length / 2 - 1] + arr[arr.Length / 2]) / 2;
                }
                else
                {
                    return arr[this.Timeline.Count / 2];
                }
            }
        }

        public double AverageWind
        {
            get => this.Timeline.Average(x => x.Wind);
        }

        public double MinWind
        {
            get => this.Timeline.Min(x => x.Wind);
        }

        public double MaxWind
        {
            get => this.Timeline.Max(x => x.Wind);
        }

        public double MedianWind
        {
            get
            {
                var arr = this.Timeline.OrderBy(x => x.Wind).Select(x => x.Wind).ToArray();
                if (arr.Length % 2 == 0)
                {
                    return (arr[arr.Length / 2 - 1] + arr[arr.Length / 2]) / 2;
                }
                else
                {
                    return arr[this.Timeline.Count / 2];
                }
            }
        }

        public double AverageHumidity
        {
            get => this.Timeline.Average(x => x.Humidity);
        }

        public double MinHumidity
        {
            get => this.Timeline.Min(x => x.Humidity);
        }

        public double MaxHumidity
        {
            get => this.Timeline.Max(x => x.Humidity);
        }

        public double MedianHumidity
        {
            get
            {
                var arr = this.Timeline.OrderBy(x => x.Humidity).Select(x => x.Humidity).ToArray();
                if (arr.Length % 2 == 0)
                {
                    return (arr[arr.Length / 2 - 1] + arr[arr.Length / 2]) / 2;
                }
                else
                {
                    return arr[this.Timeline.Count / 2];
                }
            }
        }

        public double AveragePrecipitation
        {
            get => this.Timeline.Average(x => x.Precipitation);
        }

        public double MinPrecipitation
        {
            get => this.Timeline.Min(x => x.Precipitation);
        }

        public double MaxPrecipitation
        {
            get => this.Timeline.Max(x => x.Precipitation);
        }

        public double MedianPrecipitation
        {
            get
            {
                var arr = this.Timeline.OrderBy(x => x.Precipitation).Select(x => x.Precipitation).ToArray();
                if (arr.Length % 2 == 0)
                {
                    return (arr[arr.Length / 2 - 1] + arr[arr.Length / 2]) / 2;
                }
                else
                {
                    return arr[this.Timeline.Count / 2];
                }
            }
        }

        public void AddDay(IDayForecast day)
        {
            this.Timeline.Add(day);
        }

        public IDayForecast CalculateMostAppropriateDayForLaunch()
        {
            IDayForecast mostAppropriateDayForLaunch =
                this.Timeline.OrderByDescending(x => x.CalculateAppropriateScore()).First();
            return mostAppropriateDayForLaunch;
        }

        public string CreateCSV()
        {
            var mostAppropriateDayForLaunch = this.CalculateMostAppropriateDayForLaunch();

            var csv = new StringBuilder();
            csv.AppendLine($"Aggregate function/Parameter;Average value;Max value;Min value;Median value;Most appropriate launch day: {mostAppropriateDayForLaunch.Date.ToString("MM/dd/yyyy")}");
            csv.AppendLine($"Temperature (C);{Math.Round(AverageTemperature)};{Math.Round(MaxTemperature)};{Math.Round(MinTemperature)};{Math.Round(MedianTemperature)};{mostAppropriateDayForLaunch.Temperature}");
            csv.AppendLine($"Wind (m/s);{Math.Round(AverageWind)};{Math.Round(MaxWind)};{Math.Round(MinWind)};{Math.Round(MedianWind)};{mostAppropriateDayForLaunch.Wind}");
            csv.AppendLine($"Humidity (%);{Math.Round(AverageHumidity)};{Math.Round(MaxHumidity)};{Math.Round(MinHumidity)};{Math.Round(MedianHumidity)};{mostAppropriateDayForLaunch.Humidity}");
            csv.AppendLine($"Precipitation (%);{Math.Round(AveragePrecipitation)};{Math.Round(MaxPrecipitation)};{Math.Round(MinPrecipitation)};{Math.Round(MedianPrecipitation)};{mostAppropriateDayForLaunch.Precipitation}");
            csv.AppendLine($"Lightning;;;;;{mostAppropriateDayForLaunch.Lightning}");
            csv.AppendLine($"Clouds;;;;;{mostAppropriateDayForLaunch.Clouds}");

            return csv.ToString();
        }
    }
}