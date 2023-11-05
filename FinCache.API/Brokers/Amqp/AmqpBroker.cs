using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.API.Brokers.Amqp
{
    public class AmqpBroker : IAmqpBroker
    {
        private readonly IBus bus;

        public AmqpBroker(IBus bus)
        {
            this.bus = bus;
        }

        public void Respond<TRequest, TResponse>(Func<TRequest, TResponse> response)
        {
            this.bus.Rpc.Respond<TRequest, TResponse>(response);
        }

        public async Task PublishAsync<T>(T message)
        {
            await this.bus.PubSub.PublishAsync(message);
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
        {
            return await this.bus.Rpc.RequestAsync<TRequest, TResponse>(request);
        }
    }
}
