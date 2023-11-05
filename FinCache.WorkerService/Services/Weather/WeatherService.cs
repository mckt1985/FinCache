using FinCache.WorkerService.Brokers.Loggings;
using FinCache.WorkerService.Brokers.Storages;
using FinCache.WorkerService.Models.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Services.Weather
{
    public partial class WeatherService : IWeatherService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public WeatherService(
            IStorageBroker storageBroker, 
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public Task<WeatherForecast> RetrieveWeatherForecastByCityAsync(string city) =>
        TryCatch(async () =>
        {
            ValidateCity(city);

            var weatherForcast =
                await this.storageBroker.SelectWeatherForecastByCityAsync(city);

            return weatherForcast;

        });       
    }
}
