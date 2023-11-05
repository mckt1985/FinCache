namespace FinCache.API.Models.Amqp.Exceptions
{
    public class AmqpProcessingServiceException : Exception
    {
        public AmqpProcessingServiceException(Exception innerException)
           : base(message: "AMQP Processing Service error occurred.", innerException) { }
    }
}
