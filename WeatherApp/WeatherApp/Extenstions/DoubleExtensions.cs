using System;
using WeatherApp.Models;

namespace WeatherApp.Extenstions;
public static class DoubleExtensions
{
    /// <exception cref="ArgumentOutOfRangeException">if source is not in the range 0 to 1</exception>
    public static MoonPhase GetMoonPhase(this double source)
    {
        return source == 0.0 || source == 1.0
                    ? MoonPhase.New_Moon
                    : source < 0.25
                    ? MoonPhase.Waxing_Crescent
                    : source == 0.25
                    ? MoonPhase.First_Quarter
                    : source < 0.5
                    ? MoonPhase.Waxing_Gibbous
                    : source == 0.5
                    ? MoonPhase.Full_Moon
                    : source < 0.75
                    ? MoonPhase.Waning_Gibbous
                    : source == 0.75
                    ? MoonPhase.Third_Quarter
                    : source < 1.0
                    ? MoonPhase.Waning_Crescent
                    : throw new ArgumentOutOfRangeException("moon phase should be less than or equal 1 and greater or equal than 0", nameof(source));
    }


    public static Uvi GetUvIndex(this double source)
    {
        return source <= 2
            ? Uvi.Low
            : source <= 5
            ? Uvi.Normal
            : source <= 7
            ? Uvi.High
            : source <= 10
            ? Uvi.Very_High
            : Uvi.Extrem;
    }
}
