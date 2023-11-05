using EasyNetQ;
using EasyNetQ.DI;
using FinCache.WorkerService;
using FinCache.WorkerService.Brokers.Amqp;
using FinCache.WorkerService.Brokers.Loggings;
using FinCache.WorkerService.Brokers.Storages;
using FinCache.WorkerService.Models.Amqp;
using FinCache.WorkerService.Services.Weather;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
   {
       var enviroment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

       var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{enviroment}.json", optional: true, reloadOnChange: true)
        .Build();

       //rabbitMQ      
       var rabbitConnection = configuration.GetValue<string>("RabbitConnection");
       services.AddSingleton(RabbitHutch.CreateBus(rabbitConnection, RegisterServices));

       //Brokers
       services.AddTransient<IAmqpBroker, AmqpBroker>();
       services.AddTransient<ILoggingBroker, LoggingBroker>();
       services.AddTransient<IStorageBroker, StorageBroker>();

       //Services
       services.AddTransient<IWeatherService, WeatherService>();

       services.AddHostedService<Worker>();
       
   })
    .Build();

await host.RunAsync();

void RegisterServices(IServiceRegister serviceRegister)
{
    serviceRegister.Register<ITypeNameSerializer>(provider => new AttributeNameSerializer());
}