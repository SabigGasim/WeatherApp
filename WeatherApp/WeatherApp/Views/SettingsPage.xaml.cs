using WeatherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.Models;
using WeatherApp.Extenstions;
using WeatherApp.Services;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private UI oldUi;

        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = new SettingsPageVM();
        }

        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            oldUi = Global.weatherPageVM.FullWeatherCast.ui.DeepCopy();
        }

        private void ContentPage_Disappearing(object sender, System.EventArgs e)
        {
            if (!IsUserChangedTheSettings())
            {
                return;
            }

            ApplySettings();

            Global.weatherPageVM.UpdateBindings(nameof(Global.weatherPageVM.FullWeatherCast));
            
            Pages.WeatherPage.Value.AdjustWidth();

            JsonService.WriteJsonFile(Global.weatherPageVM.FullWeatherCast);
        }

        private bool IsUserChangedTheSettings()
        {
            return !oldUi.Equals(Global.weatherPageVM.FullWeatherCast.ui);
        }

        /// <returns>true: if the user changed anything in the settings</returns>
        private void ApplySettings()
        {
            var fullWeatherCast = Global.weatherPageVM.FullWeatherCast;

            if (oldUi.unit != fullWeatherCast.ui.unit)
            {
                Global.weatherPageVM.ChangeTemperatureUnit(oldUi.unit);
                Global.weatherPageVM.ChangeSpeedUnit(oldUi.unit);
            }

            if(oldUi.hours != fullWeatherCast.ui.hours)
            {
                fullWeatherCast.ui.hourly = WeatherPageVM
                    .GetHourlyListWithTimeSpacing(fullWeatherCast.apiRoot.hourly, 
                    fullWeatherCast.ui.hours);
            }
        }

        private async void Back_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.LockPopModalAsync();
        }
    }
}