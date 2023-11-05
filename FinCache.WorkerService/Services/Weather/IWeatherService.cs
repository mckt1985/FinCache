using FinCache.WorkerService.Models.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Services.Weather
{
    public interface IWeatherService
    {
        Task<WeatherForecast> RetrieveWeatherForecastByCityAsync(string city);
    }
}
