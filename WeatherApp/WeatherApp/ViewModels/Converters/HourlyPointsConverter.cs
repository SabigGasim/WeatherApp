using System;
using System.Globalization;
using System.Linq;
using WeatherApp.Views;
using Xamarin.Forms;

namespace WeatherApp.ViewModels.Converters;

public class HourlyPointsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if (value is long dateTime)
        {
            if(dateTime == default(long))
            {
                return -20;
            }

            var hour = Global.weatherPageVM.FullWeatherCast.ui.hourly
                .FirstOrDefault(x => x.dt == dateTime);
            if(hour is null)
            {
                return -20;
            }

            var index = Global.weatherPageVM.FullWeatherCast.ui.hourly.IndexOf(hour);

            return Pages.WeatherPage.Value.Polyine.Points[index].Y - 14;
        }

        return -20;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
