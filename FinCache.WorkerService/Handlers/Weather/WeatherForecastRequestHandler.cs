using FinCache.WorkerService.Brokers.Amqp;
using FinCache.WorkerService.Models.Weather;
using FinCache.WorkerService.Services.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Handlers.Weather
{
    internal class WeatherForecastRequestHandler : MessageHandler<GetWeatherForecastRequest>
    {
        protected override string HandlerId { get { return "fincache.service"; } }
        protected override string Topic { get { return "#"; } }

        private readonly IWeatherService weatherService;
        private readonly IAmqpBroker amqpBroker;

        public WeatherForecastRequestHandler(
            IWeatherService weatherService,
            IAmqpBroker amqpBroker) : base(null)
        {
            this.weatherService = weatherService;
            this.amqpBroker = amqpBroker;
        }

        public override void Start()
        {
            amqpBroker.Respond<GetWeatherForecastRequest, GetWeatherForecastResponse>(HandleResponse);
        }

        public GetWeatherForecastResponse HandleResponse(GetWeatherForecastRequest request)
        {
            var task = HandleResponseAsync(request);

            return task.Result;
        }

        private async Task<GetWeatherForecastResponse> HandleResponseAsync(GetWeatherForecastRequest request)
        {
            var response =
                await weatherService.RetrieveWeatherForecastByCityAsync(request.City);

            return new() { Weather = response };
        }

        public override void Handle(GetWeatherForecastRequest message)
        {
            throw new NotImplementedException();
        }
    }
}
