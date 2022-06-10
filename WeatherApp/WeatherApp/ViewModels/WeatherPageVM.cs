using WeatherApp.Models;
using WeatherApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Views;
using System;
using Plugin.Connectivity;
using WeatherApp.Threading;
using WeatherApp.Extensions;
using Xamarin.Forms;
using WeatherApp.Extenstions;
using System.Collections.ObjectModel;

namespace WeatherApp.ViewModels
{
    public class WeatherPageVM : PropertyBinding
    {
        private readonly IWeatherService _weatherService;
        
        private Root fullWeatherCast = new Root();

        public WeatherPageVM()
        {
            _weatherService = DependencyService.Get<IWeatherService>();
        }

        public Root FullWeatherCast
        {
            get => fullWeatherCast;
            set => SetValue(ref fullWeatherCast, ref value, checkEquality: false);
        }

        public async Task<ApiResponse> UpdateWeatherCast()
        {
            var response = await GetWeatherCast();
            if (!response.Succeded)
            {
                return ApiResponse.Failed();
            }

            fullWeatherCast.ui.icon =
                $"weatherIcon_{response.FullWeatherCast.current.weather[0].icon}.png";

            fullWeatherCast.ui.date = DateTime.Now.ToString("dd-MMMM-yyyy\ndddd");
            fullWeatherCast.ui.hourly = 
                GetHourlyListWithTimeSpacing(response.FullWeatherCast.hourly, fullWeatherCast.ui.hours);

            NameTheWeekDays(response.FullWeatherCast.daily);

            fullWeatherCast.apiRoot = response.FullWeatherCast;

            ApplyBindings();

            JsonService.WriteJsonFile<Root>(fullWeatherCast);

            return response;
        }

        private void ApplyBindings()
        {
            FullWeatherCast = fullWeatherCast;
            Pages.WeatherPage.Value.AdjustWidth();
        }

        public static List<Hourly> GetHourlyListWithTimeSpacing(List<Hourly> oldHourly, int hours)
        {
            var list = new List<Hourly>();
            double spacing = ((double)oldHourly.Count - 1) / ((double)hours - 1);

            for(double index = 0; list.Count < hours;  index += spacing)
            {
                if(list.Count == hours - 1)
                {
                    list.Add(oldHourly[oldHourly.Count - 1]);
                    break;
                }

                list.Add(oldHourly[(int)index]);
            }

            return list;
        }

        public void ChangeSpeedUnit(Unit oldUnit)
        {
            fullWeatherCast.apiRoot.current.wind_speed = fullWeatherCast.apiRoot.current.wind_speed.ConvertSpeedUnit(oldUnit);
            fullWeatherCast.apiRoot.current.wind_gust = fullWeatherCast.apiRoot.current.wind_gust.ConvertSpeedUnit(oldUnit);
            foreach (Daily day in fullWeatherCast.apiRoot.daily)
            {
                day.wind_speed = day.wind_speed.ConvertSpeedUnit(oldUnit);
                day.wind_gust = day.wind_gust.ConvertSpeedUnit(oldUnit);
            }
        }

        public void ChangeTemperatureUnit(Unit oldUnit)
        {
            fullWeatherCast.apiRoot.current.temp = fullWeatherCast.apiRoot.current.temp.ConvertTemperatureUnit(oldUnit);
            fullWeatherCast.apiRoot.current.feels_like = fullWeatherCast.apiRoot.current.feels_like.ConvertTemperatureUnit(oldUnit);

            // We copy only the things that will be bind to the UI
            var daily =  fullWeatherCast.apiRoot.daily.DeepCopy();
            var uiHourly = fullWeatherCast.ui.hourly.DeepCopy();

            ChangeTemperatureUnit(oldUnit,
                daily,
                fullWeatherCast.apiRoot.hourly,
                uiHourly);

            fullWeatherCast.apiRoot.daily = daily;
            fullWeatherCast.ui.hourly = uiHourly;
        }

        private static void ChangeTemperatureUnit(Unit oldUnit,
            IList<Daily> daily,
            IList<Hourly> hourly,
            IList<Hourly> uiHourly)
        {
            foreach (var day in daily)
            {
                day.temp.eve = day.temp.eve.ConvertTemperatureUnit(oldUnit);
                day.temp.morn = day.temp.morn.ConvertTemperatureUnit(oldUnit);
                day.temp.night = day.temp.night.ConvertTemperatureUnit(oldUnit);
                day.temp.day = day.temp.day.ConvertTemperatureUnit(oldUnit);
                day.temp.min = day.temp.min.ConvertTemperatureUnit(oldUnit);
                day.temp.max = day.temp.max.ConvertTemperatureUnit(oldUnit);

                day.feels_like.eve = day.feels_like.eve.ConvertTemperatureUnit(oldUnit);
                day.feels_like.morn = day.feels_like.morn.ConvertTemperatureUnit(oldUnit);
                day.feels_like.night = day.feels_like.night.ConvertTemperatureUnit(oldUnit);
                day.feels_like.day = day.feels_like.day.ConvertTemperatureUnit(oldUnit);
            }

            foreach(var hour in hourly)
            {
                hour.temp = hour.temp.ConvertTemperatureUnit(oldUnit);
                hour.dew_point = hour.dew_point.ConvertTemperatureUnit(oldUnit);
                hour.feels_like = hour.feels_like.ConvertTemperatureUnit(oldUnit);
            }

            foreach(var hour in uiHourly)
            {
                hour.temp = hour.temp.ConvertTemperatureUnit(oldUnit);
                hour.dew_point = hour.dew_point.ConvertTemperatureUnit(oldUnit);
                hour.feels_like = hour.feels_like.ConvertTemperatureUnit(oldUnit);
            }
        }

        public async Task<ApiResponse> GetWeatherCast()
        {
            if (!IsWifiConnected())
            {
                MainThread.InvokeOnMainThreadIfNeeded(() =>
                {
                    App.Current.MainPage.DisplayAlert("Error", "There is no Wifi connection!", "OK");
                    isRefreshing = false;
                });
                return ApiResponse.Failed();
            }

            isRefreshing = true;
            
            var root = await _weatherService
                .GetFullWeatherCast(fullWeatherCast.ui.location.Coordinates, fullWeatherCast.ui.unitString);
            
            isRefreshing = false;

            await Task.Delay(175);
            
            if (root is null)
            {
                return ApiResponse.Failed();
            }
            
            return new ApiResponse
            {
                FullWeatherCast = root,
                Succeded = true
            };
        }

        private bool IsWifiConnected()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        private void NameTheWeekDays(ObservableCollection<Daily> days)
        {
            days[0].dayName = "Today";
            
            for (int index = 1; index < days.Count; index++)
            {
                days[index].dayName = DateTime.Now.AddDays(index).DayOfWeek.ToString();
            }
        }

        private bool isRefreshing
        {
            get => Pages.WeatherPage.Value.RefreshView.IsRefreshing;
            set => Pages.WeatherPage.Value.RefreshView.IsRefreshing = value;
        }
    }
}
