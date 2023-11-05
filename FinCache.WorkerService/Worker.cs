using FinCache.WorkerService.Brokers.Amqp;
using FinCache.WorkerService.Brokers.Loggings;
using FinCache.WorkerService.Handlers;
using FinCache.WorkerService.Handlers.Weather;
using FinCache.WorkerService.Processors;
using FinCache.WorkerService.Services.Weather;

namespace FinCache.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILoggingBroker _logger;
        private bool IsRunning = false;
        private readonly IAmqpBroker amqpBroker;
        private readonly IWeatherService weatherService;

        public Worker(
            ILoggingBroker logger,
            IAmqpBroker amqpBroker,
            IWeatherService weatherService)
        {
            _logger = logger;
            this.amqpBroker = amqpBroker;
            this.weatherService = weatherService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!IsRunning)
                {
                    var messageHandlers = new List<IMessageHandler>();
                    messageHandlers.Add(new WeatherForecastRequestHandler(weatherService, amqpBroker));

                    var requestProcessor = new RequestProcessor(messageHandlers);
                    requestProcessor.Start();

                    IsRunning = true;

                }

                _logger.LogInformation($"Worker running at: { DateTimeOffset.Now}");

                await Task.Delay(1000, stoppingToken);
            }
        }

    }
}