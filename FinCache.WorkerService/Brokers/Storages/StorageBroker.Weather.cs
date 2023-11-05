using FinCache.WorkerService.Models.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Brokers.Storages
{
    public partial class StorageBroker
    {
        public async Task<WeatherForecast> SelectWeatherForecastByCityAsync(string city)
        {
            // here we would call the database
            var weatherForecast = SampleDatabaseWeatherItems().Where(w => w.City.ToLower() == city.ToLower()).FirstOrDefault();

            return await Task.FromResult(weatherForecast);
        }

        private IList<WeatherForecast> SampleDatabaseWeatherItems()
        {
            var list = new List<WeatherForecast>()
            {
                new() { City = "New York", Weather = "Partly Cloudy", Latitude = 40.7128, Longitude = -74.0060 },
                new() { City = "Tokyo", Weather = "Rainy", Latitude = 35.6895, Longitude = 139.6917 },
                new() { City = "London", Weather = "Overcast", Latitude = 51.5074, Longitude = -0.1278 },
                new() { City = "Paris", Weather = "Sunny", Latitude = 48.8566, Longitude = 2.3522 },
                new() { City = "Shanghai", Weather = "Smoggy", Latitude = 31.2304, Longitude = 121.4737 },
                new() { City = "Los Angeles", Weather = "Hazy", Latitude = 34.0522, Longitude = -118.2437 },
                new() { City = "Singapore", Weather = "Humid", Latitude = 1.3521, Longitude = 103.8198 },
                new() { City = "Hong Kong", Weather = "Foggy", Latitude = 22.3193, Longitude = 114.1694 },
                new() { City = "Dubai", Weather = "Scorching", Latitude = 25.276987, Longitude = 55.296249 },
                new() { City = "Beijing", Weather = "Windy", Latitude = 39.9042, Longitude = 116.4074 },
                new() { City = "Mumbai", Weather = "Muggy", Latitude = 19.0760, Longitude = 72.8777 },
                new() { City = "Sydney", Weather = "Pleasant", Latitude = -33.8688, Longitude = 151.2093 },
                new() { City = "Berlin", Weather = "Chilly", Latitude = 52.5200, Longitude = 13.4050 },
                new() { City = "Toronto", Weather = "Snowy", Latitude = 43.651070, Longitude = -79.347015 },
                new() { City = "Chicago", Weather = "Blustery", Latitude = 41.8781, Longitude = -87.6298 },
                new() { City = "Seoul", Weather = "Clear", Latitude = 37.5665, Longitude = 126.9780 },
                new() { City = "Moscow", Weather = "Freezing", Latitude = 55.7558, Longitude = 37.6173 },
                new() { City = "São Paulo", Weather = "Drizzly", Latitude = -23.550520, Longitude = -46.633308 },
                new() { City = "Mexico City", Weather = "Thunderstorms", Latitude = 19.4326, Longitude = -99.1332 },
                new() { City = "Amsterdam", Weather = "Breezy", Latitude = 52.3676, Longitude = 4.9041 }
             };
           
            return list;
        }
    }
}
