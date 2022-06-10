using Newtonsoft.Json;
using System.Linq;
using Xamarin.Essentials;

namespace WeatherApp.Models;

public class Coordinates
{
    public double Lat { get; set; } = default;
    public double Lon { get; set; } = default;

    internal void Deconstruct(out double lat, out double lon)
    {
        lat = Lat;
        lon = Lon;
    }
}

public class Location
{
    [JsonIgnore]
    private Coordinates coordinates = new Coordinates();

    public Coordinates Coordinates 
    {
        get => coordinates;
        set
        {
            coordinates = value;

            SetRegion(this);
        }
    }

    public string Name { get; set; }
    
    private async void SetRegion(Location location)
    {
        var (lat, lon) = location.Coordinates;
         
        var placemark = await Threading.MainThread.InvokeOnMainThreadIfNeeded(async () =>
                     {
                         return (await Geocoding.GetPlacemarksAsync(lat, lon))
                            .FirstOrDefault();
                     });

        var adminArea = placemark?.AdminArea;
        var country = placemark?.CountryName;

        location.Name = !string.IsNullOrEmpty(country) && !string.IsNullOrEmpty(adminArea)
             ? $"{country} - {adminArea}"
             : "-unkonwn-";
    }
}
