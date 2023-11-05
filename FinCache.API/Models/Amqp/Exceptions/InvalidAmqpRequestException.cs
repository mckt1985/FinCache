namespace FinCache.API.Models.Amqp.Exceptions
{
    public class InvalidAmqpRequestException : Exception
    {
        public InvalidAmqpRequestException()
            : base(message: "Invalid AMQP Request") { }
    }
}
