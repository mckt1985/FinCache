using EasyNetQ;

namespace FinCache.WorkerService.Handlers
{
    public abstract class MessageHandler<TMessage> : IMessageHandler, IDisposable where TMessage : class
    {
        private readonly IBus _bus;
        private IDisposable _subscription;
        protected abstract string HandlerId { get; }
        protected virtual string Topic { get { return "#"; } }
        protected virtual bool AutoDelete { get; private set; }

        protected MessageHandler(IBus bus)
        {
            AutoDelete = false;
            _bus = bus;
        }

        public virtual void Start()
        {
            _subscription = _bus.PubSub.Subscribe<TMessage>(HandlerId, Handle,
                x => x.WithTopic(Topic ?? "#").WithAutoDelete(AutoDelete));
        }

        public virtual void Stop()
        {
            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }
        }

        public abstract void Handle(TMessage message);

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
