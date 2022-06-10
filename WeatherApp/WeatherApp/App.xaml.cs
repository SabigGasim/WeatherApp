using Xamarin.Forms;
using WeatherApp.Views;
using System.IO;
using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.Register<IWeatherService, OpenWeatherMapService>();
            DependencyService.RegisterSingleton<MapPageVM>(new MapPageVM());

            MainPage = new NavigationPage(Pages.WeatherPage.Value);
            InitializeComponent();
        }

        protected override void OnStart()
        {
            base.OnStart();
            
            if (!File.Exists(JsonService._fileName))
            {
                File.Create(JsonService._fileName);
            }

            Pages.WeatherPage.Value.SetAllBindings();
        }
    }
}