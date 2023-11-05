using FinCache.WorkerService.Models.Weather;
using FinCache.WorkerService.Models.Weather.Exceptions;

namespace FinCache.WorkerService.Services.Weather
{
    public partial class WeatherService
    {
        private delegate Task<WeatherForecast> ReturningWeatherForecastFunction();

        private async Task<WeatherForecast> TryCatch(ReturningWeatherForecastFunction returningWeatherForecastFunction)
        {
            try
            {
                return await returningWeatherForecastFunction();
            }
            catch(InvalidCityException invalidCityException)
            {
                throw CreateAndLogValidationException(invalidCityException);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private WeatherServiceException CreateAndLogServiceException(Exception exception)
        {
            var weatherServiceException = new WeatherServiceException(exception);
            this.loggingBroker.LogError(weatherServiceException);

            return weatherServiceException;
        }

        private WeatherServiceValidationException CreateAndLogValidationException(Exception exception)
        {
            var weatherServiceValidationException = new WeatherServiceValidationException(exception);
            this.loggingBroker.LogError(weatherServiceValidationException);

            return weatherServiceValidationException;
        }
    }
}
