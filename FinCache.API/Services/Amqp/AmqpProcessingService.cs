using FinCache.API.Brokers.Amqp;
using FinCache.API.Brokers.Loggings;

namespace FinCache.API.Services.Amqp
{
    public partial class AmqpProcessingService : IAmqpProcessingService
    {
        private readonly IAmqpBroker amqpBroker;
        private readonly ILoggingBroker loggingBroker;

        public AmqpProcessingService(
           IAmqpBroker amqpBroker, 
           ILoggingBroker loggingBroker)
        {
            this.amqpBroker = amqpBroker;
            this.loggingBroker = loggingBroker;
        }

        public async Task PublishMessageAsync<T>(T message)
        {
            await this.amqpBroker.PublishAsync(message);
        }

        public Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request) =>
        TryCatch(async () => {

            ValidateAmqpRequest(request);

            return await this.amqpBroker.RequestAsync<TRequest, TResponse>(request);

        });
    }
}
