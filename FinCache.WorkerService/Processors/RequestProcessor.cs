using EasyNetQ;
using FinCache.WorkerService.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.WorkerService.Processors
{
    public class RequestProcessor : IRequestProcessor
    {
        private readonly IEnumerable<IMessageHandler> _handlers;
        private IBus _bus;

        public RequestProcessor(IEnumerable<IMessageHandler> handlers)
        {
            _bus = null;
            _handlers = handlers;
        }

        public void Start()
        {
            foreach (var handler in _handlers)
            {
                handler.Start();
            }
        }

        public void Stop()
        {
            foreach (var handler in _handlers)
            {
                handler.Stop();
            }

            if (_bus != null)
            {
                _bus.Dispose();
                _bus = null;
            }

        }
    }
}
