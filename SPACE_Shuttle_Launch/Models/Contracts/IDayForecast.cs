namespace SPACE_Shuttle_Launch.Models.Contracts
{
    using System;
    using SPACE_Shuttle_Launch.Models.Enums;

    public interface IDayForecast
    {
        DateTime Date { get; }

        double Temperature { get; }

        double Wind { get; }

        double Humidity { get; }

        double Precipitation { get; }

        bool Lightning { get; }

        Clouds Clouds { get; }

        bool IsAppropriateDayForLaunch();

        double CalculateAppropriateScore();
    }
}