using FinCache.WorkerService.Models.Weather;
using FinCache.WorkerService.Models.Weather.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Tests.Unit.Services.Weather
{
    public partial class WeatherServiceTests
    {
        [Theory]
        [InlineData("london")]
        public async Task Should_Retrieve_Weather_Forecast_By_City_Async(string city)
        {
            //given
            var weatherForecast = new WeatherForecast() { City = "london", Weather = "cloudy" };

            this.storageBrokerMock.Setup(b => b.SelectWeatherForecastByCityAsync(city))
                .ReturnsAsync(weatherForecast);

            //when
            var result =
                await this.weatherService.RetrieveWeatherForecastByCityAsync(city);

            //then
            result.Should().BeEquivalentTo(weatherForecast);

            this.storageBrokerMock.Verify(b => b.SelectWeatherForecastByCityAsync(city), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }


        [Theory]
        [InlineData(null)]
        public async Task Should_Throw_Exception_When_City_IsNull_Async(string city)
        {
            //given
            var exception = new InvalidCityException();
            var expectedValidationException = new WeatherServiceValidationException(exception);
            //when
            Task weatherForecastTask = this.weatherService.RetrieveWeatherForecastByCityAsync(city);

            //then
            await Assert.ThrowsAsync<WeatherServiceValidationException>(() => weatherForecastTask);

            this.storageBrokerMock.Verify(b => b.SelectWeatherForecastByCityAsync(city), Times.Never);
            this.loggingBrokerMock.Verify(b => b.LogError(It.Is(SameExceptionAs(expectedValidationException))), Times.Once); 

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

    }
}
