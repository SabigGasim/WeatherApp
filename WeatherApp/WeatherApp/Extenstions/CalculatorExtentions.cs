using WeatherApp.Models;

namespace WeatherApp.Extensions
{
    public static class CalculatorExtensions
    {
        public static double ConvertTemperatureUnit(this double temperature, Unit oldUnit)
        {
            double newTemperature = oldUnit switch
            {
                Unit.Fahrenheit => (temperature - 32) / 1.8,
                Unit.Celsius => temperature * 1.8 + 32,
                _ => 0
            };

            return newTemperature;
        }

        public static double ConvertSpeedUnit(this double speed, Unit oldUnit)
        {
            double newSpeed = oldUnit switch
            {
                Unit.Fahrenheit => speed / 2.236936,
                Unit.Celsius => speed * 2.236936,
                _ => 0
            };

            return newSpeed;
        }
    }
}
