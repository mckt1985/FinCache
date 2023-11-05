namespace FinCache.API.Models.Amqp.Exceptions
{
    public class AmqpServiceDependencyException : Exception
    {
        public AmqpServiceDependencyException(Exception innerException)
            : base(message: "Amqp Processing Service dependency error occurred, please verify rabbit mq is running.", innerException) { }
    }
}

