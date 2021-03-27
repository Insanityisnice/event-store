using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventStore
{
    public class EventStore : IEventStore
    {
        private readonly IEventStreams streams;
        private readonly ILogger<EventStore> logger;

        internal EventStore(IServiceProvider provider, ILogger<EventStore> logger)
        {
            if(provider == null) throw new ArgumentNullException(nameof(provider));

            this.streams = new EventStreams(provider, provider.GetService<ILogger<EventStreams>>());
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEventStreams Streams
        {
            get => streams;
        }
    }
}