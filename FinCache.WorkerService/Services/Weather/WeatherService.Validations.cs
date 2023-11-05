using FinCache.WorkerService.Models.Weather.Exceptions;

namespace FinCache.WorkerService.Services.Weather
{
    public partial class WeatherService
    {       
        private void ValidateCity(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                throw new InvalidCityException();
            }
        }
    }
}
