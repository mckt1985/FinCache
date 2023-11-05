# FinCache
FinCache is a generic in-memory thread-safe caching library for .Net. It stores arbitrary types of objects, which are added and retrieved 
using a unique key (similar to a dictionary).

To avoid the risk of running out of memory, the cache uses a configurable threshold for the maximum number of items which it can hold at any one time. 
If the cache becomes full, adding additional items will succeed, but will result in another item in the cache being 
evicted. The cache implements implement the ‘least recently used’ approach when selecting which item to evict.

## Installation

To install FinCache you simply add a reference to the library in your application

```csharp
using FinCache.InMemory.Config;
using FinCache.InMemory.Extensions;
```

Add a config section to your appsettings.json and change the Capacity as desired

```json
  "FinCacheConfig": {
    "Capacity": 10
  },
```

Register the service, this gets registered as a Singleton for use throughout the applacation

```csharp

var finCacheConfig = configuration.GetSection("FinCacheConfig").Get<FinCacheConfig>();

builder.Services.AddFinCache(finCacheConfig);

public static IServiceCollection AddFinCache(this IServiceCollection services, FinCacheConfig config)
{                
    services.AddSingleton<IFinCache>(x => new FinCache(config));
    return services;
}
```

## Usage
To use FinCache in your application, once the service has been registered you can inject it in to your application

```csharp
private readonly IFinCache cache;

public WeatherForecastController(IFinCache cache)
{
    this.cache = cache;            
}
```
You can then use FinCache as below

GetCache

```csharp
var value = this.cache.GetCache(key);
```

AddCache

```csharp
this.cache.AddCache(key, value);
```
There is also an Evicted Event which can be subscribed to. This could be hooked up to a notification service to send an alert when a cache item was evicted.

```csharp
this.cache.CacheItemEvicted += Cache_CacheItemEvicted;

private void Cache_CacheItemEvicted(object key)
{
    // send notification that cache item was evicted
}
```

## Sample Application

To showcase how to implement FinCache within your application there is a sample application within this repository.

It uses the default WeatherForecast example provided in the ASP.Net Core Web API to simulate the retrieval of the Weather forecast from a backend database through RabbitMQ using the RPC pattern. 

There is a backend worker service that is listenting to the queue and simulates calling a PostgreSQL/SQL Server database to retrieve the weather forecast by city name.

The cache uses the city name as the key so the first time the weather is requested it will go to the backend service to get the data, otherwise the weather will be returned from the cache.

![image](https://github.com/mckt1985/FinCache/assets/58369494/a9a248f9-b96f-48fe-a407-4780f1b7d8e2)

If using docker, navigate to the root of the solution directory and run the below command

```bash
docker-compose up
```

Your Swagger API documentation will be accessible by running the following 

```bash
http://localhost:5000/swagger/index.html
```

Or you can use Postman to test the weather API

```bash
https://localhost:5000/api/weather/forecast?city={city}
```
The list of available Cities to test are 

* New York
* Tokyo
* London
* Paris
* Shanghai
* Los Angeles
* Singapore
* Hong Kong
* Dubai
* Beijing
* Mumbai
* Sydney
* Berlin
* Toronto
* Chicago
* Seoul
* Moscow
* São Paulo
* Mexico City
* Amsterdam

And a sample response looks like

```json
{
  "City": "New York",
  "Weather": "Partly Cloudy",
  "Latitude": 40.7128,
  "Longitude": -74.0060
}
```

## License
This project is licensed under the MIT License.
