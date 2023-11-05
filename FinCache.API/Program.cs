using EasyNetQ;
using EasyNetQ.DI;
using FinCache.API.Brokers.Amqp;
using FinCache.API.Brokers.Loggings;
using FinCache.API.Models.Amqp;
using FinCache.API.Services.Amqp;
using FinCache.InMemory.Config;
using FinCache.InMemory.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder()
 .SetBasePath(Directory.GetCurrentDirectory())
 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
 .AddJsonFile($"appsettings.{enviroment}.json", optional: true, reloadOnChange: true)
 .Build();

//Brokers
builder.Services.AddScoped<IAmqpBroker, AmqpBroker>();
builder.Services.AddScoped<ILoggingBroker, LoggingBroker>();

//Services
builder.Services.AddScoped<IAmqpProcessingService, AmqpProcessingService>();

var finCacheConfig = configuration.GetSection("FinCacheConfig").Get<FinCacheConfig>();
builder.Services.AddFinCache(finCacheConfig);

//rabbitMQ
var rabbitConnection = configuration.GetValue<string>("RabbitConnection");
builder.Services.AddSingleton(RabbitHutch.CreateBus(rabbitConnection, RegisterServices));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void RegisterServices(IServiceRegister serviceRegister)
{
    serviceRegister.Register<ITypeNameSerializer>(provider => new AttributeNameSerializer());
}
