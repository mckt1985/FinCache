using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Models.Weather.Exceptions
{
    public class InvalidCityException : Exception
    {
        public InvalidCityException() :
            base(message: "Please provoide a valid City")
        { }
    }
}
