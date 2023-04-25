namespace SPACE_Shuttle_Launch.Models.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface IWeatherForecast
    {
        ICollection<IDayForecast> Timeline { get; }

        double AverageTemperature { get; }

        double MinTemperature { get; }

        double MaxTemperature { get; }

        double MedianTemperature { get; }

        double AverageWind { get; }

        double MinWind { get; }

        double MaxWind { get; }

        double MedianWind { get; }

        double AverageHumidity { get; }

        double MinHumidity { get; }

        double MaxHumidity { get; }

        double MedianHumidity { get; }

        double AveragePrecipitation { get; }

        double MinPrecipitation { get; }

        double MaxPrecipitation { get; }

        double MedianPrecipitation { get; }

        void AddDay(IDayForecast day);

        IDayForecast CalculateMostAppropriateDayForLaunch();

        string CreateCSV();
    }
}