using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Models.Weather.Exceptions
{
    public class WeatherServiceException : Exception
    {
        public WeatherServiceException(Exception innerException)
            : base(message: "Weather Service Exception Occurred", innerException) { }
    }
}