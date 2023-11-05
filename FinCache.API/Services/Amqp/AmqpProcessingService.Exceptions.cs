using EasyNetQ;
using FinCache.API.Models.Amqp.Exceptions;

namespace FinCache.API.Services.Amqp
{
    public partial class AmqpProcessingService
    {
        private delegate Task<TResult> ReturningAMQPResponseFunction<TResult>();

        private async Task<TResult>TryCatch<TResult>(ReturningAMQPResponseFunction<TResult> returningAMQPResponseFunction) 
        {
            try
            {
                return await returningAMQPResponseFunction();
            }
            catch (InvalidAmqpRequestException invalidAmqpRequestException)
            {
                throw CreateAndLogValidationException(invalidAmqpRequestException);
            }
            catch (EasyNetQException easynetqException) 
            {
                throw CreateAndLogDependencyException(easynetqException);
            }
            catch (Exception exception)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private Exception CreateAndLogValidationException(Exception exception)
        {
            var amqpValidationException = new AmqpValidationException(exception);
            this.loggingBroker.LogError(amqpValidationException);

            return amqpValidationException;
        }

        private AmqpServiceDependencyException CreateAndLogDependencyException(Exception exception)
        {
            var amqpServiceDependencyException = new AmqpServiceDependencyException(exception);
            this.loggingBroker.LogError(amqpServiceDependencyException);

            return amqpServiceDependencyException;
        }

        private AmqpProcessingServiceException CreateAndLogServiceException(Exception exception)
        {
            var amqpProcessingServiceException = new AmqpProcessingServiceException(exception);
            this.loggingBroker.LogError(amqpProcessingServiceException);

            return amqpProcessingServiceException;
        }
    }
}
