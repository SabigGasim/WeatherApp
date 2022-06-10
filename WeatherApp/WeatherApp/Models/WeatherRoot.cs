using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WeatherApp.Extenstions;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace WeatherApp.Models
{
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Current
    {
        [JsonIgnore]
        public string time
        {
            get => dt.ConvertToDateTime("h tt");
        }

        public long dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double uvi { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public double wind_gust { get; set; }
        public List<Weather> weather { get; set; }
    }

    public class Temp
    {
        public double day { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }
    }

    public class FeelsLike
    {
        public double day { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }
    }

    public class Daily
    {
        public long dt { get; set; }
        public long sunrise { get; set; }
        public long sunset { get; set; }
        public long moonrise { get; set; }
        public long moonset { get; set; }
        [JsonProperty("moon_phase")]
        public double moonPhase { get; set; }
        public Temp temp { get; set; }
        public FeelsLike feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public double wind_gust { get; set; }
        public List<Weather> weather { get; set; }
        public double clouds { get; set; }
        public double uvi { get; set; }
        public string dayName { get; set; }
        public double pop { get; set; }

        [JsonIgnore]
        public double popChance
        {
            get => pop * 100;
        }
        [JsonIgnore]
        public string time
        {
            get => dt.ConvertToDateTime("MMM / dd");
        }
        [JsonIgnore]
        public string sun_rise
        {
            get => sunrise.ConvertToDateTime("hh:mm tt");
        }
        [JsonIgnore]
        public string sun_set
        {
            get => sunset.ConvertToDateTime("hh:mm tt");
        }
        [JsonIgnore]
        public string moon_rise
        {
            get => moonrise.ConvertToDateTime("hh:mm tt");
        }
        [JsonIgnore]
        public string moon_set
        {
            get => moonset.ConvertToDateTime("hh:mm tt");
        }
        [JsonIgnore]
        public string moon_phase
        {
            get => Enum.GetName(typeof(MoonPhase), moonPhase.GetMoonPhase()).Replace('_', ' ');
        }
        [JsonIgnore]
        public string temperatureStatus
        {
            get => temp.GetTemperatureStatus();
        }
        [JsonIgnore]
        public string feelsLikeStatus
        {
            get => feels_like.GetFeelsLikeStatus();
        }
        [JsonIgnore]
        public string weatherStatus
        {
            get => this.GetWeatherStatus();
        }
        [JsonIgnore]
        public string dateTimeStatus
        {
            get => this.GetDateTimeStatus();
        }
    }

    public class Root
    {
        // Will be set from the api
        public ApiRoot apiRoot { get; set; }

        // Will be set from the ViewModels
        public UI ui { get; set; } = new UI();
    }

    public class Hourly
    {
        [JsonIgnore]
        public string time
        {
            get => dt.ConvertToDateTime("h tt");
        }

        public long dt { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double uvi { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public double wind_gust { get; set; }
        public List<Weather> weather { get; set; }
        public double pop { get; set; }
        
        [JsonIgnore]
        public double popChance 
        {
            get => pop * 100;
        }

    }

    public class ApiRoot
    {
        public Current current { get; set; }
        public ObservableCollection<Daily> daily { get; set; }
        public List<Hourly> hourly { get; set; }
        public string timezone
        {
            get => _timezone;
            set => _timezone = value.Replace("/", " - ");
        }

        // Backing-fields
        [JsonIgnore]
        private string _timezone = "";
    }

    public class UI : IEquatable<UI>
    {
        public Unit unit { get; set; }
        public string date { get; set; }
        public string icon { get; set; }
        public int hours { get; set; } = 24;
        public List<Hourly> hourly { get; set; } = new();

        [JsonIgnore]
        public string unitSymbol
        {
            get => unit == Unit.Celsius
                    ? "°C"
                    : "°F";
        }

        [JsonIgnore]
        public string unitString
        {
            get => unit == Unit.Celsius
                    ? "metric"
                    : "imperial";
        }

        [JsonIgnore]
        public string speedUnit
        {
            get => unit == Unit.Celsius
                    ? " m/s"
                    : "/mph";
        }

        [JsonIgnore]
        public PointCollection points
        {
            get
            {
                var points = new PointCollection();

                if (hourly.Count < 1)
                {
                    return points;
                }

                var min = hourly.Min(hour => hour.temp);
                var max = hourly.Max(hour => hour.temp);

                var viewWidth = 44d; // WeatherPage > HourlyStacklayout > DataTemplate > HourlyGrid > WeatherIcon.WidthRequest
                var spacing = 20d; // WeatherPage > HourlyStacklayout.Spacing
                var leftPadding = 10d;
                var xSpacing = spacing + viewWidth;
                var minHeight = 145d; // WeatherPage > MainGrid > Grid.RowDefinitions > RowDefinition[2] - 55
                var area = 35d / (max - min);
                var xPosition = (leftPadding + viewWidth / 2) - xSpacing;
                double yPosition;

                for (int i = 0; i < hourly.Count; i++)
                {
                    yPosition = minHeight - (area * (hourly[i].temp - min));
                    xPosition += xSpacing;

                    points.Add(new Point(xPosition, yPosition));
                }

                return points;
            }
        }

        public Location location { get; set; } = new Location();

        public bool Equals(UI other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (EqualityComparer<int>.Default.Equals(hours, other.hours) == false) return false;
            if (EqualityComparer<Unit>.Default.Equals(unit, other.unit) == false) return false;

            return true;
        }
    }
}
