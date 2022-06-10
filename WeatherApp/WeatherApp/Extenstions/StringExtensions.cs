using System.Text.RegularExpressions;

namespace WeatherApp.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex _splitCamelCaseFirstRegex = new Regex(@"(\p{Ll})(\P{Ll})",
            RegexOptions.Compiled);
        private static readonly Regex _splitCamelCaseSecondRegex = new Regex(@"(\P{Ll})(\P{Ll}\p{Ll})",
            RegexOptions.Compiled);

        public static string SplitCamelCase(this string str)
        {
            return _splitCamelCaseFirstRegex.Replace(
                _splitCamelCaseSecondRegex.Replace(
                    str,
                    "$1 $2"
                ),
                "$1 $2"
            );
        }
    }
}
