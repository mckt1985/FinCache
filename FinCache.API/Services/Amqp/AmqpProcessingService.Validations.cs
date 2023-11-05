using FinCache.API.Models.Amqp.Exceptions;

namespace FinCache.API.Services.Amqp
{
    public partial class AmqpProcessingService
    {
        private void ValidateAmqpRequest<TRequest>(TRequest request)
        {
            if (request is null)
            {
                throw new InvalidAmqpRequestException();
            }
        }
    }
}
