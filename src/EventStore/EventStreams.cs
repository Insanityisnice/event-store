using System;
using Microsoft.Extensions.DependencyInjection;
using EventStore.Persistence;
using Microsoft.Extensions.Logging;

namespace EventStore
{
    internal class EventStreams : IEventStreams
    {
        private readonly IServiceProvider provider;
        private readonly ILogger<EventStreams> logger;

        public EventStreams(IServiceProvider provider, ILogger<EventStreams> logger)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEventStream this[string streamName] => ConstructStream(streamName);
        
        private IEventStream ConstructStream(string streamName)
        {
            using var scope = logger.MethodScope(nameof(EventStreams), nameof(ConstructStream));

            var eventStream = new EventStream(streamName, provider.GetService<IStreamStore>(), provider.GetService<ILogger<EventStream>>());
            logger.StreamConstructed(streamName);

            return eventStream;
        }
    }
}