using FinCache.API.Models.Weather;
using FinCache.API.Services.Amqp;
using FinCache.InMemory.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FinCache.API.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IAmqpProcessingService amqpProcessingService;
        private readonly IFinCache cache;

        public WeatherForecastController(
            IAmqpProcessingService amqpProcessingService, 
            IFinCache cache)
        {
            this.amqpProcessingService = amqpProcessingService;
            this.cache = cache;
            this.cache.CacheItemEvicted += Cache_CacheItemEvicted;
        }
       
        [HttpGet("forecast")]
        public async Task<IActionResult> Forecast([FromQuery]string city)
        {
            try
            {
                var weather = this.cache.GetCache(city);

                if (weather is not null)
                {
                    return Ok(weather);
                }

                var request = new GetWeatherForecastRequest() { City = city };

                var response =
                    await this.amqpProcessingService.RequestAsync<GetWeatherForecastRequest, GetWeatherForecastResponse>(request);

                if(response.Weather is null)
                {
                    var result = new ObjectResult(new { error = "City not found in the list of the available cities." })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };

                    return result;
                }

                this.cache.AddCache(city, response.Weather);

                return Ok(response);
            }
            catch (Exception exception)
            {
                var result = new ObjectResult(new { error = exception.Message })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

                return result;
            }
        }

        private void Cache_CacheItemEvicted(object key)
        {
            // send notification that cache item was evicted
        }
    }
}