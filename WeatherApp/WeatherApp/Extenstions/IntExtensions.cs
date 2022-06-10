using System;
using WeatherApp.Domain;

namespace WeatherApp.Extenstions;
public static class IntExtensions
{
    /// <exception cref="ArgumentOutOfRangeException">if source is not in the range 0 to 360</exception>
    public static char GetDirection(this int source)
    {
        return source == 0 || source == 360
            ? Constants.Symbols.Directions.North
            : source < 90
            ? Constants.Symbols.Directions.NorthEast
            : source == 90
            ? Constants.Symbols.Directions.East
            : source < 180
            ? Constants.Symbols.Directions.SouthEast
            : source == 180
            ? Constants.Symbols.Directions.South
            : source < 270
            ? Constants.Symbols.Directions.SouthWest
            : source == 270
            ? Constants.Symbols.Directions.West
            : source < 360
            ? Constants.Symbols.Directions.NorthWest
            : throw new ArgumentOutOfRangeException("direction should be in range 0 to 360", nameof(source));
    }
}
