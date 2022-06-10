using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Extenstions
{
    public static class LongExtensions
    {
        public static string ConvertToDateTime(this long source, string format)
        {
            return DateTimeOffset.FromUnixTimeSeconds(source).ToLocalTime().ToString(format);
        }
    }
}
