namespace WeatherApp.Services;

public interface IGpsDependencyService
{
    void OpenSettings();
    bool IsGpsEnabled();
}
