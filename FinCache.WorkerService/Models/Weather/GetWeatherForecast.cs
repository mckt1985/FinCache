using FinCache.WorkerService.Models.Amqp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Models.Weather
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
