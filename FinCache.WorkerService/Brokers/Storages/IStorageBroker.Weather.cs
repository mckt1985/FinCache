using FinCache.WorkerService.Models.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        Task<WeatherForecast> SelectWeatherForecastByCityAsync(string city);
    }
}
