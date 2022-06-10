using FluentAssertions;
using System.Collections.ObjectModel;
using WeatherApp.Model;
using WeatherApp.Services;
using WeatherApp.ViewModels;
using Xunit;

namespace WeatherApp.Tests;

public class ModelsTests
{

    [Theory]
    [InlineData(1)] [InlineData(2)] [InlineData(3)] [InlineData(4)]
    [InlineData(5)] [InlineData(6)] [InlineData(7)] [InlineData(8)]
    [InlineData(9)] [InlineData(10)][InlineData(11)][InlineData(12)]
    [InlineData(13)][InlineData(14)][InlineData(15)][InlineData(16)] 
    [InlineData(17)][InlineData(18)][InlineData(19)][InlineData(20)] 
    [InlineData(21)][InlineData(22)][InlineData(23)][InlineData(24)]
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
        if(hours > 1)
        {
            list.First().dt.Should().Be(hourly.First().dt);
        }

        list.Count.Should().Be(hours);
        
        ContainsDuplicates.Should().BeFalse();
    }
}
