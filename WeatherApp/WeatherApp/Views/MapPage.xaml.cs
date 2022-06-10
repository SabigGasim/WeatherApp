using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Models;
using WeatherApp.ViewModels;
using System.Threading.Tasks;
using System;
using Xamarin.Forms.GoogleMaps;
using WeatherApp.Threading;
using WeatherApp.Services;

namespace WeatherApp.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MapPage : ContentPage
{
    private readonly IGpsDependencyService _gpsDependencyService;

    public MapPage()
    {
        InitializeComponent();

        _gpsDependencyService = DependencyService.Get<IGpsDependencyService>();
        this.BindingContext = DependencyService.Get<MapPageVM>();

        map.UiSettings.MyLocationButtonEnabled = true;

        var coords = Global.weatherPageVM.FullWeatherCast.ui.location?.Coordinates;
        Position position = coords is null
            ? new Position(40, 35)
            : GetPosition(coords);

        map.InitialCameraUpdate = CameraUpdateFactory.NewCameraPosition(new CameraPosition(
            position,  // latlng
            13d, // zoom
            0d, // rotation
            60d)); // angle
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        var coords = Global.weatherPageVM.FullWeatherCast.ui.location?.Coordinates;

        Position position = coords is null
            ? new Position(40, 35)
            : GetPosition(coords);

        map.MoveCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(
            position,  // latlng
            13d, // zoom
            0d, // rotation
            60d))); // angle

        if (coords is null)
        {
            return;
        }

        AddPin(coords.Lat, coords.Lon);
    }

    private void Map_MapClicked(object sender, MapClickedEventArgs e)
    {
        map.SelectedPin = AddPin(e.Point.Latitude, e.Point.Longitude);
    }

    private Pin AddPin(double lat, double lon)
    {
        map.Pins.Clear();
        var pin = new Pin()
        {
            Position = new Position(lat, lon),
            Label = "Selected location".ToUpper()
        };
        map.Pins.Add(pin);

        return pin;
    }

    private async Task SetNewLocation(Coordinates coordinates)
    {
        await Task.Delay(250);
        Global.weatherPageVM.FullWeatherCast.ui.location.Coordinates = coordinates;
        await Global.weatherPageVM.UpdateWeatherCast();
    }

    private void SelectPlace_Clicked(object sender, EventArgs e)
    {
        Lock.LockAsync(async () =>
        {
            var coordinates = new Coordinates()
            {
                Lat = map.SelectedPin.Position.Latitude,
                Lon = map.SelectedPin.Position.Longitude
            };

            await Navigation.PopAsync();

            await SetNewLocation(coordinates);
        });
    }

    private Position GetPosition(Coordinates coord)
    {
        var (lat, lon) = coord;
        return new Position(lat, lon);
    }

    private async void Map_MyLocationButtonClicked(object sender, MyLocationButtonClickedEventArgs e)
    {
        if (_gpsDependencyService.IsGpsEnabled())
        {
            return;
        }

        e.Handled = true;

        var accepted = await App.Current.MainPage.DisplayAlert(
            title: null,
            message: "You have to enable GPS to continue", 
            accept: "Ok",
            cancel: "Cancel");

        if (accepted)
        {
            _gpsDependencyService.OpenSettings();
        }
    }

    private async void MapTypeButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.LockPushPopupAsync(new MapTypesPopupPage());
    }
}
