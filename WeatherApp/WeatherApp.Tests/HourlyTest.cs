using FluentAssertions;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels;
using Xunit;

namespace WeatherApp.Tests;

public class ModelsTests
{
    [Theory]
    [MemberData(nameof(GetData), 1, 24)]
    public async void GetHourlyListWithTimeSpacing_ShouldWork(int hours)
    {
        // Arrange
        OpenWeatherMapService openWeatherMapService = new OpenWeatherMapService();
        var hourly = (await openWeatherMapService
            .GetFullWeatherCast(new Coordinates { Lat = 12, Lon = 40 }, "metric")).hourly;

        // Act 
        var list = WeatherPageVM.GetHourlyListWithTimeSpacing(hourly, hours);

        // Assert
        var ContainsDuplicates = list.Count != list.Distinct().Count();

        hourly.Should().NotBeNull();
        hourly.Count.Should().Be(24);

        list.Last().dt.Should().Be(hourly.Last().dt);
        if (hours > 1)
        {
            list.First().dt.Should().Be(hourly.First().dt);
        }

        list.Count.Should().Be(hours);

        ContainsDuplicates.Should().BeFalse();
    }

    public static IEnumerable<object[]> GetData(int first, int last)
    {
        for(; first <= last; first++)
        {
            yield return new object[] { first };
        }
    }
}