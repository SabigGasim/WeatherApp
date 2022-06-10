using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Domain;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class OpenWeatherMapService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public OpenWeatherMapService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Constants.OpenWeatherMapApi.Address)
            };
        }

        public async Task<ApiRoot> GetFullWeatherCast(Coordinates coords, string temperatureUnit)
        {
            try
            {
                var (lat, lon) = coords;

                var url = $"onecall?lat={lat}" +
                          $"&lon={lon}" +
                           "&exclude=minutely" +
                          $"&units={temperatureUnit}" +
                          $"&appid={Constants.OpenWeatherMapApi.Api_Key}";


                var fullWeatherCastJson = await _httpClient.GetStringAsync(url);
                var fullWeatherCast = JsonConvert.DeserializeObject<ApiRoot>(fullWeatherCastJson);
                KeepOnlyTheFirst24Hours(fullWeatherCast.hourly);

                return fullWeatherCast;
            }
            catch (ArgumentNullException) { return null; }
            catch (HttpRequestException)  { return null; }
        }

        private static void KeepOnlyTheFirst24Hours(List<Hourly> hourly)
        {
            hourly.RemoveRange(24, hourly.Count - 24);
            hourly.Capacity = hourly.Count;
        }
    }
}
