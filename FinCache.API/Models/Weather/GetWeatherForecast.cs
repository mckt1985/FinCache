using FinCache.API.Models.Amqp;

namespace FinCache.API.Models.Weather
{

    [MessageType("fincache.weather.forecast.request")]
    public class GetWeatherForecastRequest
    {
        public string City { get; set; }
    }

    [MessageType("fincache.weather.forecast.response")]
    public class GetWeatherForecastResponse
    {
        public WeatherForecast Weather { get; set; }
    }

}
