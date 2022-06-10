using System;
using WeatherApp.Models;
using WeatherApp.ViewModels;
using Xamarin.Forms;

namespace WeatherApp.Extenstions;
public static class WeatherExtensions
{
    public static string GetWeatherStatus(this Daily source)
    {
        var unit = DependencyService.Get<WeatherPageVM>().FullWeatherCast.ui.unit == Unit.Celsius
                ? " m/s"
                : "/mph";
        var direction = source.wind_deg.GetDirection();
        var uvi = Enum.GetName(typeof(Uvi), source.uvi.GetUvIndex()).Replace('_', ' ');

        return $"Main: {source.weather[0].main}\nPrecipitation: {(int)source.popChance}%\nHumidity: {source.humidity}%\n" +
            $"Wind: {string.Format("{0:N2}", source.wind_speed)}{unit} {direction}\n" +
            $"Gust: {string.Format("{0:N2}",source.wind_gust)}{unit} {direction}\n" +
            $"Cloudiness: {(int)source.clouds}%\nDew point: {(int)source.dew_point}°\n" +
            $"Uvi: {uvi}\nPressure: {source.pressure}/hPa";
    }

    public static string GetTemperatureStatus(this Temp source) =>
        $"Morning: {(int)source.morn}°\nDay: {(int)source.day}°\n" +
        $"Evening: {(int)source.eve}°\nNight: {(int)source.night}°";

    public static string GetFeelsLikeStatus(this FeelsLike source) =>
        $"Morning: {(int)source.morn}°\nDay: {(int)source.day}°\n" +
        $"Evening: {(int)source.eve}°\nNight: {(int)source.night}°";

    public static string GetDateTimeStatus(this Daily source) =>
        $"Date: {source.time}\nSunrise: {source.sun_rise}\nSunset: {source.sun_set}\n" +
        $"Moonrise: {source.moon_rise}\nMoonset: {source.moon_set}\nMoon phase: {source.moon_phase}";
}
