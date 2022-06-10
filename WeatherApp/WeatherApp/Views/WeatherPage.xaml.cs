using WeatherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Services;
using Xamarin.Forms.Shapes;
using System.Linq;
using Xamarin.Essentials;
using Plugin.Connectivity;
using System;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherPage : ContentPage
    {
        public RefreshView RefreshView;
        public Polyline Polyine => HourlyGraph;

        public WeatherPage()
        {
            InitializeComponent();

            MainGrid.BindingContext = Global.weatherPageVM;
            RefreshView = refreshView;
        }

        public void SetAllBindings()
        {
            var fullWeatherCast = JsonService.ReadJsonFile();
            if(fullWeatherCast is null || fullWeatherCast?.apiRoot is null)
            {
                return;
            }

            if(fullWeatherCast.ui.hourly.Count < 6)
            {
                fullWeatherCast.ui.hours = 6;
                fullWeatherCast.ui.hourly = WeatherPageVM
                    .GetHourlyListWithTimeSpacing(fullWeatherCast.apiRoot.hourly, fullWeatherCast.ui.hours);
            }

            Global.weatherPageVM.FullWeatherCast = fullWeatherCast; 
        }

        public void RefreshView_Refreshing(object sender, System.EventArgs e)
        {
            
            _ = Global.weatherPageVM.UpdateWeatherCast();
        }

        private async void Countrybtn_Clicked(object sender, System.EventArgs e)
        {
            var permissionStatus = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (permissionStatus != PermissionStatus.Granted)
            {
                var result = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (result != PermissionStatus.Granted)
                {
                    return;
                }
            }

            await Navigation.LockPushAsync(Pages.MapPage.Value);
        }

        public void AdjustWidth()
        {
            HourlyAbsoluteLayout.WidthRequest = HourlyGraph.Points.Last().X + 34;
            HourlyAbsoluteLayout.WidthRequest = HourlyGraph.Points.Last().X + 34;
        }

        private async void Settings_Clicked(object sender, System.EventArgs e)
        {
            await this.LockPushModalAsync(Pages.SettingsPage.Value);
        }

        private async void OpenweathrmapButton_Clicked(object sender, System.EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    await Browser.OpenAsync("https://openweathermap.org",
                        BrowserLaunchMode.SystemPreferred);
                }
                catch (Exception) { }
            }
        }
    }
}