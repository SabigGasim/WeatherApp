using System;

namespace WeatherApp.Views;
public static class Pages
{
    public static Lazy<WeatherPage> WeatherPage = new Lazy<WeatherPage>(() => new WeatherPage());
    public static Lazy<MapPage> MapPage = new Lazy<MapPage>(() => new MapPage());
    public static Lazy<SettingsPage> SettingsPage = new Lazy<SettingsPage>(() => new SettingsPage());
}
