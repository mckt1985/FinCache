using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.API.Brokers.Amqp
{
    public interface IAmqpBroker
    {
        Task PublishAsync<T>(T message);
        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request);
        void Respond<TRequest, TResponse>(Func<TRequest, TResponse> response);
    }
}
