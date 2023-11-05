
namespace FinCache.API.Services.Amqp
{
    public interface IAmqpProcessingService
    {
        Task PublishMessageAsync<T>(T message);
        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request);
    }
}
