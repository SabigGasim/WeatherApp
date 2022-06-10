using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace WeatherApp.ViewModels.Converters
{
    public class PinBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Pin selectedPin && selectedPin != null)
            {
                return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
