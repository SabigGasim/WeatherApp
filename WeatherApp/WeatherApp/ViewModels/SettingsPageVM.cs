using System;
using System.Collections.Generic;
using System.Linq;
using WeatherApp.Extensions;
using WeatherApp.Models;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    class SettingsPageVM : PropertyBinding
    {
        public string[] GetUnitNames { get; } = Enum.GetNames(typeof(Unit));

        public Root FullWeatherCast
        {
            get => Global.weatherPageVM.FullWeatherCast;
            set => Global.weatherPageVM.FullWeatherCast = value;
        }
    }
}
