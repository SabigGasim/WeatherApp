using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        Task<ApiRoot> GetFullWeatherCast(Coordinates coords, string temperatureUnit);
    }
}