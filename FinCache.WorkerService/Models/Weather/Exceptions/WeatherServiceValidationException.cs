using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Models.Weather.Exceptions
{
    public class WeatherServiceValidationException : Exception
    {
        public WeatherServiceValidationException(Exception innerException)
            : base(message: "Weather Service Validation Exception Occurred", innerException) { }
    }
}
