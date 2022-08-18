namespace WeatherApp.Domain
{
    public static partial class Constants
    {
        public static partial class GoogleMapsApi
        {
            public const string Address = "https://maps.googleapis.com/maps/";
            public const string Autocomplete = "api/place/autocomplete";
            public const string Details = "api/place/details";
        }

        public static partial class OpenWeatherMapApi
        {
            public const string Address = "https://api.openweathermap.org/data/2.5/";
        }

        public static class Symbols
        {
            public static class Directions
            {
                public const char North = '↑';
                public const char NorthEast = '↗';
                public const char East = '→';
                public const char SouthEast = '↘';
                public const char South = '↓';
                public const char SouthWest = '↙';
                public const char West = '←';
                public const char NorthWest = '↖';
            }
        }
    }
}