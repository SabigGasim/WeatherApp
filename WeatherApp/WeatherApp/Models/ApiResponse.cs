namespace WeatherApp.Models;

public class ApiResponse
{
    public ApiRoot FullWeatherCast { get; set; }
    public bool Succeded { get; set; }

    public static ApiResponse Failed()
    {
        return new ApiResponse()
        {
            FullWeatherCast = null,
            Succeded = false,
        };
    }
}
