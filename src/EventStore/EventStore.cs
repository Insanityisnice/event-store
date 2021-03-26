using System;
using Microsoft.Extensions.DependencyInjection;
using EventStore.Persistence;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace EventStore
{
    public interface IEventStore
    {
        IEventStreams Connect();
    }

    public interface IEventStreams
    {
        IEventStream this[string streamName] { get; }

        //IEnumerable<IEventStream> StreamsByCategory(string category);
    }

    public class EventStore : IEventStore
    {
        public IEventStreams Connect()
        {
            throw new NotImplementedException();
        }
    }

    public class EventStreams : IEventStreams
    {
        private readonly IServiceProvider provider;

        public EventStreams(IServiceProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public IEventStream this[string streamName] => GetEventStream(streamName);
        
        private IEventStream GetEventStream(string streamName)
        {
            return new EventStream(streamName, provider.GetService<IStreamStore>(), provider.GetService<ILogger<EventStream>>());
        }
    }
}