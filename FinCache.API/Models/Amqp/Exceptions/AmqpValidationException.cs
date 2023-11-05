namespace FinCache.API.Models.Amqp.Exceptions
{
    public class AmqpValidationException : Exception
    {
        public AmqpValidationException(Exception innerException)
            : base(message: "AMQP Validation Error", innerException) { }
    }
}
